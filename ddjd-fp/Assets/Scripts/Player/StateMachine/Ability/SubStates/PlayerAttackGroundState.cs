using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackGroundState : PlayerAbilityState {
    public float _elapsedTime;
    public PlayerAttackGroundState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory) : 
    base (currentContext, playerStateFactory, stateFactory) {}

    public override void EnterState() {
        base.EnterState();
        _elapsedTime = 0f;
        _context.Animator.SetBool("Attack", true);
        DealDamage();
    }

    public override void ExitState() {
        base.ExitState();
        
        _context.Animator.SetBool("Attack", false);
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        _elapsedTime += Time.deltaTime;
        if(_elapsedTime > 0.5f) {
            if (_context.Animator.GetCurrentAnimatorStateInfo(0).length < _context.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + 0.5f) {
                _stateMachine.ChangeState(_factory.IdleState);
            }
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