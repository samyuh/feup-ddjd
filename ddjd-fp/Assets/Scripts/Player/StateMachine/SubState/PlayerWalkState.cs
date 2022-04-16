using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerGroundState {
    public PlayerWalkState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory) : 
    base (currentContext, playerStateFactory, stateFactory) { }

    public override void EnterState() {
        base.EnterState();
        _context.Animator.SetBool("Walk", true);
    }  

    public override void ExitState() {
        _context.Animator.SetBool("Walk", false);
        base.ExitState();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        if(_context.PlayerInput.move != Vector2.zero) {
            _context.Move(10);
		} else {
            _stateMachine.ChangeState(_factory.IdleState);
        }
    }
}
