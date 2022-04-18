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

	public virtual void LogicUpdate() { 

		// if movement != 0
		// call Move
	}

	public virtual void PhysicsUpdate() { }

	public virtual void OnTriggerStay(Collider otherObject) { }

	public bool GroundedCheck() {
        Vector3 spherePosition = new Vector3(_context.transform.position.x, _context.transform.position.y - _context.Data.GroundedOffset, _context.transform.position.z);
        return Physics.CheckSphere(spherePosition, _context.Data.GroundedRadius, _context.Data.GroundLayers, QueryTriggerInteraction.Ignore);
    }

	// Make move private
    public void Move(float targetSpeed) {
        if (targetSpeed != 0) {
            Vector3 inputDirection = new Vector3(_context.PlayerInput.move.x, 0.0f, _context.PlayerInput.move.y).normalized;
            _context.Data.TargetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _context.MainCamera.transform.eulerAngles.y;
            
            float rotationVelocity = _context.Data.RotationVelocity;
            float rotation = Mathf.SmoothDampAngle(_context.transform.eulerAngles.y, _context.Data.TargetRotation, ref rotationVelocity, _context.Data.RotationSmoothTime);
            _context.Data.RotationVelocity = rotationVelocity;
            
            _context.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }
        
        float speedOffset = 0.1f;
        float currentHorizontalSpeed = new Vector3(_context.Controller.velocity.x, 0.0f, _context.Controller.velocity.z).magnitude;
        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset) {
            _context.Data.Speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * _context.Data.SpeedChangeRate);
            _context.Data.Speed = Mathf.Round(_context.Data.Speed * 1000f) / 1000f;
        }
        else {
            _context.Data.Speed = targetSpeed;
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, _context.Data.TargetRotation, 0.0f) * Vector3.forward;
        _context.Controller.Move(targetDirection.normalized * (_context.Data.Speed * Time.deltaTime) + new Vector3(0.0f, _context.Data.VerticalVelocity, 0.0f) * Time.deltaTime);
    }
}
