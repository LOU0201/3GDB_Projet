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
    [Header("Icons")]
    public Sprite rienSprite; 
    public Sprite trouSprite; 
    public Sprite cubeSprite;
    public Sprite yellowSprite;
    public NewConveyor conveyorBelt;

    private bool var=true;

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;
    }

    // Update is called once per frame
    public void UpdateTom()
    {
        print("currentIndex : " + currentIndex);
        if (var)
        {
            var = false;
            string currentItem = liste[currentIndex];
            ExecuteCurrentItem(currentItem);

            currentIndex = currentIndex % liste.Length;
            FindObjectOfType<NewConveyor>().UpdateConveyor();
        }
        else
        {
            currentIndex = (currentIndex + 1) % liste.Length;
            string currentItem = liste[currentIndex];
            ExecuteCurrentItem(currentItem);

            FindObjectOfType<NewConveyor>().UpdateConveyor();
        }
    }

    // NEW: Helper method to handle item execution
    private void ExecuteCurrentItem(string currentItem)
    {
        switch (currentItem)
        {
            case "cube":
                G3D.Faire_carrer(joueur.position);
                break;

            case "trou":
                G3D.Faire_Trou(joueur.position);
                break;

            case "yellow":
                // NEW: Handle yellow cube logic
                if (G3D.Non_Blockeur == true)
                {
                    // Show yellow icon or perform yellow-specific action
                    FMODUnity.RuntimeManager.PlayOneShot("event:/V2/Blocs/SpecialYellow"); // Example sound
                }
                break;

            case "rien":
                FMODUnity.RuntimeManager.PlayOneShot("event:/V2/Player/Move");
                break;

            default:
                Debug.LogWarning("Unknown item type: " + currentItem);
                break;
        }
    }
    public int GetIndex()
    {
        return currentIndex;    
    }
    public string GetcurrentItem()
    {
        return liste[currentIndex];
    }
    public void setIndex(int index)
    {
        if (index == 0)
        {
            RefrecheIndex();
        }
        else
        {
            currentIndex = index;
            FindObjectOfType<NewConveyor>().UpdateConveyor();
        }
    }

    public void RefrecheIndex()
    {
        currentIndex = 0;
        conveyorBelt.ResetElementsScale();
        var =true;
        
    }
}
