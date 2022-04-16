using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState {
	protected Player _context;
	protected StateMachine _stateMachine;
	protected StateFactory _factory;

	public bool ExitingState = false;

	public PlayerState(Player currentContext, StateMachine stateMachine, StateFactory stateFactory) {
		_context = currentContext;
		_stateMachine = stateMachine;
		_factory = stateFactory;
	}
	
	public virtual void EnterState() {
		ExitingState = false;
	}

	public virtual void ExitState() {
		//ExitingState = true;
	}

	public virtual void LogicUpdate() { }

	public virtual void PhysicsUpdate() { }

	public virtual void OnTriggerStay(Collider otherObject) { }
}
