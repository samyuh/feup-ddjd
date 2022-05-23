using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackGroundState : PlayerAbilityState {
    public float _elapsedTime;
    public static int _currentAttackIndex = 0;

    private bool _dealDamage;
    private bool _closeUp;
    private bool _movingTowards;

    private Vector3 _startPosition;
    private Vector3 _destination;

    public PlayerAttackGroundState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory) : 
    base (currentContext, playerStateFactory, stateFactory) {}

    public override void EnterState() {
        base.EnterState();

        _movingTowards = false;
        _closeUp = true;
        _dealDamage = true;
        _elapsedTime = 0f;
        Debug.Log("Attack" + _currentAttackIndex.ToString());
        _context.Animator.SetBool("Attack" + _currentAttackIndex.ToString(), true);
    }

    public override void ExitState() {
        base.ExitState();
        
        _context.Animator.SetBool("Attack" + _currentAttackIndex.ToString(), false);
        _currentAttackIndex = (_currentAttackIndex + 1) % 3;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > 0.5f) {
            if (_dealDamage) DealDamage();

            if (_context.Animator.GetCurrentAnimatorStateInfo(0).length < _context.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + 0.5f) {
                _stateMachine.ChangeState(_factory.IdleState);
            }
        } else if (_elapsedTime > 0.20f) {
            if (_closeUp) CloseUp();

            if (_movingTowards) { 
                float distanceDisplacement = _elapsedTime - 0.25f;
                float interpolationRatio = distanceDisplacement / 0.23f;

                Vector3 targetPos = Vector3.Lerp(_startPosition, _destination, interpolationRatio);
                _context.Controller.Move(targetPos - _context.Controller.transform.position);
            }
        }
    }

    private void CloseUp() {
        _closeUp = false;

        // TODO: 
        // Do this with raycast instead?
        Vector3 spherePosition = new Vector3(_context.transform.position.x + 2f * _context.transform.TransformDirection(Vector3.forward).x, _context.transform.position.y + 0.2f, 
                                        _context.transform.position.z + 2f * _context.transform.TransformDirection(Vector3.forward).z);
        Collider[] hitColliders = Physics.OverlapSphere(spherePosition, 1.3f);

        foreach (var hitCollider in hitColliders) {
            if (hitCollider.gameObject.tag == "Enemy") {
                _startPosition = _context.Controller.transform.position;
                _destination = new Vector3(hitCollider.gameObject.transform.position.x - 1f, _context.Controller.transform.position.y, hitCollider.gameObject.transform.position.z);
                _movingTowards = true;
            };
        }
    }

    private void DealDamage() {
        _dealDamage = false;
        Vector3 spherePosition = new Vector3(_context.transform.position.x + 0.616f * _context.transform.TransformDirection(Vector3.forward).x, _context.transform.position.y - 0.1f, 
                                        _context.transform.position.z + 0.616f * _context.transform.TransformDirection(Vector3.forward).z);

        Collider[] hitColliders = Physics.OverlapSphere(spherePosition, 0.2f);
        foreach (var hitCollider in hitColliders) {
            
            if (hitCollider.gameObject.tag == "Enemy") {
                hitCollider.gameObject.SendMessage("ApplyDamage", 30);
            }
            else if (hitCollider.gameObject.tag == "PuzzleCube") {
                hitCollider.gameObject.SendMessage("MoveRequest", _context.transform.position);
            }
        }
    }
}