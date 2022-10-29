using System;
using MooreMachineSimulator.Models;

MooreMachine FSM = new MooreMachine(string.Empty);

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