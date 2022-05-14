using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderController : MonoBehaviour {
    protected int _healthPoints;
    [SerializeField] private Slider _healthBar;

    public float stopDistance = 0.25f;
    public float followDistance = 1f;
    private float speed = 5f;
    public float maxSpeed = 0.025f;
    public float acceleration = 0.00001f;
    public float deceleration = 0.1f;

    private GameObject _target;

    private void Awake() {
        _healthPoints = 350;
        _target = GameObject.Find("Player");
    }

    #region Move Spider
    private void Update() {
        transform.LookAt(_target.transform.position);
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit)) {
            float distance = hit.distance;
            Debug.Log(distance);
            Debug.Log(followDistance);

            // Only follow after a certain _distance from the _target
            // Follows the _target until its close to his head
            if (distance > followDistance) accelerate();
            else decelarate();

            Move();
        }
    }

    private void accelerate() {
        speed += acceleration * Time.deltaTime;
        if(speed > maxSpeed) speed = maxSpeed;
    }
    
    private void decelarate() {
        speed -= deceleration * Time.deltaTime;
        if (speed < 0) speed = 0f;
    }

    private void Move() {
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, speed);
    }
    #endregion

    public void Attack() {

    }
    
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
}
