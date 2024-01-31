using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine
{
    private Dictionary<string, AIState> states = new Dictionary<string, AIState>();
    public AIState CurrentState { get; private set; } = null; 

    public void Update()
    {
        CurrentState?.OnUpdate();
    }

    public void AddState(string name, AIState state)
    {
        Debug.Assert(!states.ContainsKey(name), "State Machine already contains state " + name);

        states[name] = state;
    }

    public void SetState(string name)
    {
        Debug.Assert(states.ContainsKey(name), "State Machine does not contain state " + name);

        AIState newState = states[name];

        //don't re-enter state
        if (newState == CurrentState) { return; }
        //exit current state 
        CurrentState?.OnExit();
        //set next state
        CurrentState = newState;
        //enter new state
        CurrentState?.OnEnter();
    }
}
