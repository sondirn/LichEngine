using LichEngine.GameCode.Components;
using LichEngine.Portable.States;
using LichEngine.Portable.States.PlayerStates;
using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LichEngine.States
{
    public class PlayerStateFree : KindredState
    {
        private Player _player;
        public bool Sheathed = false;
        private float _sheathedTimer;
        private float _attackComboResetTimer;
        public PlayerStateFree(Player player)
        {
            _player = player;
            Sheathed = true;
            _sheathedTimer = 0.0f;
            _attackComboResetTimer = 0.0f;
        }

        public override void StateEnter()
        {
            _attackComboResetTimer = 0.0f;
            base.StateEnter();
        }

        public override void Update()
        {
            _attackComboResetTimer += Time.DeltaTime;
            if (_attackComboResetTimer > .3f)
            {
                var attackState = _player.StateMachine.States[STATES.PLAYER_ATTACK] as PlayerStateAttack;
                attackState.attackNum = 0;
            }
            if (_player.AttackInput.IsPressed)
            {
                _player.StateMachine.SetState(STATES.PLAYER_ATTACK);
                return;
            }
            //Calculate movement 
            var moveDir = new Vector2(_player.X_AxisInput.Value, _player.Y_AxisInput.Value);
            var animation = "Run" + GetSheathe();
            //handle movement
            if (moveDir != Vector2.Zero)
            {
                //reset sheathe timer
                _sheathedTimer = 0.0f;
                //animation
                //_player.Animator.Speed = 1;
                animation = "Run" + GetSheathe();
                if (!_player.Animator.IsAnimationActive(animation))
                    _player.Animator.Play(animation);
                else
                    _player.Animator.UnPause();
                //Flip depending on where we are facing
                if (moveDir.X > 0)
                    _player.Animator.FlipX = false;
                if (moveDir.X < 0)
                    _player.Animator.FlipX = true;
                //move character
                moveDir = Vector2.Normalize(moveDir);
                var movement = moveDir * _player.MoveSpeed * _player.MoveSpeedModifier * Time.DeltaTime;
                _player.Mover.CalculateMovement(ref movement, out var res);
                _player._subpixelV2.Update(ref movement);
                _player.Mover.ApplyMovement(movement);
            }
            else
            {
                //start sheathing timer
                if (!Sheathed)
                {
                    _sheathedTimer += Time.DeltaTime;
                }
                if (_sheathedTimer > 3f)
                {
                    //sheathe weapon
                    SetSheathe();
                }
                animation = "Idle" + GetSheathe();
                //_player.Animator.Speed = 1;
                if (!_player.Animator.IsAnimationActive(animation))
                    _player.Animator.Play(animation);
                else
                    _player.Animator.UnPause();

            }
            
            base.Update();
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
