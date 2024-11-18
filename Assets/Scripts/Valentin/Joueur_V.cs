using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur_V : MonoBehaviour
{
    public int compte_carré = 0;
    public int variable_compte_carré = 3;
    public GameObject Update_grille3d;
    public Vector3 vec;
    public int fonction;
    public bool LP;
    public Vector3 pos;
    public GameObject cam;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (cam.transform.rotation.eulerAngles.y <= 45f)
            {
                vec = transform.position + new Vector3(1, 0, 0);
                if (Update_grille3d.GetComponent<Grille_3d>().Estprit(vec))
                {
                    this.transform.position += new Vector3(1, 0, 0);
                    if (Update_grille3d.GetComponent<Grille_3d>().est_temporaire(vec))
                    {
                        compte_carré++;
                    }
                }
                else//Si le block n'est pas libre on fait ascention'
                {
                    ascention(vec);
                }
                Update_plus();
            }
            else
            {
                vec = transform.position + new Vector3(0, 0, -1);
                if (Update_grille3d.GetComponent<Grille_3d>().Estprit(vec))
                {
                    this.transform.position += new Vector3(0, 0, -1);
                    if (Update_grille3d.GetComponent<Grille_3d>().est_temporaire(vec))
                    {
                        compte_carré++;
                    }
                }
                else//Si le block n'est pas libre on fait ascention'
                {
                    ascention(vec);
                }
                Update_plus();
            }
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
            else//Si le block n'est pas libre on fait ascention'
            {
                ascention(vec);
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
            else//Si le block n'est pas libre on fait ascention'
            {
                ascention(vec);
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
            else//Si le block n'est pas libre on fait ascention'
            {
                ascention( vec);
            }
            Update_plus();
        }
    }
    public void ascention(Vector3 vec)//est charger de faire monter ou descendre le joueur si la boite n'est pas libre 
    //en vérifiant si la boite placer juste audessus puis c'elle audessous sont libre, si c'est le cas le joueur va dans cette boite alors
    {
        if (!Update_grille3d.GetComponent<Grille_3d>().EstStop(vec))
        {
            Vector3 vec_bas=vec + new Vector3(0,-1,0);
            if(Update_grille3d.GetComponent<Grille_3d>().Estprit(vec_bas))
            {
                 this.transform.position =vec_bas;
                    if(Update_grille3d.GetComponent<Grille_3d>().est_temporaire(vec_bas))
                    {
                        compte_carré++;
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
                        compte_carré++;
                    }
                }
            }
        }
    }
    public void Update_plus()
    {
        if (LP == false)
        {
            if (compte_carré >= variable_compte_carré)
            {
                fonction = Random.Range(1, 3);
                if (fonction == 1)
                {
                    Update_grille3d.GetComponent<Grille_3d>().Faire_carrer(transform.position);
                    compte_carré = 0;
                    //print("obstacle");
                }
                if (fonction == 2)
                {
                    Update_grille3d.GetComponent<Grille_3d>().Faire_Trou(transform.position);
                    compte_carré = 0;
                    //print("trou");
                }
            }
        }
    }
    public void TP()
    {
        transform.position = pos;
    }
}
