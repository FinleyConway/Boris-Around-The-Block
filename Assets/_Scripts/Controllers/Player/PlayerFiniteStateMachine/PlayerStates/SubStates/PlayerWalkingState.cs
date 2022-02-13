public class PlayerWalkingState : PlayerGroundedState
{
    public PlayerWalkingState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData) : base(player, stateMachine, playerData)
    {
    }
    public override void Enter()
    {
        base.Enter();

        player.CurrentMovementSpeed = playerData.walkingVelocity;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.SetVelocity(input);

        if (input.x == 0 && input.y == 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
