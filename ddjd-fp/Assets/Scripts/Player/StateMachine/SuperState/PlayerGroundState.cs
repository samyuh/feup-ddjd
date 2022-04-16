using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState {
    public PlayerGroundState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory) 
    : base (currentContext, playerStateFactory, stateFactory) { }

    public override void EnterState() {

     }

    public override void ExitState() {

    }

    public override void LogicUpdate() {
        if (!_context.GroundedCheck()) {
            _stateMachine.ChangeState(_factory.AirState);
        }

        if (_context.VerticalVelocity < 0.0f) {
            _context.VerticalVelocity = -2f;
        }

        if (_context.VerticalVelocity < _context.TerminalVelocity) {
            _context.VerticalVelocity += _context.PlayerSettings.Gravity * Time.deltaTime;
        }

        if (_context.PlayerInput.jump)  {
            _stateMachine.ChangeState(_factory.JumpState);
        } else if (_context.PlayerInput.meleeAttack) {
            _stateMachine.ChangeState(_factory.AttackState);
        }
    }
}
