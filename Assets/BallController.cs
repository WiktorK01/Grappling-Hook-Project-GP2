using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float moveSpeed = 10f; 
    private Rigidbody rb;
    public Transform cameraTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // move relative to the camera
        Vector3 movement = cameraTransform.right * moveHorizontal + cameraTransform.forward * moveVertical;
        movement.y = 0; // ensure that the ball doesn't move up/down

        rb.AddForce(movement.normalized * moveSpeed);
    }
}
