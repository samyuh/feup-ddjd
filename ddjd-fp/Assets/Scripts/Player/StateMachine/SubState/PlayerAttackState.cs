using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState {
    public PlayerAttackState(Player currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) {}

    public override void EnterState() {
        Debug.Log("Start Attacking");
        _context.Animator.SetBool("Attack", true);
        DealDamage();
    }

    public override void ExitState() {
        _context.PlayerInput.meleeAttack = false;
        _context.Animator.SetBool("Attack", false);
    }

    public override void UpdateState() {
        if (_context.Animator.GetCurrentAnimatorStateInfo(0).length < _context.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + 0.5f) CheckSwitchState();
    }

	public override void CheckSwitchState() {
        if (_context.PlayerInput.move == Vector2.zero) {
            SwitchState(_factory.Idle());
        } else {
            SwitchState(_factory.Walk());
        }
    }

    private void DealDamage() {
        Vector3 spherePosition = new Vector3(_context.transform.position.x + 1.616f * _context.transform.TransformDirection(Vector3.forward).x, 0.515f, 
                                        _context.transform.position.z + 1.616f * _context.transform.TransformDirection(Vector3.forward).z);

        Collider[] hitColliders = Physics.OverlapSphere(spherePosition, 0.9f);
        foreach (var hitCollider in hitColliders) {
            Debug.Log(hitCollider.gameObject.name);
        }
    }
}
