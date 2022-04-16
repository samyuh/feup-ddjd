using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState {
    public PlayerIdleState(Player currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {}

    public override void EnterState() {
        _context.TargetSpeed = 0f;
    }

    public override void ExitState() {}

    public override void UpdateState() {
        CheckSwitchState();
    }

	public override void CheckSwitchState() {
        if (_context.PlayerInput.move != Vector2.zero) {
            SwitchState(_factory.Walk());
        } else if (_context.PlayerInput.meleeAttack) {
            SwitchState(_factory.Attack());
        }
    }
}