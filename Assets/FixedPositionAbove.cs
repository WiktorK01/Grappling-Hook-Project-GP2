using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPositionAbove : MonoBehaviour
{
    public Transform targetSphere; 
    public Vector3 offset = new Vector3(0, 1, 0);

    void Update()
    {
        if (targetSphere != null)
        {
            transform.position = targetSphere.position + offset;
        }
    }
}
