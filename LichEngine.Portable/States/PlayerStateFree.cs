using LichEngine.GameCode.Components;
using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LichEngine.Portable.States
{
    class PlayerStateFree : KindredState
    {
        private Player _player;
        private bool Sheathed = false;
        public PlayerStateFree(Player player)
        {
            _player = player;
        }

        public override void Update()
        {
            //Calculate movement 
            var moveDir = new Vector2(_player._xAxisInput.Value, _player._yAxisInput.Value);
            var animation = "Run" + GetSheathe();
            //handle movement
            if (moveDir != Vector2.Zero)
            {
                //animation
                _player._animator.Speed = .75f;
                animation = "Run" + GetSheathe();
                if (!_player._animator.IsAnimationActive(animation))
                    _player._animator.Play(animation);
                else
                    _player._animator.UnPause();
                //Flip depending on where we are facing
                if (moveDir.X > 0)
                    _player._animator.FlipX = false;
                if (moveDir.X < 0)
                    _player._animator.FlipX = true;
                //move character
                moveDir = Vector2.Normalize(moveDir);
                var movement = moveDir * _player._moveSpeed * Time.DeltaTime;
                _player._mover.CalculateMovement(ref movement, out var res);
                _player._subpixelV2.Update(ref movement);
                _player._mover.ApplyMovement(movement);
            }
            else
            {
                animation = "Idle" + GetSheathe();
                _player._animator.Speed = .50f;
                if (!_player._animator.IsAnimationActive(animation))
                    _player._animator.Play(animation);
                else
                    _player._animator.UnPause();

            }
            if (_player._attackInput.IsPressed)
            {
                Sheathed = !Sheathed;
            }
            base.Update();
        }

        private string GetSheathe()
        {
            return (this.Sheathed ? "Sheathed" : "UnSheathed");
        }
    }
}
