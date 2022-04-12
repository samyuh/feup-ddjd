using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState {
    public PlayerGroundedState(Player currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {
        _isRootState = true;
        InitializeSubState();
    }

    public override void EnterState() {
        Debug.Log("Grounded");
    }

    public override void ExitState() {
        Debug.Log("Player left ground  and now is");
    }

    public override void UpdateState() {
        if (_context.VerticalVelocity < 0.0f) {
            _context.VerticalVelocity = -2f;
        }

        if (_context.VerticalVelocity < _context.TerminalVelocity) {
            _context.VerticalVelocity += _context.PlayerSettings.Gravity * Time.deltaTime;
        }

        if (_context.PlayerInput.jump)  {
            _context.VerticalVelocity = Mathf.Sqrt(_context.PlayerSettings.JumpHeight * -2f * _context.PlayerSettings.Gravity);
        }

        
        CheckSwitchState();
    }

	public override void CheckSwitchState() {
        if (!_context.GroundedCheck()) {
            SwitchState(_factory.Jump());
        }
    }

	protected override void InitializeSubState() {
        if (_context.PlayerInput.move != Vector2.zero) {
            SetSubState(_factory.Walk());
        } else {
            SetSubState(_factory.Idle());
        }
    }
}
