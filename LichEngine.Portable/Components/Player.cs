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
        [Range(0, 10, .5f)]
        public float MoveSpeed = 1f;
        [NotInspectable]
        public float MoveSpeedModifier = 100f;
        public VirtualIntegerAxis X_AxisInput;
        public VirtualIntegerAxis Y_AxisInput;
        public VirtualButton AttackInput;
        public FollowCamera Camera;
        public CircleCollider Collider;
        public StateMachine StateMachine;
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
            Collider = Entity.AddComponent<CircleCollider>();
            Collider.Radius = 10;
            Collider.SetLocalOffset(new Vector2(0, 8));
            
            //Set up StateMachine
            StateMachine = Entity.AddComponent(new StateMachine());
            StateMachine.AddState(STATES.PLAYER_FREE, new PlayerStateFree(this));
            StateMachine.AddState(STATES.PLAYER_ATTACK, new PlayerStateAttack(this));
            StateMachine.CurrentState = STATES.PLAYER_FREE;

            //Set up Camera
            var camera = new FollowCamera(Entity);
            Entity.AddComponent(camera);
            Entity.Scene.AddRenderer(new DefaultRenderer(camera: camera.Camera));
            #endregion

            #region Animations...
            Animator.AddAnimation("IdleSheathed", 3.5f, new[]
            {
                idleSprite[0],
                idleSprite[1],
                idleSprite[2],
                idleSprite[3]
                
            });
            Animator.AddAnimation("IdleUnSheathed", 3.5f, new[]
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
            Animator.AddAnimation("Attack1", 10, new[]
            {
                attackSprite[6],
                attackSprite[7],
                attackSprite[8],
                attackSprite[9],
                attackSprite[10]
            });
            Animator.AddAnimation("Attack2", 8, new[]
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
            AttackInput.Nodes.Add(new VirtualButton.MouseLeftButton());
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

