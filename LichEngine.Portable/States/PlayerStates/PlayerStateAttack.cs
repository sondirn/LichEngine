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
        private bool _attackQueued = false;
        private float _attackQueueTimer = 0.0f;
        private float _attackQueueEnd = .2f;

        public PlayerStateAttack(Player player)
        {
            _player = player;
            _stateMachine = player.StateMachine;
            attackNum = 0;
        }

        private void Animator_OnAnimationCompletedEvent(string obj)
        {
            
            attackNum++;
            if (_attackQueueTimer < _attackQueueEnd && _attackQueued)
            {
                _stateMachine.SetState(STATES.PLAYER_ATTACK);
                return;
            }
            _stateMachine.SetState(STATES.PLAYER_FREE);
             
            
        }

        public override void StateEnter()
        {
            _player.Animator.OnAnimationCompletedEvent += Animator_OnAnimationCompletedEvent;
            _attackQueued = false;
            

            base.StateEnter();
        }

        public override void Update()
        {
            if (attackNum > 2)
            {
                attackNum = 0;
            }
            if (_player.AttackInput.IsPressed)
            {
                _attackQueued = true;
                _attackQueueTimer = 0.0f;
            }
            _attackQueueTimer += Time.DeltaTime;
            if (_attackQueueTimer >= _attackQueueEnd)
                _attackQueued = false;
            var animation = "Attack" + attackNum.ToString();
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
            //sheathe._velocity.X = 0;
            _player.Animator.OnAnimationCompletedEvent -= Animator_OnAnimationCompletedEvent;
            base.StateExit();
        }

    }
}
