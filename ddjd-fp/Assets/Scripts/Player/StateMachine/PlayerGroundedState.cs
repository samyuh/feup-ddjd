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
        Debug.Log("Player is grounded yayy");
    }

    public override void UpdateState() {
         _context.FallTimeoutDelta = _context.FallTimeout;
        if (_context.VerticalVelocity < 0.0f) {
            _context.VerticalVelocity = -2f;
        }

        CheckSwitchState();

        if (_context.JumpTimeoutDelta >= 0.0f) {
            _context.JumpTimeoutDelta -= Time.deltaTime;
        } 

        if (_context.VerticalVelocity < _context.TerminalVelocity) {
            _context.VerticalVelocity += _context.Gravity * Time.deltaTime;
        }
        
        _context.Move();
    }

    public override void ExitState() {}

	public override void CheckSwitchState() {
         if (_context.PlayerInput.jump  && _context.JumpTimeoutDelta <= 0.0f)  {
            _context.VerticalVelocity = Mathf.Sqrt(_context.JumpHeight * -2f * _context.Gravity);
            SwitchState(_factory.Jump());
        }
    }

	public override void InitializeSubState() {
        if (false) {
            SetSubState(_factory.Walk());
        } else {
            //SetSubState(_factory.Idle());
        }
    }
}
