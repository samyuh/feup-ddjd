using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpiderAttack : SpiderState {
    private float _elapsedTime;
    private bool _dealDamage;
    
    public SpiderAttack(SpiderController context, SpiderStateMachine stateMachine,  SpiderFactory stateFactory) : 
    base (context, stateMachine, stateFactory) { }

    public override void EnterState() { 
        base.EnterState();
        _context.Animator.SetBool("LightAttack", true);
        
        _dealDamage = true;
        _elapsedTime = 0f;
        speed = 0f;
    }

    public override void ExitState() {
        base.ExitState();

        _context.Animator.SetBool("LightAttack", false);
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > 0.3f) {
            if (_dealDamage) DealDamage();

            if (_context.Animator.GetCurrentAnimatorStateInfo(0).length < _context.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + 0.5f) {
                _stateMachine.ChangeState(_stateFactory.SpiderWalk);
            }
        }
    }

    protected void DealDamage() {
        _dealDamage = false;
        _target.SendMessage("ApplyDamage", 50);
    }
}