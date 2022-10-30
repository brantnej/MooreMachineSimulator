using System;
using MooreMachineSimulator.Models;

using (StreamReader r = new StreamReader("DefinitionMoore.json"))
using (StreamReader i = new StreamReader("Inputs.json"))
{
    string definitionJSON = r.ReadToEnd();
    string inputsJSON = i.ReadToEnd();
    MooreMachine FSM = new MooreMachine(definitionJSON);
    FSM.Simulate(inputsJSON);
}

