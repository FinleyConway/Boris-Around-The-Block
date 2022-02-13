public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData) : base(player, stateMachine, playerData)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.CurrentMovementSpeed = 0;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (input.x != 0 || input.y != 0)
        {
            stateMachine.ChangeState(player.WalkingState);
        }
    }
}
