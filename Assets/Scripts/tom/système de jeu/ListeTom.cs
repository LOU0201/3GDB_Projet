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

    public Sprite rienSprite; // Icon for "Rien"
    public Sprite trouSprite; // Icon for "Trou"
    public Sprite cubeSprite; // Icon for "Cube"
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
            var=false;
            // Perform the action for the current item
            string currentItem = liste[currentIndex];
            if (currentItem == "cube")
            {
                
                G3D.Faire_carrer(joueur.position); // Spawn a cube
            }
            else if (currentItem == "trou")
            {
                G3D.Faire_Trou(joueur.position); // Spawn a hole FaireTrou va donc désactiver le cube en bas du joueur
               // GetComponent<musiqueblocs>().Note();
            }
            else if (currentItem == "rien")
            {
                Debug.Log("Nothing spawned this step.");
                FMODUnity.RuntimeManager.PlayOneShot("event:/V2/Player/Move");
            }


            // Move to the next index in the list
            currentIndex = (currentIndex) % liste.Length;
            FindObjectOfType<NewConveyor>().UpdateConveyor();
        }
        else
        {
            // Perform the action for the current item
            currentIndex = (currentIndex + 1) % liste.Length;
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
                FMODUnity.RuntimeManager.PlayOneShot("event:/V2/Player/Move");
            }
            // Move to the next index in the list
            FindObjectOfType<NewConveyor>().UpdateConveyor();

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
        print("setIndex FAIS");
    }
}
