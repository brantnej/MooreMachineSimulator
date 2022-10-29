using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooreMachineSimulator.Models
{
    public class MooreMachine
    {
        public MooreMachine(State startState)
        {
            CurrentState = startState;
        }
        public void Transition(string input)
        {
            var transition = CurrentState.Transitions.First(t => string.Equals(t.Input, input));
            if (transition == null)
            {
                throw new ArgumentException();
            }
            CurrentState = transition.NextState;
        }
        public State CurrentState { get; private set; }
    }
}
