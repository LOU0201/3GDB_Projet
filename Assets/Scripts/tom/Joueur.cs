using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur : MonoBehaviour
{
    public int compte_carré = 0;
    public int variable_compte_carré = 3;
    public GameObject Update_grille3d;
    public Vector3 vec;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            vec=transform.position+ new Vector3(1, 0, 0);
            if (Update_grille3d.GetComponent<Grille_3d>().Estprit(vec))
            {
                this.transform.position += new Vector3(1, 0, 0);
                if(Update_grille3d.GetComponent<Grille_3d>().est_temporaire(vec))
                {
                    compte_carré++;
                }
            }
            Update_plus();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            vec=transform.position+ new Vector3(-1, 0, 0);
            if (Update_grille3d.GetComponent<Grille_3d>().Estprit(vec))
            {
                this.transform.position += new Vector3(-1, 0, 0);

                if(Update_grille3d.GetComponent<Grille_3d>().est_temporaire(vec))
                {
                    compte_carré++;
                }
            }
            Update_plus();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            vec=transform.position+ new Vector3(0, 0, 1);
            if (Update_grille3d.GetComponent<Grille_3d>().Estprit(vec))
            {
                this.transform.position += new Vector3(0, 0, 1);
                if(Update_grille3d.GetComponent<Grille_3d>().est_temporaire(vec))
                {
                    compte_carré++;
                }
            }
            Update_plus();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            vec=transform.position+ new Vector3(0, 0, -1);
            if (Update_grille3d.GetComponent<Grille_3d>().Estprit(vec))
            {
                this.transform.position += new Vector3(0, 0, -1);
                if(Update_grille3d.GetComponent<Grille_3d>().est_temporaire(vec))
                {
                    compte_carré++;
                }
            } 
            Update_plus();
        }
    }
    public void Update_plus()
    {
        if(compte_carré>=variable_compte_carré)
        {
            Update_grille3d.GetComponent<Grille_3d>().Faire_carrer(transform.position);
            compte_carré = 0;
            print("fais");
        }
        this.transform.position=Update_grille3d.GetComponent<Grille_3d>().fait_ascenseur(this.transform.position);
    }

}
