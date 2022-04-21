public class StateFactory {
    public PlayerFallingState FallingState;

    public PlayerAttackGroundState AttackState;
    public PlayerIdleState IdleState;
    public PlayerJumpState JumpState;
    public PlayerWalkState WalkState;

    public PlayerRunState RunState;
    public PlayerAimState AimState;
    public PlayerDashState DashState;
    public PlayerInteractState InteractState;

    public StateFactory(Player context, StateMachine stateMachine) {
        #region Ability States
        AttackState = new PlayerAttackGroundState(context, stateMachine, this);
        JumpState = new PlayerJumpState(context, stateMachine, this);
        AimState = new PlayerAimState(context, stateMachine, this);
        DashState = new PlayerDashState(context, stateMachine, this);
        InteractState = new PlayerInteractState(context, stateMachine, this);
        #endregion

        #region Ground
        IdleState = new PlayerIdleState(context, stateMachine, this);
        RunState = new PlayerRunState(context, stateMachine, this);
        WalkState = new PlayerWalkState(context, stateMachine, this);
        #endregion

        #region Air
        FallingState = new PlayerFallingState(context, stateMachine, this);
        #endregion
    }
}