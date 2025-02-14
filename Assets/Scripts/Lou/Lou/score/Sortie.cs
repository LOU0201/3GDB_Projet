using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sortie : MonoBehaviour
{
    [Header("UI Components")]
    public TMP_Text scoreText; // Reference to the score text UI element

    [Header("Game References")]
    public GameObject playerSpawnPoint; // Predetermined spawn point for the player
    public ResetTom resetTom; // Reference to the ResetTom script

    private int playerScore = 0; // Keeps track of the player's score

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Increment the score
            playerScore++;
            scoreText.text = "Score: " + playerScore.ToString();
            resetTom.Rappatriment();
            Debug.Log("Rappatriment() called from Sortie script.");
        }
    }
}
