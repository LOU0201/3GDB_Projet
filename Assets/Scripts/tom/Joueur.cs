using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Joueur : MonoBehaviour
{
    //Refactorisation
    public bool debug=true;
    UndoableAction undoableAction;
    public GameObject Liste;
    public int compte_carré;
    public int variable_compte_carré = 3;
    public Grille_3d Update_grille3d;
    public Vector3 vec;
    public int fonction;
    public bool LP;
    public Vector3 pos;
    public Camera cameraPrincipal; // Associez votre caméra ici via l'inspecteur
    public bool trou;
    public float Ygrav;
    private Rigidbody RB;
    // Start is called before the first frame update
    public void Start()
    {
        print("Debug :  " + debug); 
        debug = true;  
        RB = GetComponent<Rigidbody>();
    }
    public int GetNextAction()
    {
        return fonction;  // Accesses the next action type stored in fonction
    }

    // Update is called once per frame
    public void Update()
    {
        // Récupérer la rotation Y de la caméra
        float cameraRotationY = cameraPrincipal.transform.rotation.eulerAngles.y;

        // Détermine les axes principaux basés sur la rotation
        Vector3 forward = new Vector3(Mathf.Round(Mathf.Sin(Mathf.Deg2Rad * cameraRotationY)), 0, Mathf.Round(Mathf.Cos(Mathf.Deg2Rad * cameraRotationY)));
        Vector3 right = new Vector3(Mathf.Round(Mathf.Cos(Mathf.Deg2Rad * cameraRotationY)), 0, -Mathf.Round(Mathf.Sin(Mathf.Deg2Rad * cameraRotationY)));
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Scene scene= SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            vec = transform.position + right;
            MovePlayer(vec);
            Update_grille3d.isFin(transform.position);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            vec = transform.position - right;
            MovePlayer(vec);
            Update_grille3d.isFin(transform.position);

        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            vec = transform.position + forward;
            MovePlayer(vec);
            Update_grille3d.isFin(transform.position);

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            vec = transform.position - forward;
            MovePlayer(vec);
            Update_grille3d.isFin(transform.position);

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
    public bool isDebut()//Ici, Luu
    {
        return Update_grille3d.trouve_boit(transform.position).equalType("Debut");
    }
    public void Update_plus()
    {
        //Atention LP active UpdateTom()
        if (LP == false)
        {
            if (compte_carré >= variable_compte_carré )
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
                    Update_grille3d.Faire_carrer(transform.position);
                    compte_carré = 0;

                    //print("obstacle");
                }
                if (fonction == 2)
                {
                    if (trou)
                    {
                        Update_grille3d.Faire_Trou(transform.position);
                        compte_carré = 0;
                        //print("trou");
                    }
                    else
                    {
                        Update_grille3d.Faire_carrer(transform.position);
                        compte_carré = 0;
                    }
                }
            }
        }
    }
    public void surveillePhantome(Boite b)
    {
        if(b != null)
        {
            print("Boite : " + b.transform.position);
            print("Boite = " +  b.getType());
            if (debug)
            {
                Debug.Log("fais_surveillePhantome");
            }
            if (b.equalType("Phantome"))
            {
                b.gameObject.GetComponent<Boite>().SetType("Normal");
                print("fais Phantome");
            }
            else
            {
                if (b.equalType("PhantomeJaune"))
                {
                    print("fais PhantomeJaune");
                    b.gameObject.GetComponent<Boite>().SetType("Stop");
                }
                print("rien============");
            }
        }
    }
    public void TP()
    {
        transform.position = pos;
    }

    public void ReMove()
    {

    }

    void MovePlayer(Vector3 targetPosition)
    {
        if (Update_grille3d.isPlein(targetPosition))
        {
            if (Update_grille3d.isPlein(targetPosition+new Vector3(0,1,0)))
            {
                if (debug)
                {
                    Debug.Log("BLOQUE_MUR : " + (targetPosition + new Vector3(0, 1, 0)));
                }
            }else
            {
                if (Update_grille3d.EstStop(targetPosition))
                {
                    if (debug)
                    {
                        Debug.Log("BLOQUE_BLOQUANT : " + (targetPosition));
                    }
                }
                else
                {
                    surveillePhantome(Update_grille3d.trouve_boit(transform.position));
                    undoableAction.position=transform.position; 
                    UndoSystem.Instance.RecordAction(undoableAction);
                    transform.position = (targetPosition + new Vector3(0, 1, 0));
                    Update_grille3d.refreche();
                    if (Liste & Update_grille3d.non_est_temporaire(targetPosition))
                    {
                        Liste.GetComponent<ListeTom>().UpdateTom();//Déplacement donc on lence la liste si néscéssaire
                    }
                    if (debug)
                    {
                        Debug.Log("AVANCE_HAUT : " + (targetPosition + new Vector3(0, 1, 0)));
                    }
                }
            }
        }
        else 
        {
            if (Update_grille3d.isPlein(targetPosition + new Vector3(0, -1, 0)))
            {
                surveillePhantome(Update_grille3d.trouve_boit(transform.position));
                undoableAction.position = transform.position;
                UndoSystem.Instance.RecordAction(undoableAction);
                transform.position = (targetPosition);
                Update_grille3d.refreche();
                if (Liste & Update_grille3d.non_est_temporaire(targetPosition + new Vector3(0, -1, 0)))
                {
                    Liste.GetComponent<ListeTom>().UpdateTom();//Déplacement donc on lence la liste si néscéssaire
                }
                if (debug)
                {
                    Debug.Log("AVANCE : " + (targetPosition));
                }
            }
            else
            {
                if (Update_grille3d.isPlein(targetPosition + new Vector3(0, -2, 0)))
                {
                    surveillePhantome(Update_grille3d.trouve_boit(transform.position));
                    undoableAction.position = transform.position;
                    UndoSystem.Instance.RecordAction(undoableAction);
                    transform.position = (targetPosition + new Vector3(0, -1, 0));
                    Update_grille3d.refreche();
                    if (Liste & Update_grille3d.non_est_temporaire(targetPosition + new Vector3(0, -2, 0)))
                    {
                        Liste.GetComponent<ListeTom>().UpdateTom();//Déplacement donc on lence la liste si néscéssaire
                    }
                    if (debug)
                    {
                        Debug.Log("AVANCE_BAS : " + (targetPosition + new Vector3(0, -1, 0)));
                    }
                }
                else
                {
                    if (debug)
                    {
                        Debug.Log("BLOQUE_TROU : " + (targetPosition + new Vector3(0, -1, 0)));
                    }
                }
            }
        }
    }
}
