public class StateMachine {
	private Player _context;
	public PlayerState CurrentState { get; private set; }

	public StateMachine(Player context) {
		_context = context;
	}

    public void Initialize(PlayerState startingState) {
        CurrentState = startingState;
    }

	public void ChangeState(PlayerState newState) {
		CurrentState.ExitState();
		CurrentState = newState;
		CurrentState.EnterState();
	}
}