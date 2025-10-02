using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    abstract class State
    {

        protected bool _end;
        protected Stack<State> _states;

        public State(Stack<State> states)
        {
            this._states = states;
        }

        public bool RequestEnd() //Method for ending the state
        {
            return this._end;
        }
        virtual public void Update() //Method for updating state
        {

        }

        virtual public void ProcessInput(int? input) //Method for menu logic
        {

        }
    }
}