using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewConveyor : MonoBehaviour
{
    public string[] liste; // Array of spawnable objects ("cube", "trou", "rien")
    public int currentIndex; // Current index in the list

    public Vector3 highlightedScale = new Vector3(1.2f, 1.2f, 1); // Scale for the active element
    public Vector3 defaultScale = Vector3.one; // Default scale for inactive elements

    public Sprite rienSprite; // Icon for "Rien"
    public Sprite trouSprite; // Icon for "Trou"
    public Sprite cubeSprite; // Icon for "Cube"

    private Image[] conveyorSlots; // Fixed slots for the conveyor

    void Start()
    {
        currentIndex = 0;

        conveyorSlots = GetComponentsInChildren<Image>(); // Dynamically assign child Image components

        UpdateConveyorDisplay(); // Initialize conveyor display
    }

    // Updates the conveyor and highlights the current index
    public void UpdateTom()
    {
        // Increment the index and loop back
        currentIndex = (currentIndex + 1) % liste.Length;

        // Update the conveyor display
        UpdateConveyorDisplay();
    }

    // Updates conveyor icons and highlights
    private void UpdateConveyorDisplay()
    {
        for (int i = 0; i < conveyorSlots.Length; i++)
        {
            // Calculate the corresponding index in the list (wrap around using modulo)
            int listIndex = (currentIndex + i) % liste.Length;

            // Retrieve the corresponding sprite from the current list
            string item = liste[listIndex];
            conveyorSlots[i].sprite = GetSpriteForItem(item);

            // Reset all slots to default scale
            conveyorSlots[i].transform.localScale = defaultScale;
        }

        // Highlight the current index
        if (currentIndex < conveyorSlots.Length)
        {
            conveyorSlots[currentIndex].transform.localScale = highlightedScale;
        }
    }

    // Retrieves the sprite for the given item in ListeTom
    private Sprite GetSpriteForItem(string item)
    {
        return item switch
        {
            "cube" => cubeSprite,
            "trou" => trouSprite,
            _ => rienSprite
        };
    }
}
