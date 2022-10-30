using System;
using MooreMachineSimulator.Models;

using (StreamReader r = new StreamReader("Definition.json"))
{
    string json = r.ReadToEnd();
    MooreMachine FSM = new MooreMachine(json);
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

    FSM.Simulate(Inputs);
}

