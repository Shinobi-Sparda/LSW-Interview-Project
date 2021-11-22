using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void MovementInputEvent(Vector2 dir);
public delegate void InterationInputEvent();

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public MovementInputEvent movementInputEvent;
    public InterationInputEvent interationInputEvent;
    public bool useFixedUpdate;


    void Awake()
    {
        if (InputManager.instance == null)
            InputManager.instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            interationInputEvent?.Invoke();

        if (!useFixedUpdate)
            movementInputEvent?.Invoke(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }

    private void FixedUpdate()
    {
        if (useFixedUpdate)
            movementInputEvent?.Invoke(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }
}
