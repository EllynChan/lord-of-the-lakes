using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoatState : PlayerState
{
    public PlayerBoatState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        ProcessInputs();
        ProcessCollider();
        handleMove();
        player.Animator.SetFloat(player._horizontal, player.moveDirection.x);
        player.Animator.SetFloat(player._vertical, player.moveDirection.y);

        if (player.moveDirection != Vector2.zero)
        {
            player.Animator.SetFloat(player._lastHorizontal, player.moveDirection.x);
            player.Animator.SetFloat(player._lastVertical, player.moveDirection.y);
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            stateMachine.ChangeState(player.FishState);
        }
    }

    void ProcessInputs()
    {
        float horizontalInput = Input.GetKey(KeyCode.D) ? 1f : Input.GetKey(KeyCode.A) ? -1f : 0f;
        float verticalInput = Input.GetKey(KeyCode.W) ? 1f : Input.GetKey(KeyCode.S) ? -1f : 0f;

        player.moveDirection = new Vector2(horizontalInput, verticalInput).normalized;
    }

    void ProcessCollider()
    {
        if (player.renderer.sprite == player.sprites[0] || player.renderer.sprite == player.sprites[1])
        {
            player.collider.size = player.colliderSizeVertical;
            player.collider.offset = player.colliderOffsetVertical;
        }
        else
        {
            player.collider.size = player.colliderSizeHorizontal;
            player.collider.offset = player.colliderOffsetHorizontal;
        }
    }

    void handleMove()
    {
        player.rb.MovePosition(player.rb.position + player.moveDirection * player.moveSpeed * Time.fixedDeltaTime);
    }
}
