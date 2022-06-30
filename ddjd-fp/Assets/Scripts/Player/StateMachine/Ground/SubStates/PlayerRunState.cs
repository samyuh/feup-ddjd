using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunState : PlayerGroundState {
    private int _walkSoundPeriod;
    private long _startTicks;

    public PlayerRunState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory) : 
    base (currentContext, playerStateFactory, stateFactory) { }

    public override void EnterState() {
        base.EnterState();
        _walkSoundPeriod = 200;
        _startTicks = DateTime.Now.Ticks;
        _targetVelocity = 10f;
        _context.Animator.SetBool("Run", true);

        _context.PlayerInput.PlayerRun.performed += OnRun;
        _context.PlayerInput.PlayerDash.performed += OnDash;
    }

    public override void ExitState() {
        base.ExitState();
        _context.Animator.SetBool("Run", false);

        _context.PlayerInput.PlayerRun.performed -= OnRun;
        _context.PlayerInput.PlayerDash.performed -= OnDash;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        if ((DateTime.Now.Ticks - _startTicks) / 10000 > _walkSoundPeriod) {
            FMODUnity.RuntimeManager.PlayOneShot(_context.WalkSoundEvent);
            _startTicks = DateTime.Now.Ticks;
        }

        if (_context.PlayerInput.Movement == Vector2.zero) {
            _stateMachine.ChangeState(_factory.IdleState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }

    protected virtual void OnRun(InputAction.CallbackContext context) {
        _stateMachine.ChangeState(_factory.WalkState);
    }

    protected virtual void OnDash(InputAction.CallbackContext context) {
        _stateMachine.ChangeState(_factory.DashState);
    }
}