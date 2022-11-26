using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
public class StateMachine : MonoBehaviour
{
    public State CurrentState { get; private set; }

    public List<State> States;

    public void AddState(string name, State state)
    {
        state.Name = name;
        state.Parent = this;
        States.Add(state);
    }

    public void ChangeState(string name)
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
        }

        States.Find(s => s.Name == name).Enter();
    }

    public void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.Update();
        }
    }

}

