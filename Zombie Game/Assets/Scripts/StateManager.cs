using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState,BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();
    
    protected  BaseState<EState> _currentState;
    
    void State(){}
    void Update(){}
    void OnTriggerEnter(Collider other){}
    void OnTriggerStay(Collider other){}
    void OnTriggerExit(Collider other){}
    
}
