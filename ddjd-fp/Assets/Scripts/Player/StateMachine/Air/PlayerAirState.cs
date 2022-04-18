using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState {
    public PlayerAirState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory) : 
    base (currentContext, playerStateFactory, stateFactory) { }

    public override void EnterState() { }

    public override void ExitState() { }

    public override void LogicUpdate() {
        if(_context.PlayerInput.move != Vector2.zero) {
			Move(5);
		} else {
            Move(0);
        }

        if (_context.Data.VerticalVelocity < _context.Data.TerminalVelocity) {
            _context.Data.VerticalVelocity += _context.Data.Gravity * Time.deltaTime;
        }

        if (GroundedCheck()) {
            _stateMachine.ChangeState(_factory.IdleState);
        } 
    }
}
