public class PlayerStateMachine
{
    public PlayerState CurrentState { get; private set; }
    public PlayerState previousState { get; private set; }

    public void Initialize(PlayerState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerState newState)
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
        }

        previousState = CurrentState;
        CurrentState = newState;

        if (CurrentState != null)
        {
            CurrentState.Enter();
        }
    }

    public void RevertState()
    {
        if (previousState != null)
        {
            ChangeState(previousState);
        }
    }
}
