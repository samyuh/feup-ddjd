using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractState : PlayerAbilityState {
    public PlayerInteractState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory) : 
    base (currentContext, playerStateFactory, stateFactory) { }
    bool interactionExecuted = false;

    public override void EnterState() {
        base.EnterState();
    }

    public override void ExitState() {
        base.ExitState();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        if (interactionExecuted){
            interactionExecuted = false;
            _stateMachine.ChangeState(_factory.IdleState);
        }
    }

    public override void OnTriggerStay(Collider otherObject) {
        base.OnTriggerStay(otherObject);

        if (otherObject.gameObject.tag == "Health") {
            Events.OnCatchHealthPlant.Invoke();
            _context.DestroyObject(otherObject.gameObject);
        } 
        else if (otherObject.gameObject.tag == "Crystal") {
            Events.OnCatchCrystal.Invoke();
            _context.DestroyObject(otherObject.gameObject);
        }
        interactionExecuted = true;
        
        //_stateMachine.ChangeState(_factory.IdleState);
    }
}