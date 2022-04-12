using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState {
    private float _elapsedTime;

    public PlayerAttackState(Player currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {}

    public override void EnterState() {
        _elapsedTime = 0.5f;
        _context.PlayerInput.meleeAttack = false;
        _context.Animator.SetBool("Attack", true);
    }

    public override void ExitState() {
        _context.Animator.SetBool("Attack", false);
    }

    public override void UpdateState() {
        // Deal Damage
        
        // Wait for time to pass
        _elapsedTime -= Time.deltaTime;
        Debug.Log(_elapsedTime);
        if (_context.Animator.GetCurrentAnimatorStateInfo(0).length < _context.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + 0.5f) CheckSwitchState();
    }

	public override void CheckSwitchState() {
        if (_context.PlayerInput.move == Vector2.zero) {
            SwitchState(_factory.Idle());
        } else {
            SwitchState(_factory.Walk());
        }
    }
}
