using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerGroundState {
    public PlayerWalkState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory) 
    : base (currentContext, playerStateFactory, stateFactory) {
    }

    public override void EnterState() {
        base.EnterState();
        _context.TargetSpeed = 10f;
        _context.Animator.SetBool("Walk", true);
    }  

    public override void ExitState() {
        _context.Animator.SetBool("Walk", false);
        _context.TargetSpeed = 0f;
        base.ExitState();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        if(_context.PlayerInput.move != Vector2.zero) {
            _context.Move();
		} else {
            _stateMachine.ChangeState(_factory.IdleState);
        }
    }
}
