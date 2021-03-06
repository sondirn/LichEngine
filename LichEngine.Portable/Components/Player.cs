﻿using LichEngine.GameCode.Scenes;
using LichEngine.Portable.Components;
using LichEngine.Portable.States;
using LichEngine.Portable.States.PlayerStates;
using LichEngine.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using Nez.Tiled;

namespace LichEngine.GameCode.Components
{
    public class Player : Component, ITriggerListener, IUpdatable
    {
        #region Fields

        public SpriteAnimator Animator;
        public SubpixelVector2 _subpixelV2 = new SubpixelVector2();
        public TiledMapMover Mover;
        public TiledMapMover.CollisionState CollisionState = new TiledMapMover.CollisionState();
        public CollisionResult CollisionResult;

        [Range(0, 10, .5f)]
        public float MoveSpeed = 1.5f;

        [NotInspectable]
        public float MoveSpeedModifier = 100;

        public VirtualIntegerAxis X_AxisInput;
        public VirtualIntegerAxis Y_AxisInput;
        public VirtualButton AttackInput;
        public VirtualButton JumpInput;
        public FollowCamera Camera;
        public BoxCollider Collider;
        public StateMachine StateMachine;
        public CollisionListener CollisionListener;
        public TypeComponent Type;

        #endregion Fields

        public override void OnAddedToEntity()
        {
            #region Load up texture atlas...

            //Load up character texture atlas
            var idleTexture = Entity.Scene.Content.Load<Texture2D>(Content.Textures.HeroIdle);
            var runTexture = Entity.Scene.Content.Load<Texture2D>(Content.Textures.HeroRun);
            var attackTexture = Entity.Scene.Content.Load<Texture2D>(Content.Textures.HeroAttack);
            var jumpTexture = Entity.Scene.Content.Load<Texture2D>(Content.Textures.HeroJump);
            var fallTexture = Entity.Scene.Content.Load<Texture2D>(Content.Textures.HeroFall);
            var idleSprite = Sprite.SpritesFromAtlas(idleTexture, 50, 37);
            var runSprite = Sprite.SpritesFromAtlas(runTexture, 50, 37);
            var attackSprite = Sprite.SpritesFromAtlas(attackTexture, 50, 37);
            var jumpSprite = Sprite.SpritesFromAtlas(jumpTexture, 50, 37);
            var fallSprite = Sprite.SpritesFromAtlas(fallTexture, 50, 37);

            #endregion Load up texture atlas...

            #region add componentents...

            //Movement component
            var map = Entity.Scene as SandBoxScene;
            Mover = Entity.AddComponent(new TiledMapMover(map.TiledMap.GetLayer<TmxLayer>("main")));
            //animator component
            Animator = Entity.AddComponent<SpriteAnimator>();

            //Set up collider
            Collider = Entity.AddComponent<BoxCollider>();
            Collider.Width = 16;
            Collider.Height = 30;
            Collider.SetLocalOffset(new Vector2(0, 3));
            Flags.SetFlagExclusive(ref Collider.CollidesWithLayers, 1);
            Flags.SetFlagExclusive(ref Collider.PhysicsLayer, 1);

            //CollisionListener
            CollisionListener = Entity.AddComponent<CollisionListener>();

            //SetType
            Type = Entity.AddComponent<TypeComponent>();
            Type.SetType(EntityType.PLAYER);

            //Set up StateMachine
            StateMachine = Entity.AddComponent(new StateMachine());
            StateMachine.AddState(STATES.PLAYER_FREE, new PlayerPlatformerStateFree(this));
            StateMachine.AddState(STATES.PLAYER_ATTACK, new PlayerStateAttack(this));
            StateMachine.CurrentState = STATES.PLAYER_FREE;

            //Set up Camera
            var camera = new FollowCamera(Entity);
            camera.MapLockEnabled = true;

            Entity.AddComponent(camera);
            var renderer = new DefaultRenderer(camera: camera.Camera);

            Entity.Scene.AddRenderer(renderer);
            //camera.Camera.Position = Entity.Transform.Position;
            camera.MapSize = new Vector2(1280, 0);
            camera.FollowLerp = .3f;

            #endregion add componentents...

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
            Animator.AddAnimation("Jump", 15, new[]{
                //jumpSprite[0],
                //jumpSprite[1],
                jumpSprite[2],
                jumpSprite[3]
            });
            Animator.AddAnimation("Fall", 8, new[]
            {
                fallSprite[0],
                fallSprite[1]
            });

            #endregion Animations...

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
            X_AxisInput.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.Left, Keys.Right));
            X_AxisInput.Nodes.Add(new VirtualAxis.GamePadLeftStickX());
            X_AxisInput.Nodes.Add(new VirtualAxis.GamePadDpadLeftRight());

            // vertical input from dpad, left stick or keyboard up/down
            Y_AxisInput = new VirtualIntegerAxis();
            Y_AxisInput.Nodes.Add(new VirtualAxis.GamePadDpadUpDown());
            Y_AxisInput.Nodes.Add(new VirtualAxis.GamePadLeftStickY());
            Y_AxisInput.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.W, Keys.S));
            Y_AxisInput.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.Up, Keys.Down));
            Y_AxisInput.Nodes.Add(new VirtualAxis.GamePadLeftStickY());
            Y_AxisInput.Nodes.Add(new VirtualAxis.GamePadDpadUpDown());

            //Attack input
            AttackInput = new VirtualButton();
            AttackInput.Nodes.Add(new VirtualButton.MouseLeftButton());
            AttackInput.Nodes.Add(new VirtualButton.KeyboardKey(Keys.M));
            AttackInput.Nodes.Add(new VirtualButton.GamePadButton(0, Buttons.X));

            //Jump INput
            JumpInput = new VirtualButton();
            JumpInput.Nodes.Add(new VirtualButton.KeyboardKey(Keys.Space));
            JumpInput.Nodes.Add(new VirtualButton.GamePadButton(0, Buttons.A));
        }

        public void Update()
        {
        }

        public void OnTriggerEnter(Collider other, Collider local)
        {
            //DebugConsole.Instance.Log("triggerEnter: {0}", other.Entity.Name);
            var type = other.Entity.GetComponent<TypeComponent>();
            Debug.Log("Colliding with: {0}", type.EntityType);
            //Animator.Color = Color.Red;
        }

        public void OnTriggerExit(Collider other, Collider local)
        {
            Debug.Log("triggerExit: {0}", other.Entity.Name);
            Animator.Color = Color.White;
        }
    }
}