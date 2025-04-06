using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackUI : MonoBehaviour
{
    public Camera mainCamera; // Assign your main camera in the Inspector

    void Update()
    {
        if (mainCamera != null)
        {
            // Make the object look at the camera's forward direction
            transform.LookAt(transform.position + mainCamera.transform.forward);
        }
    }
}
