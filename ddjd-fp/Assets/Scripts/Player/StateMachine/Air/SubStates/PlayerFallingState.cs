using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerAirState {
    private float _elapsedTime;
    private float _fallTime;

    public PlayerFallingState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory) : 
    base (currentContext, playerStateFactory, stateFactory) { }

    public override void EnterState() {
        base.EnterState();
        Events.OnFreeFall.Invoke();
        _targetVelocity = 5f;
        _fallTime = 4f;
        _elapsedTime = 0f;
     }

    public override void ExitState() {
        base.ExitState();

        _context.Animator.SetBool("Jump", false);
     }

    public override void LogicUpdate() {
        base.LogicUpdate();

        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > _fallTime) {
            Events.OnDeath.Invoke();
        }
    }
}
