using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossAttack : BossState {
    private float _elapsedTime;
    private bool _dealDamage;
    public static int _currentAttackIndex = 0;
    
    public BossAttack(BossController context, BossStateMachine stateMachine,  BossFactory stateFactory) : 
    base (context, stateMachine, stateFactory) { }

    public override void EnterState() { 
        base.EnterState();
        //_context.Animator.SetBool("LightAttack", true);
        _context.Animator.SetBool("Attack" + _currentAttackIndex.ToString(), true);
        
        _dealDamage = true;
        _elapsedTime = 0f;
        speed = 0f;
    }

    public override void ExitState() {
        base.ExitState();

        //_context.Animator.SetBool("LightAttack", false);
        _context.Animator.SetBool("Attack" + _currentAttackIndex.ToString(), false);
        _currentAttackIndex = (_currentAttackIndex + 1) % 3;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > 0.3f) {
            if (_dealDamage) DealDamage();

            if (_context.Animator.GetCurrentAnimatorStateInfo(0).length < _context.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + 0.5f) {
                _stateMachine.ChangeState(_stateFactory.BossWalk);
            }
        }
    }

    protected void DealDamage() {
        _dealDamage = false;
        _target.SendMessage("ApplyDamage", 50);
    }
}