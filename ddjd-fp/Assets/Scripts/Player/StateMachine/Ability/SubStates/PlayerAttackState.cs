using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackGroundState : PlayerAbilityState {
    public PlayerAttackGroundState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory) : 
    base (currentContext, playerStateFactory, stateFactory) {}

    public override void EnterState() {
        base.EnterState();

        _context.Animator.SetBool("Attack", true);
    }

    public override void ExitState() {
        base.ExitState();
        
        _context.Animator.SetBool("Attack", false);
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        if (_context.Animator.GetCurrentAnimatorStateInfo(0).length < _context.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + 0.5f) {
            _stateMachine.ChangeState(_factory.IdleState);
        }
    }
}