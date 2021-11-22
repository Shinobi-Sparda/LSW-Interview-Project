using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform playerPos;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    void Start()
    {
        playerPos = FindObjectOfType<MovementController>().transform;
        transform.position = new Vector3(playerPos.position.x, playerPos.position.y, transform.position.z);
    }

    void Update()
    {
        if (playerPos != null)
        {
            float clampX = Mathf.Clamp(playerPos.position.x, minX, maxX);
            float clampY = Mathf.Clamp(playerPos.position.y, minY, maxY);

            transform.position = Vector3.Lerp(transform.position, new Vector3(clampX, clampY, transform.position.z), 0.25f);
        }
    }
}
