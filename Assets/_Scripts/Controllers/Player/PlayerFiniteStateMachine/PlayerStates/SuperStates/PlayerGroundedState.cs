using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected Vector2 input;
    protected bool sprintInput;

    public PlayerGroundedState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData) : base(player, stateMachine, playerData)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        input = player.InputSystem.GetMoveInput;
        sprintInput = player.InputSystem.SprintInput;

        if (sprintInput && input.y != -1)
        {
            stateMachine.ChangeState(player.RunState);
        }
    }
}
