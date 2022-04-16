

public abstract class PlayerState {
	protected bool _isSuperState = false;

	protected Player _context;
	protected PlayerStateFactory _factory;
	protected PlayerState _currentSuperState;
	protected PlayerState _currentSubState;

	public PlayerState(Player currentContext, PlayerStateFactory playerStateFactory) {
		_context = currentContext;
		_factory = playerStateFactory;
	}

	#region Update State
	public void UpdateStates() {
		UpdateState();

		_currentSubState?.UpdateStates();
	}

	protected void SwitchState(PlayerState newState) {
		ExitState();

		if (_isSuperState) {
			newState.EnterState();
			_context.CurrentState = newState;
		} else if (_currentSuperState != null) {
			_currentSuperState.SetSubState(newState);
		}
	}

	protected void SetSuperState(PlayerState newSuperState) {
		_currentSuperState = newSuperState;
	}

	protected void SetSubState(PlayerState newSubState) {
		newSubState.EnterState();
		
		_currentSubState = newSubState;
		newSubState.SetSuperState(this);
	}
	#endregion
	
	#region State Methods
	public abstract void EnterState();

	public abstract void UpdateState();

	public abstract void ExitState();

	public abstract void CheckSwitchState();

	protected virtual void InitializeSubState() {}
	#endregion
}
