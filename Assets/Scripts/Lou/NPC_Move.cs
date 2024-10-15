using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Move : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;          // Reference to the NavMeshAgent
    public Transform player;             // Reference to the player transform
    public float jumpForce = 5f;        // Jumping force
    public float detectionRange = 10f;   // Range to detect the player
    private bool isJumping = false;      // Check if the NPC is currently jumping
    private Rigidbody rb;                // Reference to the Rigidbody component

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            FollowPlayer();

            // Check if the player is above the NPC and decide to jump
            if (player.position.y > transform.position.y)
            {
                TryJumpToPlayer();
            }
        }
    }

    private void FollowPlayer()
    {
        // Move towards the player unless jumping
        if (!isJumping)
        {
            agent.SetDestination(player.position);
        }
    }

    private void TryJumpToPlayer()
    {
        // Check if the NPC is grounded before jumping
        if (!isJumping && IsGrounded())
        {
            // Calculate the position to jump to (above the player)
            Vector3 jumpTarget = new Vector3(player.position.x, transform.position.y, player.position.z);

            // Set the destination for the NavMeshAgent to jump towards the target
            agent.SetDestination(jumpTarget);
            Jump();
        }
    }

    private void Jump()
    {
        isJumping = true;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // Start a coroutine to handle jump duration
        StartCoroutine(JumpRoutine());
    }

    private IEnumerator JumpRoutine()
    {
        // Wait for a short duration (you can adjust this based on your jump animation)
        yield return new WaitForSeconds(0.5f);

        // Reset jumping state after landing
        isJumping = false;
    }

    private bool IsGrounded()
    {
        // Check if the NPC is on the ground (can use raycasting or collision detection)
        return Physics.Raycast(transform.position, Vector3.down, 1.1f); // Adjust the distance as needed
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false; // Reset jump state when landing
        }
    }
}
