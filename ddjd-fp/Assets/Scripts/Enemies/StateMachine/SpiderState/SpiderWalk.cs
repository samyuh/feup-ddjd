using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpiderWalk : EnemyState {
    public SpiderWalk(SpiderController context, EnemyStateMachine stateMachine,  EnemyFactory stateFactory) : 
    base (context, stateMachine, stateFactory) { }

    public override void EnterState() { 
        base.EnterState();

        _context.Animator.SetBool("Run", true);

        // Running Sound
        FMODUnity.RuntimeManager.PlayOneShot(_context.RunSoundEvent);

    }

    public override void ExitState() {
        base.ExitState();

        _context.Animator.SetBool("Run", false);
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

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
