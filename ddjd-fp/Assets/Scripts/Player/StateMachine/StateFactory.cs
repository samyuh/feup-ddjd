public class StateFactory {
    public PlayerGroundState GroundState;
    public PlayerAirState AirState;
    public PlayerAbilityState AbilityState;

    public PlayerAttackGroundState AttackState;
    public PlayerIdleState IdleState;
    public PlayerJumpState JumpState;
    public PlayerWalkState WalkState;

    public StateFactory(Player context, StateMachine stateMachine) {
        #region Ability States
        AbilityState = new PlayerAbilityState(context, stateMachine, this);
        AttackState = new PlayerAttackGroundState(context, stateMachine, this);
        JumpState = new PlayerJumpState(context, stateMachine, this);
        #endregion

        #region Ground
        GroundState = new PlayerGroundState(context, stateMachine, this);
        IdleState = new PlayerIdleState(context, stateMachine, this);
        WalkState = new PlayerWalkState(context, stateMachine, this);
        #endregion

        #region Air
        AirState = new PlayerAirState(context, stateMachine, this);
        #endregion
    }
}