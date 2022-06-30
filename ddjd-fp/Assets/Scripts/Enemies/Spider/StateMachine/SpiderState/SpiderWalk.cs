using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpiderWalk : SpiderState {
    public SpiderWalk(SpiderController context, SpiderStateMachine stateMachine,  SpiderFactory stateFactory) : 
    base (context, stateMachine, stateFactory) { }

    public override void EnterState() { 
        base.EnterState();
        _context.Animator.SetBool("Run", true);
    }

    public override void ExitState() {
        base.ExitState();

        _context.Animator.SetBool("Run", false);
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();

        if (Physics.Raycast(_context.transform.position, _target.transform.position - _context.transform.position, out RaycastHit hit, maxDistance, mask)) {
            float distance = hit.distance;

            if (distance > followDistance)  {
                Accelerate();
            }
            else {
                Decelerate();

                if (speed == 0f) {
                    _stateMachine.ChangeState(_stateFactory.SpiderAttack);
                }
            }
        }
    }
}
