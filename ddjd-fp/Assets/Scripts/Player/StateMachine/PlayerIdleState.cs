using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState {
    public PlayerIdleState(Player currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {}

    public override void EnterState() {
        _context.TargetSpeed = 0f;
        //Debug.Log("Player is idle yayy");
    }

    public override void ExitState() {
        //Debug.Log("Player stoped idle");
    }

    public override void UpdateState() {
        CheckSwitchState();
    }

	public override void CheckSwitchState() {
        if (_context.PlayerInput.move != Vector2.zero) {
            SwitchState(_factory.Walk());
        } 
    }
}