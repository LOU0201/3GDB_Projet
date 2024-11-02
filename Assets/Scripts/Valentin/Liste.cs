using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liste : MonoBehaviour
{
    public string[] liste;
    public int curseur;
    public Grille_3d G3D;
    public Transform joueur;
    public int limite = 0;
    // Start is called before the first frame update
    void Start()
    {
        curseur = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            curseur += 1;
            if (curseur == limite)
            {
                curseur=0;
            }
            if(liste[curseur] == "rien")
            {
                Debug.Log("rien");
            }
            else if (liste[curseur] == "cube")
            {
                G3D.Faire_carrer(joueur.position);
                Debug.Log("cube");
            }
            else if (liste[curseur] == "trou")
            {
                G3D.Faire_Trou(joueur.position);
                Debug.Log("trou");
            }
        }
    }
}
