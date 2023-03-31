using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public Dictionary<State, List<KeyValuePair<Transition, State>>> stateTransitions
        = new Dictionary<State, List<KeyValuePair<Transition, State>>>();

    public State currentState;

	public void Update()
	{
		if (currentState == null) return;

		var transitions = stateTransitions[currentState];
		foreach (var transition in transitions)
		{
			if (transition.Key.ToTransition())
			{
				SetState(transition.Value);
				break;
			}
		}
		currentState.OnUpdate();
	}

	public void SetState(State newState)
	{
		if (newState == null || newState == currentState) return;

		currentState?.OnExit();
		currentState = newState;
		newState.OnEnter();
	}

	public void AddState(State state)
	{
		if (!stateTransitions.ContainsKey(state))
		{
			stateTransitions[state] = new List<KeyValuePair<Transition, State>>();
		}
	}

	public void AddTransition(State stateFrom, Transition transition, State stateTo)
	{
		if (stateTransitions.ContainsKey(stateFrom))
		{
			var transitions = stateTransitions[stateFrom];
			transitions.Add(new KeyValuePair<Transition, State>(transition, stateTo));
		}
	}
	public void AddTransition(string stateFrom, Transition transition, string stateTo)
	{
		if (stateTransitions.ContainsKey(StateFromName(stateFrom)))
		{
			var transitions = stateTransitions[StateFromName(stateFrom)];
			transitions.Add(new KeyValuePair<Transition, State>(transition, StateFromName(stateTo)));
		}
	}

	public State StateFromName(string name)
	{
		foreach (var state in stateTransitions)
		{
			if (string.Equals(state.Key.name, name, System.StringComparison.OrdinalIgnoreCase))
			{
				return state.Key;
			}
		}

		return null;
	}

	public string GetStateName()
	{
		return (currentState == null) ? null : currentState.name;
	}
}
