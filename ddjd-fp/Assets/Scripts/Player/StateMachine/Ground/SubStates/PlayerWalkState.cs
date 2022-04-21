using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalkState : PlayerGroundState {
    public PlayerWalkState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory) : 
    base (currentContext, playerStateFactory, stateFactory) { }

    public override void EnterState() {
        base.EnterState();
        _targetVelocity = 5f;
        _context.Animator.SetBool("Walk", true);

        _context.PlayerInput.PlayerRun.performed += OnRun;
        _context.PlayerInput.PlayerDash.performed += OnDash;
    }  

    public override void ExitState() {
        base.ExitState();
        _context.Animator.SetBool("Walk", false);

        _context.PlayerInput.PlayerRun.performed -= OnRun;
        _context.PlayerInput.PlayerDash.performed -= OnDash;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        if (_context.PlayerInput.Movement == Vector2.zero) {
            _stateMachine.ChangeState(_factory.IdleState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }

    protected virtual void OnRun(InputAction.CallbackContext context) {
        _stateMachine.ChangeState(_factory.RunState);
    }

    protected virtual void OnDash(InputAction.CallbackContext context) {
        _stateMachine.ChangeState(_factory.DashState);
    }
}
