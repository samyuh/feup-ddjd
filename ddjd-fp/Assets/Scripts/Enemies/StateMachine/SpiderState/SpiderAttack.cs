using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpiderAttack : EnemyState {
    public SpiderAttack(SpiderController context, EnemyStateMachine stateMachine,  EnemyFactory stateFactory) : 
    base (context, stateMachine, stateFactory) { }

    public override void EnterState() { 
        base.EnterState();

        speed = 0f;
    }

    public override void ExitState() {
        base.ExitState();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
    }
}