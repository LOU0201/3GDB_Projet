using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulle : MonoBehaviour
{
    public GameObject apparence;
    public GameObject _parent;
    public Animator sortie;
    public float triggerDistance = 0.1f;
    public float sortieAnimationLength = 1.1f; // Manually set to 1.1 seconds
    private float cooldownTimer = 0f;

    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            return; // Skip updates during cooldown
        }

        if (Vector3.Distance(apparence.transform.position, _parent.transform.position) < triggerDistance)
        {
            sortie.Play("sortie", 0, 0f); // Force play from start
            cooldownTimer = sortieAnimationLength; // Prevent interruption
        }
        else
        {
            sortie.Play("Bulle", 0, 0f); // Default state
        }
    }
}
