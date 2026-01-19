using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();

    protected BaseState<EState> _currentState;

    protected bool IsTranstionToState = false;

    void Start()
    {
        _currentState.EnterState();
    }

    void Update()
    {
        EState nextStateKey = _currentState.GetNextState();
        if (!IsTranstionToState && nextStateKey.Equals(_currentState.StateKey))
        {
            _currentState.UpdateState();
        }
        else
        {
            TranstionToState(nextStateKey);
        }
    }

    void TranstionToState(EState StateKey)
    {
        IsTranstionToState = true;
        _currentState.ExitState();
        _currentState = States[StateKey];
        _currentState.EnterState();
        IsTranstionToState = false;
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