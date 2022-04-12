using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerState {
    public PlayerWalkState(Player currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {}

    public override void EnterState() {
        Debug.Log("Player is walking yayy");
    }

    public override void UpdateState() {
        CheckSwitchState();
    }

    public override void ExitState() {}

	public override void CheckSwitchState() {
        if (false) {
            //SwitchState(_factory.Idle());
        } 
    }

	public override void InitializeSubState() {}
}
