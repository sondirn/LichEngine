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
            if (!Enabled)
                return;
            if (States.ContainsKey(CurrentState))
            {
                States[CurrentState].Run();
            }
        }

        public void SetState(STATES state)
        {
            States[CurrentState].StateExit();
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
