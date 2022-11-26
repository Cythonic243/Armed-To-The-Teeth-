// State be generated by StateMachine:
//
using UnityEngine;
using System;

public class State : MonoBehaviour
{
    public string Name;
    public StateMachine Parent { get; set; }

    public virtual void Enter() 
    {
        if (EnterAction != null)
        {
            EnterAction();
        }
    }

    public virtual void Update() 
    {
        if (UpdateAction != null)
        {
            UpdateAction();
        }
    }
    public virtual void Exit() 
    {
        if (ExitAction != null)
        {
            ExitAction();
        }
    }

    public Action EnterAction { get; set; }
    public Action UpdateAction { get; set; }
    public Action ExitAction { get; set; }
}
