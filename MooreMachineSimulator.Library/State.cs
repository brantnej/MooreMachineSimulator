using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooreMachineSimulator.Models
{
    public class State
    {
        public State(string name, string output)
        {
            Name = name;
            Output = output;
            Transitions = new List<StateTransition>();
        }
        public string Name { get; }
        public string Output { get; }
        public List<StateTransition> Transitions { get; set; }
    }
}
