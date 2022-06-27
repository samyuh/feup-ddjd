using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState {
    private float _elapsedTime;

    public PlayerDashState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory) : 
    base (currentContext, playerStateFactory, stateFactory) { }

    public override void EnterState() {
        base.EnterState();
        _context.Controller.enabled = false;
        _elapsedTime = 0f;
        _context.Animator.SetBool("Dash", true);

        // Dash sound
        FMODUnity.RuntimeManager.PlayOneShot(_context.DodgeSoundEvent);
    }  

    public override void ExitState() {
        base.ExitState();
         _context.Controller.enabled = true;
        _context.Animator.SetBool("Dash", false);
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > 0.37f) {
            //Vector3 targetDirection = Quaternion.Euler(0.0f, _context.Data.TargetRotation, 0.0f) * Vector3.forward;
            //_context.Controller.transform.position = _context.Controller.transform.position + targetDirection * 2f;

            _stateMachine.ChangeState(_factory.IdleState);
        }

        else {
            Vector3 targetDirection = Quaternion.Euler(0.0f, _context.Data.TargetRotation, 0.0f) * Vector3.forward;
            _context.Controller.transform.position = _context.Controller.transform.position + (targetDirection * (float)(_elapsedTime / 0.37) * 0.5f);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
