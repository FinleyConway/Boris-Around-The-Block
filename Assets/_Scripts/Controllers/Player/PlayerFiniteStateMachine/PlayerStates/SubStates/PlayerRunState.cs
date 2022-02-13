public class PlayerRunState : PlayerGroundedState
{
    public PlayerRunState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData) : base(player, stateMachine, playerData)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.CurrentMovementSpeed = playerData.runningVelocity;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.SetVelocity(input);

        if (!sprintInput)
        {
            stateMachine.ChangeState(player.IdleState);
        }

        if (input.x == 0 && input.y == 0)
        {
            player.InputSystem.UseSprintInput();
            stateMachine.ChangeState(player.IdleState);
        }
        else if (input.y == -1)
        {
            player.InputSystem.UseSprintInput();
            stateMachine.ChangeState(player.WalkingState);
        }
    }
}
