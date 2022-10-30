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
            dynamic json = JsonConvert.DeserializeObject(JSONDefinition);

            if (json == null)
            {
                throw new ArgumentException();
            }

            List<State> states = new List<State>();

            if (string.Equals(Convert.ToString(json.type), "Moore"))
            {
                foreach (var state in json.states)
                {
                    states.Add(new State(Convert.ToString(state.name), Convert.ToString(state.output)));
                }
                foreach (var state in json.states)
                {
                    var currentState = states.First(s => s.Name == Convert.ToString(state.name));
                    foreach (var transition in state.transitions)
                    {
                        currentState.Transitions.Add(new StateTransition()
                        {
                            Input = transition.input,
                            NextState = states.First(s => s.Name == Convert.ToString(transition.nextstate))
                        });
                    }
                }
                _currentState = states.First();
            }
            else if (string.Equals(Convert.ToString(json.type), "Mealy"))
            {
                //TODO: Implement converting the Mealy Machine to Moore Machine
                throw new NotImplementedException();
            }
            else
            {
                throw new ArgumentException();
            }

            
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
        public void Simulate(string inputJSON)
        {
            dynamic json = JsonConvert.DeserializeObject(inputJSON);

            if (json == null)
            {
                throw new ArgumentException();
            }

            foreach (var i in json.inputs)
            {
                string input = Convert.ToString(i);
                Console.WriteLine($"Currently on state {CurrentState} and outputting {Output}");
                Console.WriteLine($"Inputting {input}...");
                Transition(input);
                Console.WriteLine($"FSM has transitioned to state {CurrentState} and outputting {Output}");
                Console.WriteLine("---------------------------------------------------------------------------------");
            }
        }
    }
}
