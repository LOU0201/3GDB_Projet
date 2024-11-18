using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur_V : MonoBehaviour
{
    public int compte_carr� = 0;
    public int variable_compte_carr� = 3;
    public GameObject Update_grille3d;
    public Camera cameraPrincipal; // Associez votre cam�ra ici via l'inspecteur
    public Vector3 vec;
    public int fonction;
    public bool LP;
    public Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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

    void MovePlayer(Vector3 targetPosition)
    {
        if (Update_grille3d.GetComponent<Grille_3d>().Estprit(targetPosition))
        {
            transform.position = targetPosition;

            if (Update_grille3d.GetComponent<Grille_3d>().est_temporaire(targetPosition))
            {
                compte_carr�++;
            }
        }
        else // Si le block n'est pas libre on fait ascension
        {
            ascention(targetPosition);
        }

        Update_plus();
    }

    public void ascention(Vector3 vec)
    {
        if (!Update_grille3d.GetComponent<Grille_3d>().EstStop(vec))
        {
            Vector3 vec_bas = vec + new Vector3(0, -1, 0);
            if (Update_grille3d.GetComponent<Grille_3d>().Estprit(vec_bas))
            {
                transform.position = vec_bas;
                if (Update_grille3d.GetComponent<Grille_3d>().est_temporaire(vec_bas))
                {
                    compte_carr�++;
                }
            }
            else
            {
                Vector3 vec_haut = vec + new Vector3(0, 1, 0);
                if (Update_grille3d.GetComponent<Grille_3d>().Estprit(vec_haut))
                {
                    transform.position = vec_haut;
                    if (Update_grille3d.GetComponent<Grille_3d>().est_temporaire(vec_haut))
                    {
                        compte_carr�++;
                    }
                }
            }
        }
    }

    public void Update_plus()
    {
        if (LP == false)
        {
            if (compte_carr� >= variable_compte_carr�)
            {
                fonction = Random.Range(1, 3);
                if (fonction == 1)
                {
                    Update_grille3d.GetComponent<Grille_3d>().Faire_carrer(transform.position);
                    compte_carr� = 0;
                }
                if (fonction == 2)
                {
                    Update_grille3d.GetComponent<Grille_3d>().Faire_Trou(transform.position);
                    compte_carr� = 0;
                }
            }
        }
    }

    public void TP()
    {
        transform.position = pos;
    }
}
