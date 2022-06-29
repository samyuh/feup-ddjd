using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerAirState {
    private float _elapsedTime;

    public PlayerFallingState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory) : 
    base (currentContext, playerStateFactory, stateFactory) { }

    public override void EnterState() {
        base.EnterState();
        _targetVelocity = 5f;
     }

    public override void ExitState() {
        base.ExitState();

        _context.Animator.SetBool("Jump", false);
     }

    public override void LogicUpdate() {
        base.LogicUpdate();
        int fallTime = 4;

        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > fallTime) {
            _context.invokeDeath();
        }
    }
}
