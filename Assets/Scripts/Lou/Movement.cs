using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;       // Speed of the player
    public float jumpForce = 5f;       // Force applied when jumping
    private Rigidbody rb;               // Reference to the Rigidbody component

    private bool isGrounded;            // Check if the player is on the ground

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    // Handle player movement
    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");  // A/D keys or Z/Q
        float moveVertical = Input.GetAxis("Vertical");      // W/S keys or Z/S

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);
    }

    // Handle jumping
    private void Jump()
    {
        // Check if the space key is pressed and the player is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // Detect ground contact
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Ensure the ground has the "Ground" tag
        {
            isGrounded = true; // Player is grounded
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Ensure the ground has the "Ground" tag
        {
            isGrounded = false; // Player is not grounded
        }
    }
}
