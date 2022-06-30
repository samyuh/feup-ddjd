using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpiderReceiveDamage : SpiderState {
    public SpiderReceiveDamage(SpiderController context, SpiderStateMachine stateMachine,  SpiderFactory stateFactory) : 
    base (context, stateMachine, stateFactory) { }

    public override void EnterState() { 
        base.EnterState();

        _context.Animator.SetBool("Damaged", true);
    }

    public override void ExitState() {
        base.ExitState();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
    }
}