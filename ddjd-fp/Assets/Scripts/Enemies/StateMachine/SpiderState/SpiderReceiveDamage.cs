using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpiderReceiveDamage : EnemyState {
    public SpiderReceiveDamage(SpiderController context, EnemyStateMachine stateMachine,  EnemyFactory stateFactory) : 
    base (context, stateMachine, stateFactory) { }

    public override void EnterState() { 
        base.EnterState();
    }

    public override void ExitState() {
        base.ExitState();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
    }
}