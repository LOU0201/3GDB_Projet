using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ListeTom_1 : MonoBehaviour
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
    public NewConveyor conveyorBelt;

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;

       // UpdateUpcomingSpawnDisplay(); // Show the first set of predictions
    }

    // Update is called once per frame
    public void UpdateTom()
    {
        // Perform the action for the current item
        string currentItem = liste[currentIndex];
        if (currentItem == "cube")
        {
            G3D.Faire_carrer(joueur.position); // Spawn a cube
        }
        else if (currentItem == "trou")
        {
            G3D.Faire_Trou(joueur.position); // Spawn a hole FaireTrou va donc désactiver le cube en bas du joueur
        }
        else if (currentItem == "rien")
        {
            Debug.Log("Nothing spawned this step.");
        }

        // Move to the next index in the list
        currentIndex = (currentIndex + 1) % liste.Length;


    }



/*    // Updates the UI Images to show the items in positions 1, 2, and 3
    private void UpdateUpcomingSpawnDisplay()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/V1/Player/playermove");
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
    }*/
}
