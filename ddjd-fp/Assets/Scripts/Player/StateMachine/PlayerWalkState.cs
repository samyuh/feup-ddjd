using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerState {
    public PlayerWalkState(Player currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {}

    public override void EnterState() {
        _context.TargetSpeed = _context.MoveSpeed;

       // Debug.Log("Player is walking yayy");
    }

    public override void ExitState() {
       // Debug.Log("Player stoped walking");
    }

    public override void UpdateState() {
        _context.Move();

        CheckSwitchState();
    }

	public override void CheckSwitchState() {
        if (_context.PlayerInput.move == Vector2.zero) {
            SwitchState(_factory.Idle());
        } 
    }
}
