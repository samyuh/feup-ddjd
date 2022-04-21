using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerAirState {
    public PlayerFallingState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory) : 
    base (currentContext, playerStateFactory, stateFactory) { }

    public override void EnterState() {
        base.EnterState();
        _targetVelocity = 5f;
     }

    public override void ExitState() {
        base.ExitState();
     }

    public override void LogicUpdate() {
        base.LogicUpdate();
    }
}
