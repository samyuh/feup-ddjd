using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpiderDeath : EnemyState {
    public float _elapsedTime;

    public SpiderDeath(SpiderController context, EnemyStateMachine stateMachine,  EnemyFactory stateFactory) : 
    base (context, stateMachine, stateFactory) { }

    public override void EnterState() { 
        base.EnterState();

        _elapsedTime = 0f;
        _context.Animator.SetBool("Death", true);
    }

    public override void ExitState() {
        base.ExitState();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > 5f) {
             _context.Destroy();
        } else if (_elapsedTime > 0.5f) {
            _context.Animator.SetBool("Death", false);
        } 
    }
}