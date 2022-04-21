using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractState : PlayerAbilityState {
    private float _elapsedTime;
    public PlayerInteractState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory) : 
    base (currentContext, playerStateFactory, stateFactory) { }

    public override void EnterState() {
        base.EnterState();
        if(_context.InteractableItem != null) {
            _context.Animator.SetBool("Interact", true);
        } else {
            _stateMachine.ChangeState(_factory.IdleState);
        }
        _elapsedTime = 0f;
    }

    public override void ExitState() {
        base.ExitState();
        _context.Animator.SetBool("Interact", false);
    }   

    public override void LogicUpdate() {
        base.LogicUpdate();

        if(_elapsedTime > 0.5f) {
            HandleObject();
            _stateMachine.ChangeState(_factory.IdleState);
        }

        _elapsedTime += Time.deltaTime;
    }

    private void HandleObject() {
        if (_context.InteractableItem.tag == "Health") {
            Events.OnCatchHealthPlant.Invoke();
            _context.DestroyObject(_context.InteractableItem);
        } else if (_context.InteractableItem.tag == "Crystal") {
            Events.OnCatchCrystal.Invoke();
            _context.DestroyObject(_context.InteractableItem);
        }    
    }
}