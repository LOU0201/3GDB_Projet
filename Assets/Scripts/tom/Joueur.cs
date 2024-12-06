using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur : MonoBehaviour
{
    public GameObject Liste;
    public int compte_carré = 0;
    public int variable_compte_carré = 3;
    public GameObject Update_grille3d;
    public Vector3 vec;
    public int fonction;
    public bool LP;
    public Vector3 pos;
    public Camera cameraPrincipal; // Associez votre caméra ici via l'inspecteur
    public bool trou;
    // Start is called before the first frame update
    public int GetNextAction()
    {
        return fonction;  // Accesses the next action type stored in fonction
    }

    // Update is called once per frame
    void Update()
    {
        // Récupérer la rotation Y de la caméra
        float cameraRotationY = cameraPrincipal.transform.rotation.eulerAngles.y;

        // Détermine les axes principaux basés sur la rotation
        Vector3 forward = new Vector3(Mathf.Round(Mathf.Sin(Mathf.Deg2Rad * cameraRotationY)), 0, Mathf.Round(Mathf.Cos(Mathf.Deg2Rad * cameraRotationY)));
        Vector3 right = new Vector3(Mathf.Round(Mathf.Cos(Mathf.Deg2Rad * cameraRotationY)), 0, -Mathf.Round(Mathf.Sin(Mathf.Deg2Rad * cameraRotationY)));

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            vec = transform.position + right;
            MovePlayer(vec);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            vec = transform.position - right;
            MovePlayer(vec);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            vec = transform.position + forward;
            MovePlayer(vec);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            vec = transform.position - forward;
            MovePlayer(vec);
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
                surveillePhantome(Update_grille3d.GetComponent<Grille_3d>().trouve_boit(transform.position));
                this.transform.position =vec_bas;
                Liste.GetComponent<ListeTom>().UpdateTom();//Déplacement donc on lence la liste si néscéssaire

                if (Update_grille3d.GetComponent<Grille_3d>().est_temporaire(vec_bas))
                    {
                        compte_carré++;
                    }
            }
            else
            {
                Vector3 vec_haut=vec + new Vector3(0,1,0);
                if(Update_grille3d.GetComponent<Grille_3d>().Estprit(vec_haut))
                {
                    surveillePhantome(Update_grille3d.GetComponent<Grille_3d>().trouve_boit(transform.position));
                    this.transform.position =vec_haut;
                    Liste.GetComponent<ListeTom>().UpdateTom();//Déplacement donc on lence la liste si néscéssaire

                    if (Update_grille3d.GetComponent<Grille_3d>().est_temporaire(vec_haut))
                    {
                        compte_carré++;
                    }
                }
            }
            //Donc tous les testes ont échouer, aussi bien Move Player que Assention 
        }
    }
    public void Update_plus()
    {
        //Atention LP active UpdateTom()
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
                    if (trou)
                    {
                        Update_grille3d.GetComponent<Grille_3d_V>().Faire_Trou(transform.position);
                        compte_carré = 0;
                        //print("trou");
                    }
                    else
                    {
                        Update_grille3d.GetComponent<Grille_3d>().Faire_carrer(transform.position);
                        compte_carré = 0;
                    }
                }
            }
        }
    }
    public void surveillePhantome(Boite b)
    {
        print("surveille");
        if (!b.libre)
        {
            print("surveilletest");

            b.transform.GetChild(0).gameObject.SetActive(true);
            b.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    public void TP()
    {
        transform.position = pos;
    }
    void MovePlayer(Vector3 targetPosition)
    {
        if (Update_grille3d.GetComponent<Grille_3d>().Estprit(vec))
        {
            surveillePhantome(Update_grille3d.GetComponent<Grille_3d>().trouve_boit(transform.position));
            transform.position = targetPosition;
            Liste.GetComponent<ListeTom>().UpdateTom();//Déplacement donc on lence la liste si néscéssaire

            if (Update_grille3d.GetComponent<Grille_3d>().est_temporaire(vec))
            {
                compte_carré++;
            }
        }
        else // Si le block n'est pas libre on fait ascension
        {
            ascention(targetPosition);
        }

        Update_plus();
    }
}
