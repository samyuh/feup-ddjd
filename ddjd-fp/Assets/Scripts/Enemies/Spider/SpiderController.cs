using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderController : MonoBehaviour {
    protected int _healthPoints;
    [SerializeField] private Slider _healthBar;

    private GameObject _target;
    private float _targetDistance = 2f;

    private float _resetAttackCooldown = 0.5f;
    private float _attackCooldown;

    void Awake() {
        _target = GameObject.Find("Player");
        _attackCooldown = 0f;
        _healthPoints = 350;
    }

    void Update() {
        _attackCooldown -= Time.deltaTime;

        transform.LookAt(_target.transform);
        if(Physics.Raycast(transform.position, _target.transform.position - transform.position, out RaycastHit hit)) {
            float distance = hit.distance;

            if (distance >= _targetDistance) {
                transform.position = Vector3.MoveTowards(transform.position,  _target.transform.position, 0.03f);
            } else if ((_attackCooldown < 0f) && (distance <1.5f)) {
                _target.SendMessage("ApplyDamage", 50);
                _attackCooldown = _resetAttackCooldown;
            }
        }
    }

    public void ApplyDamage(int damage) {
        _healthPoints -= damage;
        if (_healthPoints < 0) Death();
        else {
            _healthBar.value = _healthPoints / (float) 350;
        }
    }

    public void Death() {
        gameObject.SetActive(false);
    }
}
