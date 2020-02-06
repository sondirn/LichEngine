using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Textures;
using Nez.Sprites;
using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using LichEngine.Portable.States;

namespace LichEngine.GameCode.Components
{
    class Player : Component, ITriggerListener, IUpdatable
    {
        #region Fields
        public SpriteAnimator _animator;
        public SubpixelVector2 _subpixelV2 = new SubpixelVector2();
        public Mover _mover;
        public float _moveSpeed = 100f;
        public VirtualIntegerAxis _xAxisInput;
        public VirtualIntegerAxis _yAxisInput;
        public VirtualButton _attackInput;
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
            _mover = Entity.AddComponent(new Mover());
            //animator component
            _animator = Entity.AddComponent<SpriteAnimator>();

            //Set up collider
            _collider = Entity.AddComponent<CircleCollider>();
            _collider.Radius = 10;
            _collider.SetLocalOffset(new Vector2(0, 8));

            //Set Up Camera
            _camera = Entity.AddComponent(new FollowCamera(Entity));
            //_camera.FollowLerp = .03f;

            //Set up StateMachine
            _stateMachine = Entity.AddComponent(new StateMachine());
            _stateMachine.AddState(STATES.PLAYER_FREE, new PlayerStateFree(this));
            _stateMachine.CurrentState = STATES.PLAYER_FREE;
            #endregion

            #region Animations...
            _animator.AddAnimation("IdleSheathed", new[]
            {
                idleSprite[0],
                idleSprite[1],
                idleSprite[2],
                idleSprite[3]
                
            });
            _animator.AddAnimation("IdleUnSheathed", new[]
            {
                idleSprite[4],
                idleSprite[5],
                idleSprite[6],
                idleSprite[7]
            });
            _animator.AddAnimation("RunSheathed", new[]
            {
                runSprite[0],
                runSprite[1],
                runSprite[2],
                runSprite[3],
                runSprite[4],
                runSprite[5]
            });
            _animator.AddAnimation("RunUnSheathed", new[]
            {
                runSprite[6],
                runSprite[7],
                runSprite[8],
                runSprite[9],
                runSprite[10],
                runSprite[11]
            });
            #endregion
            //Set up Input
            SetupInput();
            base.OnAddedToEntity(); 
            
        }

        private void SetupInput()
        {
            // horizontal input from dpad, left stick or keyboard left/right
            _xAxisInput = new VirtualIntegerAxis();
            _xAxisInput.Nodes.Add(new VirtualAxis.GamePadDpadLeftRight());
            _xAxisInput.Nodes.Add(new VirtualAxis.GamePadLeftStickX());
            _xAxisInput.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.A, Keys.D));

            // vertical input from dpad, left stick or keyboard up/down
            _yAxisInput = new VirtualIntegerAxis();
            _yAxisInput.Nodes.Add(new VirtualAxis.GamePadDpadUpDown());
            _yAxisInput.Nodes.Add(new VirtualAxis.GamePadLeftStickY());
            _yAxisInput.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.W, Keys.S));

            //Attack input
            _attackInput = new VirtualButton();
            _attackInput.Nodes.Add(new VirtualButton.KeyboardKey(Keys.Space));
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

