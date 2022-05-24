using UnityEngine;

public class EnemyState {
    protected int _healthPoints;

    private float maxDistance = 50f;
    private float followDistance = 1f;
    private float speed = 0f;
    private float maxSpeed = 0.025f;
    private float acceleration = 0.1f;
    private float deceleration = 0.25f;

    private GameObject _target;
    private LayerMask mask;

    public EnemyState(int healthPoints, GameObject target) {
        _target = target;

        mask =  LayerMask.GetMask("Player");
    }

    public void EnterState() { }

    public void ExitState() { }

    public void LogicUpdate() { 
        /*
        if (Physics.Raycast(transform.position, _target.transform.position - transform.position, out RaycastHit hit, maxDistance, mask)) {
            float distance = hit.distance;

            if (distance > followDistance) Accelerate();
            else {
                Decelerate();
                Attack();
            }
        }
        */
    }

	public void PhysicsUpdate() { }

    #region Move Spider
    private void Accelerate() {
        speed += acceleration * Time.deltaTime;
        if(speed > maxSpeed) speed = maxSpeed;
        Move();
    }
    
    private void Decelerate() {
        speed -= deceleration * Time.deltaTime;
        if (speed < 0) speed = 0f;
        Move();
    }

    private void Move() {
        Vector3 posit = new Vector3(_target.transform.position.x,0 ,_target.transform.position.z);
        //transform.LookAt(posit);

        //transform.position += transform.forward * speed;
    }
    #endregion

    #region Attack
    public void Attack() {
        if (speed == 0f) {
            // check attack cooldown

            // if player in colliders
                // attack
                // set attack cooldown
        }
    }
    #endregion
}
