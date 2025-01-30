using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NextDisplay : MonoBehaviour
{
    public string[] liste; // Array of spawnable objects ("rien", "cube", "trou")
    public int curseur; // Current position in the list
    public int limite = 0; // Length of the list
    public TextMeshProUGUI nextSpawnText; // Reference to the TextMeshPro text object

    // Start is called before the first frame update
    void Start()
    {
        curseur = 0;
        UpdateNextSpawnDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Increment curseur and loop back if it exceeds limite
            curseur += 1;
            if (curseur == limite)
            {
                curseur = 0;
            }

            // Perform actions based on the current spawn type
            string currentItem = liste[curseur];
            if (currentItem == "rien")
            {
                Debug.Log("rien");
            }
            else if (currentItem == "cube")
            {
                Debug.Log("cube");
            }
            else if (currentItem == "trou")
            {
                Debug.Log("trou");
            }

            // Update the next spawn display
            UpdateNextSpawnDisplay();
        }
    }

    // Updates the TMPro text to display the next spawnable object
    void UpdateNextSpawnDisplay()
    {
        int nextIndex = (curseur + 1) % limite; // Calculate the next index
        string nextItem = liste[nextIndex];

        if (nextItem == "rien")
        {
            nextSpawnText.text = ""; // Clear the text if the next item is "rien"
        }
        else
        {
            nextSpawnText.text = "Next: " + nextItem; // Display the next item
        }
    }
}
