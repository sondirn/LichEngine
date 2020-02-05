using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Textures;
using Nez.Sprites;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace LichEngine.GameCode.Components
{
    class Player : Component, ITriggerListener, IUpdatable
    {
        

        SpriteAnimator _animator;

        SubpixelVector2 _subpixelV2 = new SubpixelVector2();
        Mover _mover;
        float _moveSpeed = 100f;
        VirtualButton _fireInput;
        VirtualIntegerAxis _xAxisInput;
        VirtualIntegerAxis _yAxisInput;
        SpriteRenderer _sprite;
        CameraShake shake;

        public override void OnAddedToEntity()
        {
            var texture = Entity.Scene.Content.Load<Texture2D>("Textures/DefaultTexture");
            _mover = Entity.AddComponent(new Mover());
            _sprite = Entity.AddComponent(new SpriteRenderer(texture: texture));
            SetupInput();
            var camera = Entity.AddComponent(new FollowCamera(Entity));
            camera.FollowLerp = 1f;
            shake = Entity.AddComponent(new CameraShake());
            
            base.OnAddedToEntity();
        }

        void SetupInput()
        {
            _xAxisInput = new VirtualIntegerAxis();
            _xAxisInput.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.A, Keys.D));

            _yAxisInput = new VirtualIntegerAxis();
            _yAxisInput.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.W, Keys.S));
        }
        public void Update()
        {
            //shake.Shake();
            var _moveDir = new Vector2(_xAxisInput.Value, _yAxisInput.Value);
            if(_moveDir != Vector2.Zero)
            {
                var movement = _moveDir * _moveSpeed * Time.DeltaTime;
                movement = Vector2.Normalize(movement);
                _mover.CalculateMovement(ref movement, out var res);
                _subpixelV2.Update(ref movement);
                _mover.ApplyMovement(movement);
            }

        }
        public void OnTriggerEnter(Collider other, Collider local)
        {
            //throw new NotImplementedException();
        }

        public void OnTriggerExit(Collider other, Collider local)
        {
            //throw new NotImplementedException();
        }

    }
}
