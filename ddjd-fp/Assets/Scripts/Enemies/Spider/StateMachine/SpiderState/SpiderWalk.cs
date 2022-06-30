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

        Debug.Log("walk");
        _context.Animator.SetBool("Run", true);

        // Running Sound
        //FMOD.Studio.EventInstance coiso = FMODUnity.RuntimeManager.CreateInstance("event:/spidermob_run");
        //coiso.start();
    }

    public override void ExitState() {
        base.ExitState();

        _context.Animator.SetBool("Run", false);
        //_context.RunSoundEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        //_context.RunSoundEvent.release();
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();

        if (Physics.Raycast(_context.transform.position, _target.transform.position - _context.transform.position, out RaycastHit hit, maxDistance, mask)) {
            float distance = hit.distance;
            //Debug.Log("here");

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
