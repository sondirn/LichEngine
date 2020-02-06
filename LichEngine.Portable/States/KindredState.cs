using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LichEngine.Portable.States
{
    public class KindredState
    {
        public bool Entered = false;
        public bool Exiting = false;
        
        public virtual void StateEnter()
        { Entered = true; }
        
        public virtual void Update()
        { }

        public virtual void LateUpdate()
        { }

        public virtual void StateExit()
        { }

        public virtual void Run()
        {
            if (!Entered)
            {
                StateEnter();
            }
            Update();
            LateUpdate();
            if (Exiting)
            {
                StateExit();
            }
        }

        public void ChangeState(string name)
        {

        }
    }
}
