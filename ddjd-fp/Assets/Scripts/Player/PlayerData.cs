using UnityEngine;

public class PlayerData {
    #region Static Values
    public float MoveSpeed = 9.0f;
    public float SprintSpeed = 5.335f;
    public float RotationSmoothTime = 0.12f;
    public float SpeedChangeRate = 10.0f;
    public float JumpHeight = 1.2f;
    public float Gravity = -15.0f;
    public float GroundedOffset = 0.58f;
    public float GroundedRadius = 0.35f;
    public LayerMask GroundLayers =  LayerMask.GetMask("Default");
    #endregion

    #region Player Status
    private int _currentHealth = 700;
    private int _maxHealth = 700;
    public int CurrentHealth  {get { return _currentHealth; }  set { _currentHealth = value; }}
    public int MaxHealth  {get { return _maxHealth; }  set { _maxHealth = value; }}

    private int _manaCrystal = 300;
    private int _maxMana = 700;
    public int ManaCrystal { get { return _manaCrystal; } set {_manaCrystal = value;} }
    public int MaxMana { get { return _manaCrystal; } set {_manaCrystal = value;} }
    #endregion

    #region Player Inventory

    private int _healthCrystal = 0;
    public int _maxHealthCrystal = 100;
    public int HealthCrystal{get { return _healthCrystal; } set { _healthCrystal = value; }}
    public int MaxHealthCrystal{get { return _maxHealthCrystal; } set { _maxHealthCrystal = value; }}


    #endregion

    #region Velocity
    private float _speed;
    private float _targetRotation = 0.0f;
    public float RotationVelocity;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;

    public float Speed  {get { return _speed; }  set { _speed = value; }}
    public float TargetRotation  {get { return _targetRotation; }  set { _targetRotation = value; }}
    public float VerticalVelocity {get { return _verticalVelocity; }  set { _verticalVelocity = value; }}
    public float TerminalVelocity  {get { return _terminalVelocity; }  set { _terminalVelocity = value; }}
    #endregion

    public PlayerData() { }
}