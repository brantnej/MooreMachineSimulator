using System;
using MooreMachineSimulator.Models;

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

MooreMachine FSM = new MooreMachine(q0);
List<string> Inputs = new List<string>()
{
    "11",
    "10",
    "01",
    "00",
    "10",
    "10",
    "10",
    "10",
    "01",
    "00",
    "01"
};

foreach (string input in Inputs)
{ 
    Console.WriteLine($"Currently on state {FSM.CurrentState.Name}...");
    Console.WriteLine($"Inputting {input}...");
    FSM.Transition(input);
    Console.WriteLine($"FSM is now on state {FSM.CurrentState.Name} and outputting {FSM.CurrentState.Output}");
    Console.WriteLine("---------------------------------------------------------------------------------");
}