using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackGroundState : PlayerAbilityState {
    public float _elapsedTime;
    private bool _dealDamage;
    private bool _closeUp;

    public PlayerAttackGroundState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory) : 
    base (currentContext, playerStateFactory, stateFactory) {}

    public override void EnterState() {
        base.EnterState();

        _closeUp = true;
        _dealDamage = true;
        _elapsedTime = 0f;
        _context.Controller.enabled = false;
        _context.Animator.SetBool("Attack", true);
        
    }

    public override void ExitState() {
        base.ExitState();
        
        _context.Controller.enabled = true;
        _context.Animator.SetBool("Attack", false);
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > 0.5f) {
            if (_dealDamage) DealDamage();

            if (_context.Animator.GetCurrentAnimatorStateInfo(0).length < _context.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + 0.5f) {
                _stateMachine.ChangeState(_factory.IdleState);
            }
        } else if (_elapsedTime > 0.25f) {
            if (_closeUp) CloseUp();
        }
    }

    private void CloseUp() {
        _closeUp = false;
        Vector3 spherePosition = new Vector3(_context.transform.position.x + 2f * _context.transform.TransformDirection(Vector3.forward).x, _context.transform.position.y + 0.3f, 
                                        _context.transform.position.z + 2f * _context.transform.TransformDirection(Vector3.forward).z);
        Collider[] hitColliders = Physics.OverlapSphere(spherePosition, 1.3f);

        foreach (var hitCollider in hitColliders) {
            if (hitCollider.gameObject.tag == "Enemy") {

                Vector3 newPos = new Vector3(hitCollider.gameObject.transform.position.x - 1f, _context.Controller.transform.position.y, hitCollider.gameObject.transform.position.z);
                _context.Controller.transform.position = newPos;
            };
        }
    }

    private void DealDamage() {
        _dealDamage = false;
        Vector3 spherePosition = new Vector3(_context.transform.position.x + 0.616f * _context.transform.TransformDirection(Vector3.forward).x, _context.transform.position.y + 0.3f, 
                                        _context.transform.position.z + 0.616f * _context.transform.TransformDirection(Vector3.forward).z);

        Collider[] hitColliders = Physics.OverlapSphere(spherePosition, 0.2f);
        foreach (var hitCollider in hitColliders) {
            if (hitCollider.gameObject.tag == "Enemy") {
                hitCollider.gameObject.SendMessage("ApplyDamage", 30);
            };
        }
    }
}