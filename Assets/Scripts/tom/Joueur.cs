using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur : MonoBehaviour
{
    public int compte_carr� = 0;
    public int variable_compte_carr� = 3;
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
                    compte_carr�++;
                }
            }
                        else//Si le block n'est pas libre on fait ascention'
            {
                ascention( vec);
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
                    compte_carr�++;
                }
            }
                        else//Si le block n'est pas libre on fait ascention'
            {
                ascention( vec);
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
                    compte_carr�++;
                }
            }
                        else//Si le block n'est pas libre on fait ascention'
            {
                ascention( vec);
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
                    compte_carr�++;
                }
            } 
            else//Si le block n'est pas libre on fait ascention'
            {
                ascention( vec);
            }
            Update_plus();
        }
    }
    public void ascention(Vector3 vec)//est charger de faire monter ou descendre le joueur si la boite n'est pas libre 
    //en v�rifiant si la boite placer juste audessus puis c'elle audessous sont libre, si c'est le cas le joueur va dans cette boite alors
    {
        Vector3 vec_bas=vec + new Vector3(0,-1,0);
        if(Update_grille3d.GetComponent<Grille_3d>().Estprit(vec_bas))
        {
             this.transform.position =vec_bas;
                if(Update_grille3d.GetComponent<Grille_3d>().est_temporaire(vec_bas))
                {
                    compte_carr�++;
                }
        }
        else
        {
            Vector3 vec_haut=vec + new Vector3(0,1,0);
            if(Update_grille3d.GetComponent<Grille_3d>().Estprit(vec_haut))
            {
                this.transform.position =vec_haut;
                if(Update_grille3d.GetComponent<Grille_3d>().est_temporaire(vec_haut))
                {
                    compte_carr�++;
                }
            }
        }
    }
    public void Update_plus()
    {
        if(compte_carr�>=variable_compte_carr�)
        {
            Update_grille3d.GetComponent<Grille_3d>().Faire_carrer(transform.position);
            compte_carr� = 0;
            print("fais");
        }
    }

}