using System;
using UnityEngine;

public abstract class State<TEState>  where TEState : Enum
{
    protected State(TEState key) {
        StateKey = key;
        NextStateKey = key;
    }

    public TEState StateKey { get; }
    protected TEState NextStateKey { get; set; }

    public abstract void EnterState();

    public virtual void ExitState() {
        NextStateKey = StateKey;
    }
    public abstract void UpdateState();

    public virtual TEState GetNextState() {
        return NextStateKey;
    }

    public virtual void OnTriggerEnter(Collider other){}

    public virtual void OnTriggerStay(Collider other){}

    public virtual void OnTriggerExit(Collider other){}
}
