using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState,BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();
    
    protected  BaseState<EState> _currentState;

    void State()
    {
        _currentState.EnterState();
    }

    void Update()
    {
        _currentState.UpdateState();
    }

    void OnTriggerEnter(Collider other)
    {
        _currentState.OnTriggerEnter(other);
    }

    void OnTriggerStay(Collider other)
    {
        _currentState.OnTriggerStay(other);
    }

    void OnTriggerExit(Collider other)
    {
        _currentState.OnTriggerExit(other);
    }
    
}
