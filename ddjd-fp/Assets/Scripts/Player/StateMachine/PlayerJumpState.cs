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
        _context.VerticalVelocity = Mathf.Sqrt(_context.JumpHeight * -2f * _context.Gravity);
    }

    public override void UpdateState() {
        _context.JumpTimeoutDelta = _context.JumpTimeout;

        if (_context.FallTimeoutDelta >= 0.0f) {
            _context.FallTimeoutDelta -= Time.deltaTime;
        }

        if (_context.VerticalVelocity < _context.TerminalVelocity) {
            _context.VerticalVelocity += _context.Gravity * Time.deltaTime;
        }

        _context.PlayerInput.jump = false;
        _context.GroundedCheck();

        CheckSwitchState(); // Check this only after 0.5 seconds
    }

    public override void ExitState() {}

	public override void CheckSwitchState() {
        if (_context.Grounded) {
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
