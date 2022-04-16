public class PlayerStateFactory {
    Player _context;

    public PlayerStateFactory(Player currentContext) {
        _context = currentContext;
    }

    #region Super States
    public PlayerState Grounded() {
        return new PlayerGroundedState(_context, this);
    }
    public PlayerState Jump() {
        return new PlayerJumpState(_context, this);
    }
    #endregion

    #region Sub States
    public PlayerState Walk() {
        return new PlayerWalkState(_context, this);
    }

    public PlayerState Idle() {
        return new PlayerIdleState(_context, this);
    }

    public PlayerState Attack() {
        return new PlayerAttackState(_context, this);
    }
    #endregion
}
