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
        /// <summary>
        /// Construct a Moore Machine using a definition stored in a JSON file.
        /// </summary>
        /// <param name="JSONDefinition">The JSON definition for the FSM.</param>
        /// <exception cref="ArgumentException">Throws if the JSON file is not a proper FSM definition.</exception>
        public MooreMachine(string JSONDefinition)
        {
            dynamic? json = JsonConvert.DeserializeObject(JSONDefinition);

            if (json == null)
            {
                throw new ArgumentException();
            }
            if (json.states == null)
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
            }
            else if (string.Equals(Convert.ToString(json.type), "Mealy"))
            {
                foreach (var state in json.states)
                {
                    IEnumerable<dynamic>? jsonStates = (state.outputs as IEnumerable<dynamic>);
                    if (jsonStates == null)
                    {
                        throw new ArgumentException();
                    }
                    var uniqueOutputs = jsonStates.Select(o => Convert.ToString(o.output)).Distinct();
                    foreach (var output in uniqueOutputs)
                    {
                        states.Add(new State($"{Convert.ToString(state.name)}-{output}", output));
                    }
                }
                foreach (var state in states)
                {
                    IEnumerable<dynamic> JSONStates = (IEnumerable<dynamic>)json.states;
                    IEnumerable<dynamic> transitions = JSONStates.First(s =>
                        Convert.ToString(s.name) == state.Name.Substring(0, state.Name.IndexOf('-'))).transitions;
                    foreach (var transition in transitions)
                    {
                        var nextState = JSONStates.First(s => string.Equals(Convert.ToString(s.name), Convert.ToString(transition.nextstate)));
                        IEnumerable<dynamic> nextstateOutputTable = nextState.outputs;
                        string nextStateOutput = nextstateOutputTable.First(o => string.Equals(Convert.ToString(o.input), Convert.ToString(transition.input))).output;
                        state.Transitions.Add(new StateTransition()
                        {
                            Input = Convert.ToString(transition.input),
                            NextState = states.First(s => string.Equals(s.Name, $"{transition.nextstate}-{nextStateOutput}"))
                        });
                    }
                }
            }
            else
            {
                throw new ArgumentException();
            }

            _currentState = states.First();
        }
        /// <summary>
        /// Transitions the FSM to the next state.
        /// </summary>
        /// <param name="input">The input being used to transition the FSM.</param>
        /// <exception cref="ArgumentException">Throws if the input is not in the FSM's transition table.</exception>
        public void Transition(string input)
        {
            try
            {
                var transition = _currentState.Transitions.First(t => string.Equals(t.Input, input));
                _currentState = transition.NextState;
            }
            catch
            {
                throw new ArgumentException();
            }
        }
        /// <summary>
        /// Simulates the FSM using a string of inputs, encoded in JSON.
        /// </summary>
        /// <param name="inputJSON">The JSON encoding of all the inputs to simulate.</param>
        /// <exception cref="ArgumentException">Throws if the JSON is not in proper format.</exception>
        public void Simulate(string inputJSON)
        {
            dynamic? json = JsonConvert.DeserializeObject(inputJSON);

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
