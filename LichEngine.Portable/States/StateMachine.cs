using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LichEngine.Portable.States
{
    public class StateMachine : Component, IUpdatable
    {
        public Dictionary<STATES, KindredState> States;
        public STATES CurrentState { get; set; }

        public StateMachine()
        {
            States = new Dictionary<STATES, KindredState>();
            CurrentState = STATES.DEFAULT;
        }

        public void Update()
        {
            if (States.ContainsKey(CurrentState))
            {
                States[CurrentState].Run();
            }
            else
            {
                throw new NotImplementedException("State " + CurrentState + " Is not implemented");
            }
        }

        public void SetState(STATES state)
        {
            CurrentState = state;
        }

        public void AddState(STATES stateName, KindredState state)
        {
            States.Add(stateName, state);
        }
    }

    public enum STATES
    {
        DEFAULT,
        PLAYER_FREE,
        PLAYER_ATTACK
    }
}
