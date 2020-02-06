using LichEngine.GameCode.Components;
using LichEngine.States;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Nez.Sprites.SpriteAnimator;

namespace LichEngine.Portable.States.PlayerStates
{
    public class PlayerStateAttack : KindredState
    {
        private Player _player;
        private StateMachine _stateMachine;
        public int attackNum;

        public PlayerStateAttack(Player player)
        {
            _player = player;
            _stateMachine = player.StateMachine;
            player.Animator.OnAnimationCompletedEvent += Animator_OnAnimationCompletedEvent;
            attackNum = 0;
        }

        private void Animator_OnAnimationCompletedEvent(string obj)
        {
            attackNum++;

            _stateMachine.SetState(STATES.PLAYER_FREE);
            
        }

        public override void StateEnter()
        {
            if (_player.Entity.Scene.Camera.ScreenToWorldPoint(Input.MousePosition).X < _player.Transform.Position.X)
            {
                _player.Animator.FlipX = true;
            }
            else
            {
                _player.Animator.FlipX = false;
            }
            if (attackNum > 2)
            {
                attackNum = 0;
            }

            base.StateEnter();
        }

        public override void Update()
        {
            var animation = "Attack" + attackNum.ToString();
            //_player.Animator._loopMode = Nez.Sprites.SpriteAnimator.LoopMode.Once;
            if (!_player.Animator.IsAnimationActive(animation))
                _player.Animator.Play(animation, LoopMode.Once);
            else
                _player.Animator.UnPause();
            base.Update();
        }

        public override void StateExit()
        {
            
            var sheathe = _player.StateMachine.States[STATES.PLAYER_FREE] as PlayerStateFree;
            sheathe.SetUnSheathe();
            base.StateExit();
        }

    }
}
