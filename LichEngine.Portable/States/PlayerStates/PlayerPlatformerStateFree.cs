using LichEngine.GameCode.Components;
using LichEngine.Portable.States;
using Microsoft.Xna.Framework;
using Nez;
using static Nez.Sprites.SpriteAnimator;

namespace LichEngine.States
{
    public class PlayerPlatformerStateFree : KindredState
    {
        private readonly Player _player;
        public bool Sheathed = false;
        public float Gravity = 10f;
        public Vector2 Velocity;
        private float _sheathedTimer;
        private float _attackComboResetTimer;
        private float _jumpGrace;
        private readonly float _acceleration = 1000f;
        private readonly float _deceleration = 900f;
        private float _maxSpeed = 100;
        private readonly float _jumpVelocity = 2.2f;
        private readonly float _fallMult = 1.3f;
        private readonly float _lowJumpMult = 1.7f;
        private float _landFallQueue = 0f;
        private readonly float _landFallQueueEnd = .3f;
        private bool _jumpQueued = false;
        private bool _attackQueued = false;

        public PlayerPlatformerStateFree(Player player)
        {
            _player = player;
            Sheathed = true;
            _sheathedTimer = 0.0f;
            _attackComboResetTimer = 0.0f;
            _jumpGrace = 0.0f;
            Velocity = Vector2.Zero;
            _maxSpeed = player.MoveSpeed * player.MoveSpeedModifier;
        }

        public override void StateEnter()
        {
            _attackComboResetTimer = 0.0f;
            _sheathedTimer = 0;
            if (_player.AttackInput.IsPressed && _player.CollisionState.Below)
            {
                _player.StateMachine.SetState(STATES.PLAYER_ATTACK);
            }
            base.StateEnter();
        }

        public override void Update()
        {
            //Weapon sheathe code
            if (!Sheathed)
                _sheathedTimer += Time.DeltaTime;
            if (_sheathedTimer >= 3)
                Sheathed = true;
            //Check for attack
            if (_player.AttackInput.IsPressed && _player.CollisionState.Below)
            {
                _player.StateMachine.SetState(STATES.PLAYER_ATTACK);
                return;
            }
            _maxSpeed = _player.MoveSpeed * _player.MoveSpeedModifier * Time.DeltaTime;
            var tempAcceleration = _acceleration * _player.MoveSpeed * Time.DeltaTime;
            var tempDeceleration = _deceleration * _player.MoveSpeed * Time.DeltaTime;
            if (!_player.CollisionState.Below) { tempAcceleration /= 1.5f; tempDeceleration /= 3; }
            string animation = null;
            var loopMode = LoopMode.Loop;
            var moveDir = new Vector2(_player.X_AxisInput.Value, 0);
            if (moveDir.X < 0)
            {
                //move left
                Velocity.X += -tempAcceleration * Time.DeltaTime;
                _player.Animator.FlipX = true;
                animation = "Run" + GetSheathe();
                //clamp speed
                if (Velocity.X < -_maxSpeed)
                    Velocity.X = -_maxSpeed;
            }
            else if (moveDir.X > 0)
            {
                //move right
                Velocity.X += tempAcceleration * Time.DeltaTime;
                _player.Animator.FlipX = false;
                animation = "Run" + GetSheathe();
                //clamp speed
                if (Velocity.X > _maxSpeed)
                    Velocity.X = _maxSpeed;
            }
            else
            {
                //Drag
                if (Velocity.X > tempDeceleration * Time.DeltaTime)
                {
                    Velocity.X -= tempDeceleration * Time.DeltaTime;
                }
                else if (Velocity.X < -tempDeceleration * Time.DeltaTime)
                {
                    Velocity.X += tempDeceleration * Time.DeltaTime;
                }
                else
                {
                    Velocity.X = 0f;
                    animation = "Idle" + GetSheathe();
                }
            }

            //jump grace
            if (!_player.CollisionState.Below)
            {
                _jumpGrace += Time.DeltaTime;

                //Queue jump while falling
                if (_player.JumpInput.IsPressed) { _landFallQueue = 0f; _jumpQueued = true; }
                if (_player.AttackInput.IsPressed) { _landFallQueue = 0f; _attackQueued = true; }
                _landFallQueue += Time.DeltaTime;
                if (_landFallQueue >= _landFallQueueEnd)
                {
                    _jumpQueued = false;
                    _attackQueued = false;
                }
            }
            //jump
            if (_jumpGrace < .1f && _player.JumpInput.IsPressed)
            {
                Velocity.Y = -Mathf.Sqrt(_jumpVelocity * Gravity);
                _sheathedTimer = 0;
                _jumpGrace = 1f;
            }
            //jump if queued
            if (_player.CollisionState.BecameGroundedThisFrame && _jumpQueued && _landFallQueue < _landFallQueueEnd)
            {
                Velocity.Y = -Mathf.Sqrt(_jumpVelocity * Gravity);
                _sheathedTimer = 0;
                _jumpQueued = false;
                _jumpGrace = 1f;
            }
            //attack if queued
            if (_player.CollisionState.BecameGroundedThisFrame && _attackQueued && _landFallQueue < _landFallQueueEnd)
            {
                _attackQueued = false;
                _player.StateMachine.SetState(STATES.PLAYER_ATTACK);
            }
            //better jumping
            if (Velocity.Y > 0)
            {
                //Faster Falling
                Velocity += Vector2.UnitY * Gravity * _fallMult * Time.DeltaTime;
            }
            else if (Velocity.Y < 0 && !_player.JumpInput.IsDown)
            {
                Velocity += Vector2.UnitY * Gravity * _lowJumpMult * Time.DeltaTime;
            }
            //Set Vertical Velocity to 0 if colliding with something above
            if (_player.CollisionState.Above) { Velocity.Y = 0f; }

            //Apply Gravity
            Velocity.Y += Gravity * Time.DeltaTime;
            if (Velocity.Y >= Gravity * .75f)
            {
                Velocity.Y = Gravity * .75f;
            }
            //reset y velocity if touching ground
            if (_player.CollisionState.Below && Velocity.Y >= 0)
            {
                Velocity.Y = 0f;
                _jumpGrace = 0;
            }
            //DebugConsole.Instance.Log(_velocity);
            //jump animations
            if (Velocity.Y < 0) { animation = "Jump"; loopMode = LoopMode.Once; } else if (Velocity.Y > 0) { animation = "Fall"; loopMode = LoopMode.Loop; }
            var res = new CollisionResult();
            _player.Mover.CalculateMovement(ref Velocity, out res);
            if (res.Collider != null)
            {
                if (res.Collider.Entity != null)
                {
                    //DebugConsole.Instance.Log(res.Collider.Entity.Name);
                }
            }
            //Console.WriteLine(res.ToString());
            _player.Mover.Move(Velocity, _player.Collider, _player.CollisionState);

            if (animation != null && !_player.Animator.IsAnimationActive(animation))
                _player.Animator.Play(animation, loopMode);

            base.Update();
        }

        public override void StateExit()
        {
            Velocity.X = 0;
            base.StateExit();
        }

        public void SetSheathe()
        {
            Sheathed = true;
            _sheathedTimer = 0.0f;
        }

        public void SetUnSheathe()
        {
            this.Sheathed = false;
            _sheathedTimer = 0.0f;
        }

        private string GetSheathe()
        {
            return (this.Sheathed ? "Sheathed" : "UnSheathed");
        }
    }
}