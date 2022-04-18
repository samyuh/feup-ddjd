using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState {
	protected Player _context;
	protected StateMachine _stateMachine;
	protected StateFactory _factory;

	public PlayerState(Player currentContext, StateMachine stateMachine, StateFactory stateFactory) {
		_context = currentContext;
		_stateMachine = stateMachine;
		_factory = stateFactory;
	}
	
	public virtual void EnterState() { }

	public virtual void ExitState() { }

	public virtual void LogicUpdate() { }

	public virtual void PhysicsUpdate() { }

	public virtual void OnTriggerStay(Collider otherObject) { }
}
