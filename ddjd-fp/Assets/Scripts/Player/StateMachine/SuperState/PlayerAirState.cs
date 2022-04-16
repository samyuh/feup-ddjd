using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState {
    public PlayerAirState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory) 
    : base (currentContext, playerStateFactory, stateFactory) { }

    public override void EnterState() { }

    public override void ExitState() {
        _context.TargetSpeed = 0f;
    }

    public override void LogicUpdate() {
        if (_context.GroundedCheck()) {
            _stateMachine.ChangeState(_factory.IdleState);
        } 
        if(_context.PlayerInput.move != Vector2.zero) {
            _context.TargetSpeed = 7.5f;
			_context.Move();
		} else {
            _context.TargetSpeed = 0f;
        }
        if (_context.VerticalVelocity < _context.TerminalVelocity) {
            _context.VerticalVelocity += _context.PlayerSettings.Gravity * Time.deltaTime;
        }
    }
}
