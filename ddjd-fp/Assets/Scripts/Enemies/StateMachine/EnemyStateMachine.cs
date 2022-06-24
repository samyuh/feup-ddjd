public class EnemyStateMachine {
	private SpiderController _context;
	public EnemyState CurrentState { get; private set; }

	public EnemyStateMachine(SpiderController context) {
		_context = context;
	}

    public void Initialize(EnemyState startingState) {
        CurrentState = startingState;
    }

	public void ChangeState(EnemyState newState) {
		CurrentState.ExitState();
		CurrentState = newState;
		CurrentState.EnterState();
	}
}