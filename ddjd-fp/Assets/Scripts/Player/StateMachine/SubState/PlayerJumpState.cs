using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState {
    public PlayerJumpState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory)  : 
    base (currentContext, playerStateFactory, stateFactory) { }

    public override void EnterState() {
        base.EnterState();
        _context.PlayerInput.jump = false;
        PerformJump();
    }

    public override void ExitState() {
        _context.PlayerInput.jump = false;
        base.ExitState();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

		_stateMachine.ChangeState(_factory.AirState);
    }

    private void PerformJump() {
        _context.VerticalVelocity = Mathf.Sqrt(_context.PlayerSettings.JumpHeight * -2f * _context.PlayerSettings.Gravity);
        _context.Move(0);
    }
}