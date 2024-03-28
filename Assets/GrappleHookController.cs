using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHookController : MonoBehaviour
{
    public Vector3 grapplePoint;
    public Rigidbody playerRigidbody; 
    public CrosshairController crosshairController;

    private LineRenderer lineRenderer;
    private SpringJoint springJoint;
    public bool isGrappling = false;

    public float climbSpeed = 2f;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

    void Update()
    {
        if (crosshairController.isGrapplePointValid)
        {
            grapplePoint = crosshairController.GrapplePoint;
        }

        // Draw the rope if grappling
        if (isGrappling)
        {
            DrawRope();
        }

        // Check for left mouse button down to start grappling
        if (Input.GetMouseButtonDown(0) && crosshairController.isGrapplePointValid)
        {
            StartGrapple(grapplePoint);
        }

        // Check for left mouse button up to stop grappling
        if (Input.GetMouseButtonUp(0) && isGrappling)
        {
            StopGrapple();
        }

        // If right mouse button is held down, climb the rope
        if (Input.GetMouseButton(1) && isGrappling)
        {
            ClimbRope();
        }
    }

    private void StartGrapple(Vector3 grappleToPoint)
    {
        isGrappling = true;
        lineRenderer.enabled = true;

        springJoint = playerRigidbody.gameObject.AddComponent<SpringJoint>();
        springJoint.autoConfigureConnectedAnchor = false;
        springJoint.connectedAnchor = grappleToPoint;

        float distanceFromPoint = Vector3.Distance(playerRigidbody.position, grappleToPoint);
        springJoint.maxDistance = distanceFromPoint;
        springJoint.minDistance = 0f;

        springJoint.spring = 4.5f;
        springJoint.damper = 7f;
        springJoint.massScale = 4.5f;
    }

    private void DrawRope()
    {
        if (!lineRenderer.enabled) return;
        lineRenderer.SetPosition(0, playerRigidbody.position);
        lineRenderer.SetPosition(1, grapplePoint);
    }

    private void StopGrapple()
    {
        isGrappling = false;
        lineRenderer.enabled = false;
        Destroy(springJoint);
    }

    private void ClimbRope()
    {
        if (springJoint.maxDistance > 2f) 
        {
            springJoint.maxDistance -= climbSpeed * Time.deltaTime;
        }
    }
}