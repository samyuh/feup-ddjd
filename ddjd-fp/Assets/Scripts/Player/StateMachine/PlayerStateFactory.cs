public class PlayerStateFactory {
    Player _context;

    public PlayerStateFactory(Player currentContext) {
        _context = currentContext;
    }

    public PlayerState Walk() {
        return new PlayerWalkState(_context, this);
    }

    #region Root States
    public PlayerState Grounded() {
        return new PlayerGroundedState(_context, this);
    }
    public PlayerState Jump() {
        return new PlayerJumpState(_context, this);
    }
    #endregion
}
