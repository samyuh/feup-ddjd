using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTestController : MonoBehaviour {
    protected int _healthPoints;
    [SerializeField] private Slider _healthBar;

    void Awake() {
        _healthPoints = 350;
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
