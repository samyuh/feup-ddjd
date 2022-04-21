using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState {
	protected Player _context;
	protected StateMachine _stateMachine;
	protected StateFactory _factory;

    protected float _targetVelocity = 0f;

	public PlayerState(Player currentContext, StateMachine stateMachine, StateFactory stateFactory) {
		_context = currentContext;
		_stateMachine = stateMachine;
		_factory = stateFactory;
	}
	
	public virtual void EnterState() { }

	public virtual void ExitState() { }

	public virtual void LogicUpdate() { 
        PlayerRotation();

        Vector3 verticalVelocity = MoveVertical(_context.Data.Gravity);
        Vector3 horizontalVelocity = MoveHorizontal(_targetVelocity);
		_context.Controller.Move(verticalVelocity + horizontalVelocity);
	}

	public virtual void PhysicsUpdate() { }

    public virtual void OnControllerColliderHit(ControllerColliderHit hit) { }

	public bool GroundedCheck() {
        Vector3 spherePosition = new Vector3(_context.transform.position.x, _context.transform.position.y - _context.Data.GroundedOffset, _context.transform.position.z);
        return Physics.CheckSphere(spherePosition, _context.Data.GroundedRadius, _context.Data.GroundLayers, QueryTriggerInteraction.Ignore);
    }

    #region General Movement
    protected Vector3 GetMovementInputDirection() {
        return new Vector3(_context.PlayerInput.Movement.x, 0f, _context.PlayerInput.Movement.y);
    }

    protected void PlayerRotation() {
        Vector3 inputDirectionVector = GetMovementInputDirection();
        if (inputDirectionVector != new Vector3(0f, 0f, 0f)) {
            Vector3 inputDirection = inputDirectionVector.normalized;
            _context.Data.TargetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _context.Camera.MainCamera.transform.eulerAngles.y;
            
            float rotationVelocity = _context.Data.RotationVelocity;
            float rotation = Mathf.SmoothDampAngle(_context.transform.eulerAngles.y, _context.Data.TargetRotation, ref rotationVelocity, _context.Data.RotationSmoothTime);
            _context.Data.RotationVelocity = rotationVelocity;
            
            _context.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }
    }

    protected virtual Vector3 MoveVertical(float gravity) {
        return new Vector3(0.0f, _context.Data.VerticalVelocity, 0.0f) * Time.deltaTime;
    }

    protected virtual Vector3 MoveHorizontal(float targetSpeed) {
        Vector3 inputDirectionVector = GetMovementInputDirection();
        if (inputDirectionVector == new Vector3(0f, 0f, 0f)) { 
            return new Vector3(0f, 0f, 0f);
        }

        float speedOffset = 0.1f;
        float currentHorizontalSpeed = new Vector3(_context.Controller.velocity.x, 0.0f, _context.Controller.velocity.z).magnitude;
        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset) {
            _context.Data.Speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * _context.Data.SpeedChangeRate);
            _context.Data.Speed = Mathf.Round(_context.Data.Speed * 1000f) / 1000f;
        } else {
            _context.Data.Speed = targetSpeed;
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, _context.Data.TargetRotation, 0.0f) * Vector3.forward;
        return targetDirection.normalized * (_context.Data.Speed * Time.deltaTime);
    }
    #endregion
}
