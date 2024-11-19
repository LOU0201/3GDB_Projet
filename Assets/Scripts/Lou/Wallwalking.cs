using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallwalking : MonoBehaviour
{
    public Grille_3d grille3D; // Reference to the Grille_3d script for grid checks
    public LayerMask wallLayer; // Layer mask for walls

    private bool isWallWalking = false; // State to track if the player is wall walking

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector3.right);
            RotateCharacter(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector3.left);
            RotateCharacter(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(Vector3.up);
            RotateCharacter(Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Vector3.down);
            RotateCharacter(Vector3.down);
        }
    }

    void Move(Vector3 direction)
    {
        Vector3 targetPosition = transform.position + direction;

        // Check if the target position is within the grid and valid for wall movement
        if (grille3D.Estprit(targetPosition) || IsWall(targetPosition))
        {
            transform.position = targetPosition;
            isWallWalking = IsWall(targetPosition);
        }
        else
        {
            Debug.Log("Movement blocked or invalid target position.");
        }
    }

    void RotateCharacter(Vector3 direction)
    {
        // Determine rotation based on the direction of movement
        if (direction == Vector3.right)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0); // Facing right
        }
        else if (direction == Vector3.left)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0); // Facing left
        }
        else if (direction == Vector3.up)
        {
            if (isWallWalking)
            {
                transform.rotation = Quaternion.Euler(-90, 0, 0); // Facing up (on wall)
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0); // Facing up (on ground)
            }
        }
        else if (direction == Vector3.down)
        {
            if (isWallWalking)
            {
                transform.rotation = Quaternion.Euler(90, 0, 0); // Facing down (on wall)
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0); // Normal ground orientation
            }
        }
    }

    bool IsWall(Vector3 position)
    {
        // Check for the wall at the target position using the wall layer
        Collider[] colliders = Physics.OverlapSphere(position, 0.1f, wallLayer);
        return colliders.Length > 0;
    }
}
