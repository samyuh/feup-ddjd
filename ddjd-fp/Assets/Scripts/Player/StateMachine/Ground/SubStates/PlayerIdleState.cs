using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState {
    public PlayerIdleState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory) : 
    base (currentContext, playerStateFactory, stateFactory) { }

    public override void EnterState() {
        base.EnterState();
        _targetVelocity = 0f;
    }

    public override void ExitState() {
        base.ExitState();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        if(_context.PlayerInput.Movement != Vector2.zero) {
			_stateMachine.ChangeState(_factory.WalkState);
		} 
    }
}