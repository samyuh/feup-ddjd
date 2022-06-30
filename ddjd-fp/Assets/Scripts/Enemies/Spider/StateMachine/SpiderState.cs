using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpiderState {
    protected SpiderController _context;
    protected SpiderStateMachine _stateMachine;
    protected SpiderFactory _stateFactory;

    protected GameObject _target;
    protected LayerMask mask;

    protected float maxDistance = 50f;
    protected float followDistance = 2f;
    protected float speed = 0f;
    protected float maxSpeed = 0.25f;
    protected float acceleration = 0.2f;
    protected float deceleration = 1f;

    public SpiderState(SpiderController context, SpiderStateMachine stateMachine,  SpiderFactory stateFactory) { //GameObject target, GameObject context) {
       _context = context;
       _stateMachine = stateMachine;
       _stateFactory = stateFactory;

        _target = GameObject.Find("Player");
        mask =  LayerMask.GetMask("Player");
    }

    public virtual void EnterState() { }

    public virtual void ExitState() { }

    public virtual void LogicUpdate() { }

	public virtual void PhysicsUpdate() { }

    #region Move Spider
    protected void Accelerate() {
        speed += acceleration * Time.deltaTime;
        if(speed > maxSpeed) speed = maxSpeed;
        Move();
    }
    
    protected void Decelerate() {
        speed -= deceleration * Time.deltaTime;
        if (speed < 0) speed = 0f;
        Move();
    }

    protected void Move() {
        Vector3 posit = new Vector3(_target.transform.position.x, _target.transform.position.y - 0.7f ,_target.transform.position.z);
        _context.transform.LookAt(posit);
        _context.transform.position += _context.transform.forward * speed;
    }
    #endregion
}
