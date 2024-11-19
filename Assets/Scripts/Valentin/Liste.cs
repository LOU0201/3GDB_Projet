using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Liste : MonoBehaviour
{
    public string[] liste; // Array of spawnable objects ("cube", "trou")
    public int curseur; // Tracks the number of steps taken (0 to 3)
    public int currentIndex; // Current index in the list
    public int limite = 0; // Length of the list
    public Grille_3d G3D; // Reference to the grid system
    public Transform joueur; // Player position
    public TextMeshProUGUI nextSpawnText; // TextMeshPro for showing prediction

    // Start is called before the first frame update
    void Start()
    {
        curseur = 0;
        currentIndex = 0;

        // Ensure `liste` is valid
        if (liste.Length == 0 || limite <= 0)
        {
            Debug.LogError("Liste is empty or limite is not set correctly!");
            return;
        }

        UpdateNextSpawnDisplay(); // Show the first prediction
    }

    // Update is called once per frame
    void Update()
    {
        // Check for movement input
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            curseur++; // Increment the step counter

            if (curseur >= 3) // If 3 steps are taken
            {
                // Spawn the current item
                string currentItem = liste[currentIndex];
                if (currentItem == "cube")
                {
                    G3D.Faire_carrer(joueur.position); // Spawn a cube
                    Debug.Log("Spawned: cube");
                }
                else if (currentItem == "trou")
                {
                    G3D.Faire_Trou(joueur.position); // Spawn a hole
                    Debug.Log("Spawned: trou");
                }

                // Reset the step counter
                curseur = 0;

                // Advance to the next item in the list
                currentIndex = (currentIndex + 1) % limite;

                // Update the prediction display
                UpdateNextSpawnDisplay();
            }
        }
    }

    // Updates the TextMeshPro text to show the next spawnable object
    private void UpdateNextSpawnDisplay()
    {
        int nextIndex = (currentIndex) % limite; // Next item to be spawned after 3 steps
        string nextItem = liste[nextIndex];

        // Update the text to show prediction
        if (nextItem == "cube")
        {
            nextSpawnText.text = "Next: Cube";
        }
        else if (nextItem == "trou")
        {
            nextSpawnText.text = "Next: Hole";
        }
        else
        {
            nextSpawnText.text = ""; // Clear the text if invalid or unexpected
        }
    }
}
