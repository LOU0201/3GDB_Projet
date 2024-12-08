using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ListeTom : MonoBehaviour
{
    public string[] liste; // Array of spawnable objects ("cube", "trou", "rien")
    public int currentIndex; // Current index in the list
    public Grille_3d G3D; // Reference to the grid system
    public Transform joueur; // Player position

    // UI for displaying the current and upcoming spawnable objects
    public Image[] upcomingSpawnIcons; // Array of 3 UI Images for displaying sprites
    public Sprite rienSprite; // Icon for "Rien"
    public Sprite trouSprite; // Icon for "Trou"
    public Sprite cubeSprite; // Icon for "Cube"

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;

        // Ensure `liste` is valid
        if (liste == null || liste.Length == 0 || upcomingSpawnIcons.Length < 3)
        {
            Debug.LogError("Liste is empty, not assigned, or upcomingSpawnIcons is not properly set!");
            return;
        }

        UpdateUpcomingSpawnDisplay(); // Show the first set of predictions
    }

    // Update is called once per frame
    void Update()
    {
        // Check for movement input
        /*if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Perform the action for the current item
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
            else if (currentItem == "rien")
            {
                Debug.Log("Nothing spawned this step.");
            }

            // Move to the next index in the list
            currentIndex = (currentIndex + 1) % liste.Length;

            // Update the predictions for the next three items
            UpdateUpcomingSpawnDisplay();
        }*/
    }//Donc ce que fait le script en Update est désormais appeller par la fonction
    public void UpdateTom()
    {
        // Perform the action for the current item
        string currentItem = liste[currentIndex];
        if (currentItem == "cube")
        {
            G3D.Faire_carrer(joueur.position); // Spawn a cube
            Debug.Log("Spawned: cube");
        }
        else if (currentItem == "trou")
        {
            G3D.Faire_Trou(joueur.position); // Spawn a hole FaireTrou va donc désactiver le cube en bas du joueur
            Debug.Log("Spawned: trou");
        }
        else if (currentItem == "rien")
        {
            Debug.Log("Nothing spawned this step.");
        }

        // Move to the next index in the list
        currentIndex = (currentIndex + 1) % liste.Length;

        // Update the predictions for the next three items
        UpdateUpcomingSpawnDisplay();
    }



    // Updates the UI Images to show the items in positions 1, 2, and 3
    private void UpdateUpcomingSpawnDisplay()
    {
        for (int i = 0; i < upcomingSpawnIcons.Length; i++) // Loop through the 3 upcoming icons
        {
            // Get the index for the current, next, and the one after that
            int index = (currentIndex + i) % liste.Length;
            string nextItem = liste[index];

            // Assign the appropriate sprite to each Image
            if (nextItem == "cube")
            {
                upcomingSpawnIcons[i].sprite = cubeSprite;
            }
            else if (nextItem == "trou")
            {
                upcomingSpawnIcons[i].sprite = trouSprite;
            }
            else
            {
                upcomingSpawnIcons[i].sprite = rienSprite; // Default to "Rien" icon
            }
        }
    }
}
