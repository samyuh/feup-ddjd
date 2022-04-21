using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState {
    public PlayerIdleState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory) : 
    base (currentContext, playerStateFactory, stateFactory) { }

    public override void EnterState() {
        base.EnterState();
        Debug.Log("Sou Idle e sou lindo");
        _targetVelocity = 0f;
    }

    public override void ExitState() {
        base.ExitState();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        if(_context.PlayerInput.Movement != Vector2.zero) {
            Debug.Log("Andar é uma necessidade");
			_stateMachine.ChangeState(_factory.WalkState);
		} 
    }
}