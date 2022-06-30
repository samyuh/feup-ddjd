public class SpiderStateMachine {
	private SpiderController _context;
	public SpiderState CurrentState { get; private set; }

	public SpiderStateMachine(SpiderController context) {
		_context = context;
	}

    public void Initialize(SpiderState startingState) {
        CurrentState = startingState;
    }

	public void ChangeState(SpiderState newState) {
		CurrentState.ExitState();
		CurrentState = newState;
		CurrentState.EnterState();
	}
}