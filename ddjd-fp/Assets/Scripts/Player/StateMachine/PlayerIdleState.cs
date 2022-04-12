using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState {
    public PlayerIdleState(Player currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {}

    public override void EnterState() {
        Debug.Log("Player is idling yayy");
    }

    public override void UpdateState() {
        CheckSwitchState();
    }

    public override void ExitState() {}

	public override void CheckSwitchState() {
        if (false) {
            //SwitchState(_factory.Walk());
        } 
    }

	public override void InitializeSubState() {}
}