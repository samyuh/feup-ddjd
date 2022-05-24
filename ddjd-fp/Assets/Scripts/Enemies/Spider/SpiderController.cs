using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderController : MonoBehaviour {
    protected int _healthPoints;
    [SerializeField] private Slider _healthBar;

    [SerializeField] private float stopDistance = 0.25f;
    [SerializeField] private float followDistance = 1f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maxSpeed = 0.025f;
    [SerializeField] private float acceleration = 0.00001f;
    [SerializeField] private float deceleration = 0.1f;

    private GameObject _target;

    private void Awake() {
        _healthPoints = 350;
        _target = GameObject.Find("Player");
    }

    private void Update() {
        transform.LookAt(_target.transform.position);
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit)) {
            float distance = hit.distance;

            // Only follow after a certain _distance from the _target
            // Follows the _target until its close to his head
            if (distance > followDistance) Accelerate();
            else {
                Decelerate();

                Attack();
            }

            Move();
        }
    }

    #region Move Spider
    private void Accelerate() {
        speed += acceleration * Time.deltaTime;
        if(speed > maxSpeed) speed = maxSpeed;
    }
    
    private void Decelerate() {
        speed -= deceleration * Time.deltaTime;
        if (speed < 0) speed = 0f;
    }

    private void Move() {
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, speed);
    }
    #endregion

    #region Attack
    public void Attack() {
        if (speed == 0f) {
            Debug.Log("Attack");

            // check attack cooldown

            // if player in colliders
                // attack
                // set attack cooldown
        }
    }
    #endregion
    
    #region Receive Damage
    public void ApplyDamage(int damage) {
        _healthPoints -= damage;
        if (_healthPoints < 0) Death();
        else {
            _healthBar.value = _healthPoints / (float) 350;
        }
    }

    public void Death() {
        Destroy(gameObject);
    }
    #endregion
}
