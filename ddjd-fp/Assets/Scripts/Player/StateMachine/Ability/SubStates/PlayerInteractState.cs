using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractState : PlayerAbilityState {
    public PlayerInteractState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory) : 
    base (currentContext, playerStateFactory, stateFactory) { }

    public override void EnterState() {
        base.EnterState();

        HandleObject();
    }

    public override void ExitState() {
        base.ExitState();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
    }

    private void HandleObject() {
        if(_context.InteractableItem != null) {
            if (_context.InteractableItem.tag == "Health" && _context.Data.HealthCrystal < _context.Data.MaxHealthCrystal) {
                Events.OnCatchHealthCrystal.Invoke();
                _context.DestroyObject(_context.InteractableItem);
                _context.GetItem(0);
            } else if (_context.InteractableItem.tag == "Crystal") {
                Events.OnCatchManaCrystal.Invoke();
                _context.DestroyObject(_context.InteractableItem);
                _context.GetItem(1);
            }    
        }

        _stateMachine.ChangeState(_factory.IdleState);
    }
}