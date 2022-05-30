using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderController : MonoBehaviour {
    protected int _healthPoints = 350;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private GameObject spider;

    private EnemyState _enemy;

    private void Start() {
        _healthPoints = 350;
        _enemy = new EnemyState(350, GameObject.Find("Player"), spider);
    }

    private void Update() {
        _enemy.LogicUpdate();
    }

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
