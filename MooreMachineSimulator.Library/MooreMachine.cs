using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooreMachineSimulator.Models
{
    public class MooreMachine
    {
        private State _currentState;
        public string CurrentState => _currentState.Name;
        public string Output => _currentState.Output;
        public MooreMachine(string JSONDefinition)
        {
            //TODO: Initialize Machine using JSON
            dynamic items = JsonConvert.DeserializeObject(JSONDefinition);

            if (items == null)
            {
                throw new ArgumentException();
            }

            List<State> states = new List<State>();

            foreach (var state in items.states)
            {
                states.Add(new State(Convert.ToString(state.name), Convert.ToString(state.output)));
            }
            foreach (var state in items.states)
            {
                var currentState = states.First(s => s.Name == Convert.ToString(state.name));
                foreach (var transition in state.transitions)
                {
                    currentState.Transitions.Add(new StateTransition()
                    {
                        Input = transition.input,
                        NextState = states.First(s => s.Name == Convert.ToString(transition.nextstate))
                    }) ;
                }
            }
            _currentState = states.First();
        }
        public void Transition(string input)
        {
            var transition = _currentState.Transitions.First(t => string.Equals(t.Input, input));
            if (transition == null)
            {
                throw new ArgumentException();
            }
            _currentState = transition.NextState;
        }
        public void Simulate(IEnumerable<string> inputs)
        {
            foreach (string input in inputs)
            {
                Console.WriteLine($"Currently on state {CurrentState} and outputting {Output}");
                Console.WriteLine($"Inputting {input}...");
                Transition(input);
                Console.WriteLine($"FSM has transitioned to state {CurrentState} and outputting {Output}");
                Console.WriteLine("---------------------------------------------------------------------------------");
            }
        }
    }
}
