using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Joueur : MonoBehaviour
{
    //Timer
    private float Timer_max=0.5f;
    private float Timer = -1;
    private bool isSleep=false;
    private int serecureInt = 0;
    private bool ReMoveEnCasquadeGo=false;

    public GameObject prefabBoite;

    //Refactorisation
    public bool debug=false;
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
    public Animator anims;
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
        // Récupérer la rotation Y de la caméra
        float cameraRotationY = cameraPrincipal.transform.rotation.eulerAngles.y;

        // Détermine les axes principaux basés sur la rotation
        Vector3 forward = new Vector3(Mathf.Round(Mathf.Sin(Mathf.Deg2Rad * cameraRotationY)), 0, Mathf.Round(Mathf.Cos(Mathf.Deg2Rad * cameraRotationY)));
        Vector3 right = new Vector3(Mathf.Round(Mathf.Cos(Mathf.Deg2Rad * cameraRotationY)), 0, -Mathf.Round(Mathf.Sin(Mathf.Deg2Rad * cameraRotationY)));
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UndoSystem.Instance.Reset();
            Scene scene = SceneManager.GetActiveScene();
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
        if (Input.GetKeyDown(KeyCode.I))
        {
            ReMove();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            ReMoveEnCasquade();
        }
        //Timer
        if (Timer > -1)
        {

            Timer += Time.deltaTime;
            if (Timer > Timer_max)
            {
                Timer = -2;
                isSleep = false;
                print("Une boucle temporrelle"+isSleep);
            }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Timer = 0;
            print("test?");
        }

        // ReMoveEnCasquadeGo
        if (ReMoveEnCasquadeGo)
        {
            if (!UndoSystem.Instance.isBoucle() && !UndoSystem.Instance.isFinich())
            {
                if (!isSleep)
                {
                    ReMove();
                    print("fais boucle");
                    sleep();
                }
                else
                {
                    print("fais pas boucle");
                    if (serecureInt > 100)
                    {
                        isSleep = false;
                        serecureInt = 0;

                    }
                    else
                    {
                        serecureInt++;
                    }
                }
            }
            else
            {
                ReMoveEnCasquadeGo = false;
                ReMove();

            }
        }
    }
    public void sleep()
    {
        Timer = 0;
        isSleep=true;
        print("isSleep");
    }

    public bool isDebut()//Ici, Luu
    {
        return Update_grille3d.trouve_boit(transform.position).equalType("Debut");
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
    public void ReMoveEnCasquade()
    {
        ReMoveEnCasquadeGo=true;

    }//Marche arrière 
    public void ReMove()//Marche arrière 
    {
        if (!UndoSystem.Instance.isFinich())
        {
            Update_grille3d.RemoveGrille();//ON rafraichie la grille
            undoableAction = UndoSystem.Instance.UndoAction();//ON prend la dernière action en mémoir
            transform.position = undoableAction.position;//ON change l'amplacement du joueur selon cette emplacement
            Liste.GetComponent<ListeTom>().setIndex(undoableAction.currentIndex);//ON change l'index selon l'ancienne index

            var boiteIci = Update_grille3d.trouve_boit(transform.position);//ON regarde au niveau de sa position
            if (boiteIci != null)
            {
                if (boiteIci.equalType("Normal")) /*si le cube est plein, on le transforme en phantome,*/
                {
                    boiteIci.SetType("Phantome");
                }
                if (boiteIci.equalType("Stop"))//SI le cube est Stop, on le transforme en phantomejaune
                {
                    boiteIci.SetType("PhantomeJaune");
                }
            }

            var boiteIciBas = Update_grille3d.trouve_boit(transform.position + new Vector3(0, -1, 0));//ON regarde à la position bas du joueur
            if (boiteIciBas == null)
            {
                GameObject boite = Instantiate(prefabBoite, transform.position + new Vector3(0, -1, 0), Quaternion.identity);
                boite.transform.SetParent(Update_grille3d.transform);
                if (debug)
                {
                    print("RE_BOITE");
                }
            }
        }
    }

    public void MovePlayer(Vector3 targetPosition)
    {
        if (Update_grille3d.isPlein(targetPosition))
        {
            if (Update_grille3d.isPlein(targetPosition+new Vector3(0,1,0)))
            {
                if (debug)
                {
                    Debug.Log("BLOQUE_MUR : " + (targetPosition + new Vector3(0, 1, 0)));
                }
            }
            else
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
                    if(Update_grille3d.isPlein(transform.position+new Vector3(0, 1, 0)))
                    {
                        if (debug)
                        {
                            Debug.Log("PLAFON_BLOQUANT : " + (transform.position + new Vector3(0, 1, 0)));
                        }
                    }
                    else
                    {
                        anims.SetTrigger("Climbing");
                        FMODUnity.RuntimeManager.PlayOneShot("event:/V3/Player/Climb");
                        surveillePhantome(Update_grille3d.trouve_boit(transform.position));
                        UndoSystem.Instance.RecordAction(UndoableAction.MakeUndoableAction(transform.position, Liste.GetComponent<ListeTom>().GetIndex()));
                        transform.position = (targetPosition + new Vector3(0, 1, 0));
                        Update_grille3d.refreche();
                        if (Update_grille3d.non_est_temporaire(targetPosition))
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
        }
        else 
        {
            if (Update_grille3d.isPlein(targetPosition + new Vector3(0, -1, 0)))
            {
               // anims.SetTrigger("Walking");
                surveillePhantome(Update_grille3d.trouve_boit(transform.position));
                UndoSystem.Instance.RecordAction(UndoableAction.MakeUndoableAction(transform.position, Liste.GetComponent<ListeTom>().GetIndex()));
                transform.position = (targetPosition);
                Update_grille3d.refreche();
                if ( Update_grille3d.non_est_temporaire(targetPosition + new Vector3(0, -1, 0)))
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
                  //  anims.SetTrigger("Descending");
                    surveillePhantome(Update_grille3d.trouve_boit(transform.position));
                    UndoSystem.Instance.RecordAction(UndoableAction.MakeUndoableAction(transform.position, Liste.GetComponent<ListeTom>().GetIndex()));
                    transform.position = (targetPosition + new Vector3(0, -1, 0));
                    Update_grille3d.refreche();
                    if ( Update_grille3d.non_est_temporaire(targetPosition + new Vector3(0, -2, 0)))
                    {

                        FMODUnity.RuntimeManager.PlayOneShot("event:/V3/Player/Land");
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
