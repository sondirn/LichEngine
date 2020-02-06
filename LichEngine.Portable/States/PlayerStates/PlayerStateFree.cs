using LichEngine.GameCode.Components;
using LichEngine.Portable.States;
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
        private bool Sheathed = false;
        public PlayerStateFree(Player player)
        {
            _player = player;
        }

        public override void Update()
        {
            if (_player.AttackInput.IsDown)
            {
                _player._stateMachine.SetState(STATES.PLAYER_ATTACK);
                return;
            }
            //Calculate movement 
            var moveDir = new Vector2(_player.X_AxisInput.Value, _player.Y_AxisInput.Value);
            var animation = "Run" + GetSheathe();
            //handle movement
            if (moveDir != Vector2.Zero)
            {
                //animation
                _player.Animator.Speed = 1;
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
                var movement = moveDir * _player.MoveSpeed * Time.DeltaTime;
                _player.Mover.CalculateMovement(ref movement, out var res);
                _player._subpixelV2.Update(ref movement);
                _player.Mover.ApplyMovement(movement);
            }
            else
            {
                animation = "Idle" + GetSheathe();
                _player.Animator.Speed = 1;
                if (!_player.Animator.IsAnimationActive(animation))
                    _player.Animator.Play(animation);
                else
                    _player.Animator.UnPause();

            }
            
            base.Update();
        }

        private string GetSheathe()
        {
            return (this.Sheathed ? "Sheathed" : "UnSheathed");
        }
    }
}
