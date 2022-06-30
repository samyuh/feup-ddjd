public class BossStateMachine {
	private BossController _context;
	public BossState CurrentState { get; private set; }

	public BossStateMachine(BossController context) {
		_context = context;
	}

    public void Initialize(BossState startingState) {
        CurrentState = startingState;
    }

	public void ChangeState(BossState newState) {
		CurrentState.ExitState();
		CurrentState = newState;
		CurrentState.EnterState();
	}
}