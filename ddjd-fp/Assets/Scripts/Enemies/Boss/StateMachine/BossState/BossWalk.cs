using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossWalk : BossState {
    private float _elapsedTime;

    public BossWalk(BossController context, BossStateMachine stateMachine,  BossFactory stateFactory) : 
    base (context, stateMachine, stateFactory) { }

    public override void EnterState() { 
        base.EnterState();

        _elapsedTime = 0f;
        _context.Animator.SetBool("Run", true);
    }

    public override void ExitState() {
        base.ExitState();

        _context.Animator.SetBool("Run", false);
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
        _elapsedTime += Time.deltaTime;
        if (Physics.Raycast(_context.transform.position, _target.transform.position - _context.transform.position, out RaycastHit hit, maxDistance, mask)) {
            float distance = hit.distance;

            if (distance > followDistance)  {
                Accelerate();
            } else {
                Decelerate();

                if (speed == 0f && _elapsedTime > 0.4f) {
                    if(_context.Projectile != null) {
                        if (Random.Range(0f, 1f) > 0.45f) {
                            _stateMachine.ChangeState(_stateFactory.BossAttack);
                        } else {
                            _stateMachine.ChangeState(_stateFactory.BossSpecialAttack);
                        }
                    } else {
                        _stateMachine.ChangeState(_stateFactory.BossAttack);
                    }
                    
                }
            }
        }
    }
}
