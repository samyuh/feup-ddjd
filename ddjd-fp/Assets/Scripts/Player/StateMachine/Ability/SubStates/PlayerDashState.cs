using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState {
    private float _elapsedTime;

    public PlayerDashState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory) : 
    base (currentContext, playerStateFactory, stateFactory) { }

    public override void EnterState() {
        base.EnterState();
        _elapsedTime = 0f;
        _context.Animator.SetBool("Dash", true);
    }  

    public override void ExitState() {
        base.ExitState();
        _context.Animator.SetBool("Dash", false);
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        Vector3 targetDirection = Quaternion.Euler(0.0f, _context.Data.TargetRotation, 0.0f) * Vector3.forward;
        _context.Controller.Move(targetDirection * 12f * Time.deltaTime);

        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > 0.5f) {
            _stateMachine.ChangeState(_factory.IdleState);
        } 
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
