using UnityEditor.IMGUI.Controls;
using UnityEngine;

using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    public float speed = 4;
    private Rigidbody playerBody;
    private float movementX;
    private float xBound = 10.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
    }

    // Update is called once per frame
    void Update()
    {
        playerBody.linearVelocity = new Vector3(movementX * speed, 0.0f, 0.0f);
        Vector3 position = playerBody.position;
        position.x = Mathf.Clamp(position.x, -xBound, xBound);
        playerBody.position = position;
    }


}
