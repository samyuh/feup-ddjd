using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState {
    public PlayerJumpState(Player currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {
        _isSuperState = true;
        InitializeSubState();
    }

    public override void EnterState() {
        Debug.Log("Jumping");
    }

    public override void ExitState() {
        Debug.Log("Player left Jumping and now is");
    }

    public override void UpdateState() {
        if (_context.VerticalVelocity < _context.TerminalVelocity) {
            _context.VerticalVelocity += _context.PlayerSettings.Gravity * Time.deltaTime;
        }

        _context.PlayerInput.jump = false;

        CheckSwitchState();
    }

	public override void CheckSwitchState() {
        if (_context.GroundedCheck()) {
            SwitchState(_factory.Grounded());
        }
    }

	protected override void InitializeSubState() {
        if  (_context.PlayerInput.move != Vector2.zero) {
            SetSubState(_factory.Walk());
        } else {
            SetSubState(_factory.Idle());
        }
    }
}
