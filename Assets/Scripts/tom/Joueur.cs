using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur : MonoBehaviour
{
    public GameObject Liste;
    public int compte_carr�;
    public int variable_compte_carr� = 3;
    public GameObject Update_grille3d;
    public Vector3 vec;
    public int fonction;
    public bool LP;
    public Vector3 pos;
    public Camera cameraPrincipal; // Associez votre cam�ra ici via l'inspecteur
    public bool trou;
    public float Ygrav;
    private Rigidbody RB;
    // Start is called before the first frame update
    public void Start()
    {
        
        RB = GetComponent<Rigidbody>();
    }
    public int GetNextAction()
    {
        return fonction;  // Accesses the next action type stored in fonction
    }

    // Update is called once per frame
    public void Update()
    {
        // R�cup�rer la rotation Y de la cam�ra
        float cameraRotationY = cameraPrincipal.transform.rotation.eulerAngles.y;

        // D�termine les axes principaux bas�s sur la rotation
        Vector3 forward = new Vector3(Mathf.Round(Mathf.Sin(Mathf.Deg2Rad * cameraRotationY)), 0, Mathf.Round(Mathf.Cos(Mathf.Deg2Rad * cameraRotationY)));
        Vector3 right = new Vector3(Mathf.Round(Mathf.Cos(Mathf.Deg2Rad * cameraRotationY)), 0, -Mathf.Round(Mathf.Sin(Mathf.Deg2Rad * cameraRotationY)));

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            vec = transform.position + right;
            MovePlayer(vec);
            Update_grille3d.GetComponent<Grille_3d>().isFin(transform.position);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            vec = transform.position - right;
            MovePlayer(vec);
            Update_grille3d.GetComponent<Grille_3d>().isFin(transform.position);

        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            vec = transform.position + forward;
            MovePlayer(vec);
            Update_grille3d.GetComponent<Grille_3d>().isFin(transform.position);

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            vec = transform.position - forward;
            MovePlayer(vec);
            Update_grille3d.GetComponent<Grille_3d>().isFin(transform.position);

        }
        //if (transform.position == new Vector3(transform.position.x, Ygrav, transform.position.z))
        //{
        //    RB.useGravity = true;
        //}
        //else
        //{
        //    RB.useGravity = false;
        //}
    }
    public void ascention(Vector3 vec)//est charger de faire monter ou descendre le joueur si la boite n'est pas libre 
    //en v�rifiant si la boite placer juste audessus puis c'elle audessous sont libre, si c'est le cas le joueur va dans cette boite alors
    {
        if (!Update_grille3d.GetComponent<Grille_3d>().EstStop(vec))
        {

            Vector3 vec_bas =vec + new Vector3(0,-1,0);
            if(Update_grille3d.GetComponent<Grille_3d>().Estprit(vec_bas))
            {
                surveillePhantome(Update_grille3d.GetComponent<Grille_3d>().trouve_boit(transform.position));
                Update_grille3d.GetComponent<Grille_3d>().refreche();
                FMODUnity.RuntimeManager.PlayOneShot("event:/V2/Player/Drop");
                this.transform.position =vec_bas;
                if (Liste & Update_grille3d.GetComponent<Grille_3d>().est_temporaire(vec))
                {
                    Liste.GetComponent<ListeTom>().UpdateTom();//D�placement donc on lence la liste si n�sc�ssaire
                }
            }
            else
            {

                Vector3 vec_haut =vec + new Vector3(0,1,0);
                if(Update_grille3d.GetComponent<Grille_3d>().Estprit(vec_haut))
                {
                    surveillePhantome(Update_grille3d.GetComponent<Grille_3d>().trouve_boit(transform.position));
                    Update_grille3d.GetComponent<Grille_3d>().refreche();
                    FMODUnity.RuntimeManager.PlayOneShot("event:/V2/Player/Climb");
                    this.transform.position =vec_haut;
                    if (Liste & Update_grille3d.GetComponent<Grille_3d>().est_temporaire(vec))
                    {
                        Liste.GetComponent<ListeTom>().UpdateTom();//D�placement donc on lence la liste si n�sc�ssaire

                    }
                }
            }
            //Donc tous les testes ont �chouer, aussi bien Move Player que Assention 
        }
    }
    public void Update_plus()
    {
        //Atention LP active UpdateTom()
        if (LP == false)
        {
            if (compte_carr� >= variable_compte_carr� )
            {
                if (trou == false)
                {
                    fonction = 1;
                }
                else
                {
                    fonction= Random.Range(1, 3);
                }
                if (fonction == 1)
                {
                    Update_grille3d.GetComponent<Grille_3d>().Faire_carrer(transform.position);
                    compte_carr� = 0;

                    //print("obstacle");
                }
                if (fonction == 2)
                {
                    if (trou)
                    {
                        Update_grille3d.GetComponent<Grille_3d>().Faire_Trou(transform.position);
                        compte_carr� = 0;
                        //print("trou");
                    }
                    else
                    {
                        Update_grille3d.GetComponent<Grille_3d>().Faire_carrer(transform.position);
                        compte_carr� = 0;
                    }
                }
            }
        }
    }
    public void surveillePhantome(Boite b)
    {
        if (b.phantome)
        {
            b.transform.GetChild(0).gameObject.SetActive(true);
            b.transform.GetChild(1).gameObject.SetActive(false);
            b.phantome = false; 
        }
    }
    public void surveillePhantomeRouge (Boite b)
    {
        if (!b)
        {
            print("ErreurPhantomeRouge");
        }
        else
        {
            if (b.phantomeRouge)
            {
                b.transform.GetChild(4).gameObject.SetActive(false);
                b.phantomeRouge = false;
            }
        }
    }

    public void TP()
    {
        transform.position = pos;
    }
    void MovePlayer(Vector3 targetPosition)
    {
        if (Update_grille3d.GetComponent<Grille_3d>().Estprit(targetPosition))
        {
            surveillePhantome(Update_grille3d.GetComponent<Grille_3d>().trouve_boit(transform.position));
            Update_grille3d.GetComponent<Grille_3d>().refreche();
            transform.position = targetPosition;
            if (Liste & Update_grille3d.GetComponent<Grille_3d>().est_temporaire(targetPosition))
            {
                Liste.GetComponent<ListeTom>().UpdateTom();//D�placement donc on lence la liste si n�sc�ssaire
            }
        }
        else // Si le block n'est pas libre on fait ascension
        {
            ascention(targetPosition);
        }
        Update_plus();
    }
}
