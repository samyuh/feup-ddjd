using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderController : MonoBehaviour {
    protected int _healthPoints;
    [SerializeField] private Slider _healthBar;

    [SerializeField] private float maxDistance = 50f;
    [SerializeField] private float followDistance = 2f;
    [SerializeField] private float speed = 0f;
    [SerializeField] private float maxSpeed = 0.5f;
    [SerializeField] private float acceleration = 0.075f;
    [SerializeField] private float deceleration = 0.5f;

    private GameObject _target;

    private LayerMask mask;

    private void Awake() {
        _healthPoints = 350;
        _target = GameObject.Find("Player");
        mask =  LayerMask.GetMask("Player");
    }

    private void Update() {
        
        
        if (Physics.Raycast(transform.position, new Vector3(_target.transform.position.x,0.5f ,_target.transform.position.z) - transform.position, out RaycastHit hit, maxDistance, mask)) {
            float distance = hit.distance;
            
            // Only follow after a certain _distance from the _target
            // Follows the _target until its close to his head
            if (distance > followDistance) Accelerate();
            else{
                Decelerate();
                Attack();
            }
            
        }
    }

    #region Move Spider
    private void Accelerate() {
        speed += acceleration * Time.deltaTime;
        if(speed > maxSpeed) speed = maxSpeed;
        Debug.Log("Accelerate");
        Move();
    }
    
    private void Decelerate() {
        speed -= deceleration * Time.deltaTime;
        if (speed < 0) speed = 0f;
        Debug.Log("Decelerate");
        Move();
    }

    private void Move() {
        Vector3 posit = new Vector3(_target.transform.position.x,0 ,_target.transform.position.z);
        transform.LookAt(posit);

        transform.position += transform.forward * speed;
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
