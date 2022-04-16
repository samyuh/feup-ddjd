using UnityEngine;

public abstract class Enemy {
    protected int _healthPoints;
    protected GameObject _target;

    public Enemy(int healthPoints, GameObject target) {
        _healthPoints = healthPoints;
        _target = target;
    }
}
