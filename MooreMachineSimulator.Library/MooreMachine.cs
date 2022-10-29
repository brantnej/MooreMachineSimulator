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
            State q0 = new State("q0", "0000");
            State q1 = new State("q1", "0001");
            State q2 = new State("q2", "0010");
            State q3 = new State("q3", "1001");

            q0.Transitions = new List<StateTransition>()
            {
                new StateTransition()
                {
                    Input = "00",
                    NextState = q0
                },
            
                new StateTransition()
                {
                    Input = "01",
                    NextState = q0
                },
            
                new StateTransition()
                {
                    Input = "10",
                    NextState = q1
                },
            
                new StateTransition()
                {
                    Input = "11",
                    NextState = q0
                }
            };
            
            q1.Transitions = new List<StateTransition>()
            {
                new StateTransition()
                {
                    Input = "00",
                    NextState = q1
                },
            
                new StateTransition()
                {
                    Input = "01",
                    NextState = q0
                },
            
                new StateTransition()
                {
                    Input = "10",
                    NextState = q2
                },
            
                new StateTransition()
                {
                    Input = "11",
                    NextState = q1
                }
            };
            
            q2.Transitions = new List<StateTransition>()
            {
                new StateTransition()
                {
                    Input = "00",
                    NextState = q2
                },
            
                new StateTransition()
                {
                    Input = "01",
                    NextState = q1
                },
            
                new StateTransition()
                {
                    Input = "10",
                    NextState = q3
                },
            
                new StateTransition()
                {
                    Input = "11",
                    NextState = q2
                }
            };
            
            q3.Transitions = new List<StateTransition>()
            {
                new StateTransition()
                {
                    Input = "00",
                    NextState = q3
                },
            
                new StateTransition()
                {
                    Input = "01",
                    NextState = q2
                },
            
                new StateTransition()
                {
                    Input = "10",
                    NextState = q3
                },
            
                new StateTransition()
                {
                    Input = "11",
                    NextState = q3
                }
            };

            _currentState = q0;
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
                Console.WriteLine($"Currently on state {CurrentState}...");
                Console.WriteLine($"Inputting {input}...");
                Transition(input);
                Console.WriteLine($"FSM is now on state {CurrentState} and outputting {Output}");
                Console.WriteLine("---------------------------------------------------------------------------------");
            }
        }
    }
}
