using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState {
    public PlayerAirState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory) : 
    base (currentContext, playerStateFactory, stateFactory) { }

    public override void EnterState() {
        base.EnterState();
     }

    public override void ExitState() {
        base.ExitState();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        if (_context.Data.VerticalVelocity < _context.Data.TerminalVelocity) {
            _context.Data.VerticalVelocity += _context.Data.Gravity * Time.deltaTime;
        }

        if (GroundedCheck()) {
            _stateMachine.ChangeState(_factory.IdleState);
            FMODUnity.RuntimeManager.PlayOneShot(_context.JumpFallSoundEvent);
        } 
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
