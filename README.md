# MooreMachineSimulator
This solution allows for the creation of Moore Machines and Mealy Machines with defined outputs and state transitions. Mealy Machines are converted to Moore Machines before simulating. 

# Usage
To use the simulator, first define a Mealy or Moore Machine the proper definition file, then modify the application file to use the correct JSON definition file. Then, modify the Inputs JSON file to define a set of inputs to simulate the FSM with.

# JSON
Moore Machines are defined in the format:
```
{
  "type" : "Moore",
  "states": [
    {
      "name": name,
      "output": output,
      "transitions": [
        {
          "input": input
          "nextstate": next state name
        }
      ]
    }
  ]
}
```

Mealy Machines are defined in the format:
```
{
  "type" : "Mealy",
  "states": [
    {
      "name": name,
      "outputs": [
        {
          "input": input
          "output": corresponding output
        }
      ],
      "transitions": [
        {
          "input": input
          "nextstate": next state name
        }
      ]
    }
  ]
}
```

Inputs are defined in the format:
```
{
  "inputs": [
    input1,
    input2
  ]
}
```
