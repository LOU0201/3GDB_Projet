using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur : MonoBehaviour
{
    public int compte_carré = 0;
    public int variable_compte_carré = 3;
    public GameObject Update_grille3d;
    private int x;
    private int z;
    private float tempx;
    private float tempz;
    public int fonction;
    public string Linput;
    // Start is called before the first frame update
    void Start()
    {
        Debut();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Update_grille3d.GetComponent<Grille_3d>().Estprit(transform.position+ new Vector3(1, 0, 0)))
            {
                Droite();
                compte_carré++;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            if (Update_grille3d.GetComponent<Grille_3d>().Estprit(transform.position + new Vector3(-1, 0, 0)))
            {
                Gauche();
                compte_carré++;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Update_grille3d.GetComponent<Grille_3d>().Estprit(transform.position + new Vector3(0, 0, 1)))
            {
                Haut();
                compte_carré++;
            }    
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Update_grille3d.GetComponent<Grille_3d>().Estprit(transform.position + new Vector3(0, 0, -1)))
            {
                Bas();
                compte_carré++;
            } 
        }
        if (compte_carré >= variable_compte_carré)
        {
            fonction = Random.Range(1, 3);
            if (fonction == 1)
            {
                Update_grille3d.GetComponent<Grille_3d>().Faire_carrer(transform.position);
                compte_carré = 0;
                print("obstacle");
            }
            if (fonction == 2)
            {
                Update_grille3d.GetComponent<Grille_3d>().Faire_Trou(transform.position);
                compte_carré = 0;
                print("trou");
            }
        }
    }
    public void Debut()
    {
        z = Random.Range(2, 16);
        tempz = z;
        tempz += 0.5f;
        x = Random.Range(1, 16);
        tempx = x;
        tempx += 0.5f;
        transform.position = new Vector3(tempx, 1.5f, tempz);
    }

    public void Haut()
    {
        this.transform.position += new Vector3(0, 0, 1);
        Linput = "haut";
    }
    public void Gauche()
    {
        this.transform.position += new Vector3(-1, 0, 0);
        Linput = "gauche";
    }
    public void Bas()
    {
        this.transform.position += new Vector3(0, 0, -1);
        Linput = "bas";

    }
    public void Droite()
    {
        this.transform.position += new Vector3(1, 0, 0);
        Linput = "droite";
    }
}
