using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementController : MonoBehaviour
{
    public float moveSpeed;
    private Vector3 dir;
    private Player player;

    private void Start()
    {
        InputManager.instance.movementInputEvent += PlayerControl;
        player = GetComponent<Player>();
    }

    void PlayerControl(Vector2 direction)
    {
        if (direction == Vector2.zero)
            player.UpdateAnimation(EnumBase.PlayerAnimStates.Idle);
        if (direction == Vector2.up)
            player.UpdateAnimation(EnumBase.PlayerAnimStates.MovingUp);
        if (direction == Vector2.right)
            player.UpdateAnimation(EnumBase.PlayerAnimStates.MovingRight);
        if (direction == Vector2.left)
            player.UpdateAnimation(EnumBase.PlayerAnimStates.MovingLeft);
        if (direction == Vector2.down)
            player.UpdateAnimation(EnumBase.PlayerAnimStates.MovingDown);

        //dir = direction;
        Vector2 newMovePont = new Vector2(transform.position.x, transform.position.y) + direction;
        transform.position = Vector2.Lerp(transform.position, newMovePont, moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        InputManager.instance.useFixedUpdate = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        InputManager.instance.useFixedUpdate = false;
    }
}
