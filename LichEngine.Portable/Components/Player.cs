using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Textures;
using Nez.Sprites;
using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using LichEngine.Portable.States;
using LichEngine.States;
using LichEngine.Portable.States.PlayerStates;

namespace LichEngine.GameCode.Components
{
    public class Player : Component, ITriggerListener, IUpdatable
    {
        #region Fields
        public SpriteAnimator Animator;
        public SubpixelVector2 _subpixelV2 = new SubpixelVector2();
        public Mover Mover;
        [Range(0, 500, 10)]
        public float MoveSpeed = 100f;
        public VirtualIntegerAxis X_AxisInput;
        public VirtualIntegerAxis Y_AxisInput;
        public VirtualButton AttackInput;
        public FollowCamera _camera;
        public CircleCollider _collider;
        public StateMachine _stateMachine;
        #endregion

        public override void OnAddedToEntity()
        {
            #region Load up texture atlas...
            //Load up character texture atlas
            var idleTexture     = Entity.Scene.Content.Load<Texture2D>(Content.Textures.HeroIdle);
            var runTexture      = Entity.Scene.Content.Load<Texture2D>(Content.Textures.HeroRun);
            var attackTexture   = Entity.Scene.Content.Load<Texture2D>(Content.Textures.HeroAttack);
            var idleSprite      = Sprite.SpritesFromAtlas(idleTexture, 50, 37);
            var runSprite       = Sprite.SpritesFromAtlas(runTexture, 50, 37);
            var attackSprite    = Sprite.SpritesFromAtlas(attackTexture, 50, 37);
            #endregion

            #region add componentents...
            //Movement component
            Mover = Entity.AddComponent(new Mover());
            //animator component
            Animator = Entity.AddComponent<SpriteAnimator>();

            //Set up collider
            _collider = Entity.AddComponent<CircleCollider>();
            _collider.Radius = 10;
            _collider.SetLocalOffset(new Vector2(0, 8));
            
            //Set up StateMachine
            _stateMachine = Entity.AddComponent(new StateMachine());
            _stateMachine.AddState(STATES.PLAYER_FREE, new PlayerStateFree(this));
            _stateMachine.AddState(STATES.PLAYER_ATTACK, new PlayerStateAttack(this));
            _stateMachine.CurrentState = STATES.PLAYER_FREE;
            #endregion

            #region Animations...
            Animator.AddAnimation("IdleSheathed", 4, new[]
            {
                idleSprite[0],
                idleSprite[1],
                idleSprite[2],
                idleSprite[3]
                
            });
            Animator.AddAnimation("IdleUnSheathed", 4, new[]
            {
                idleSprite[4],
                idleSprite[5],
                idleSprite[6],
                idleSprite[7]
            });
            Animator.AddAnimation("RunSheathed", 7.5f, new[]
            {
                runSprite[0],
                runSprite[1],
                runSprite[2],
                runSprite[3],
                runSprite[4],
                runSprite[5]
            });
            Animator.AddAnimation("RunUnSheathed", 7.5f, new[]
            {
                runSprite[6],
                runSprite[7],
                runSprite[8],
                runSprite[9],
                runSprite[10],
                runSprite[11]
            });
            Animator.AddAnimation("Attack0", 12, new[]
            {
                attackSprite[0],
                attackSprite[1],
                attackSprite[2],
                attackSprite[3],
                attackSprite[4],
                attackSprite[5],
            });
            Animator.AddAnimation("Attack1", 12, new[]
            {
                attackSprite[6],
                attackSprite[7],
                attackSprite[8],
                attackSprite[9],
                attackSprite[10]
            });
            Animator.AddAnimation("Attack2", 12, new[]
            {
                attackSprite[11],
                attackSprite[12],
                attackSprite[13],
                attackSprite[14],
                attackSprite[15],
                attackSprite[16]
            });
            #endregion
            //Set up Input
            SetupInput();
            base.OnAddedToEntity(); 
            
        }

        private void SetupInput()
        {
            // horizontal input from dpad, left stick or keyboard left/right
            X_AxisInput = new VirtualIntegerAxis();
            X_AxisInput.Nodes.Add(new VirtualAxis.GamePadDpadLeftRight());
            X_AxisInput.Nodes.Add(new VirtualAxis.GamePadLeftStickX());
            X_AxisInput.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.A, Keys.D));

            // vertical input from dpad, left stick or keyboard up/down
            Y_AxisInput = new VirtualIntegerAxis();
            Y_AxisInput.Nodes.Add(new VirtualAxis.GamePadDpadUpDown());
            Y_AxisInput.Nodes.Add(new VirtualAxis.GamePadLeftStickY());
            Y_AxisInput.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.W, Keys.S));

            //Attack input
            AttackInput = new VirtualButton();
            AttackInput.Nodes.Add(new VirtualButton.KeyboardKey(Keys.Space));
        }

        

        public void Update()
        {
            //_stateMachine
        }

        public void OnTriggerEnter(Collider other, Collider local)
        {
            Debug.Log("triggerEnter: {0}", other.Entity.Name);
        }

        public void OnTriggerExit(Collider other, Collider local)
        {
            Debug.Log("triggerExit: {0}", other.Entity.Name);
        }
    }
}

