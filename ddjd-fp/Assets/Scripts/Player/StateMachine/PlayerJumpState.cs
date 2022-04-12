using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState {
    public PlayerJumpState(Player currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {
        _isRootState = true;
        InitializeSubState();
    }

    public override void EnterState() {
        Debug.Log("Player is flyiing yayy");
    }

    public override void UpdateState() {
        _context.JumpTimeoutDelta = _context.JumpTimeout;

        if (_context.FallTimeoutDelta >= 0.0f) {
            _context.FallTimeoutDelta -= Time.deltaTime;
        }

        _context.PlayerInput.jump = false;

        if (_context.VerticalVelocity < _context.TerminalVelocity) {
            _context.VerticalVelocity += _context.Gravity * Time.deltaTime;
        }

        CheckSwitchState();
        _context.Move();
    }

    public override void ExitState() {}

	public override void CheckSwitchState() {
        if (_context.GroundedCheck()) {
            SwitchState(_factory.Grounded());
        }
    }

	public override void InitializeSubState() {}
}
