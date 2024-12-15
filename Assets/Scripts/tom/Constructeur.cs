using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Constructeur : MonoBehaviour
{
    public GameObject prefabBoite;
    public GameObject prefabEntrer;
    public GameObject Joueur;
    public bool isActive=false;
    public GameObject Update_grille3d;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
             this.transform.GetChild(0).gameObject.SetActive(true);
             Joueur.gameObject.SetActive(false);
             isActive= true;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.transform.position += new Vector3(1, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            this.transform.position += new Vector3(-1, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.transform.position += new Vector3(0, 0, 1);    
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.transform.position += new Vector3(0, 0, -1);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            this.transform.position += new Vector3(0, 1, 0);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            this.transform.position += new Vector3(0, -1, 0);
        }

        if (Input.GetKeyDown(KeyCode.E))//UNE ENTRER
        {
            if (!Update_grille3d.GetComponent<Grille_3d>().trouve_boit(this.transform.position) || Update_grille3d.GetComponent<Grille_3d>().trouve_boit(this.transform.position).libre)
            {
                GameObject entrer = Instantiate(prefabEntrer, this.transform.position, Quaternion.identity);
                entrer.transform.SetParent(Update_grille3d.transform);
                entrer.GetComponent<ResetTom>().cons = this;
                entrer.GetComponent<ResetTom>().joueur = this.Joueur.transform;
            }
        }

        if (Input.GetKeyDown(KeyCode.T))//UNE SORTIE
        {
            if (!Update_grille3d.GetComponent<Grille_3d>().trouve_boit(this.transform.position))
            {
                GameObject boite = Instantiate(prefabBoite, this.transform.position, Quaternion.identity);
                Boite scriptboite = boite.GetComponent<Boite>();
                boite.transform.SetParent(Update_grille3d.transform);
                scriptboite.Initialisation(false, false, false, false, false);
                boite.transform.GetComponent<Boite>().libre = true;
                boite.transform.GetComponent<Boite>().fin = true;
                boite.transform.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                Boite b = Update_grille3d.GetComponent<Grille_3d>().trouve_boit(this.transform.position);
                b.transform.GetChild(2).gameObject.SetActive(true);
                b.transform.GetComponent<Boite>().libre = true;
                b.transform.GetComponent<Boite>().fin = true;

            }

            if (!Update_grille3d.GetComponent<Grille_3d>().Estprit_basique(this.transform.position + new Vector3(0, 1, 0)))
            {
                GameObject boite1 = Instantiate(prefabBoite, this.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                Boite scriptboite1 = boite1.GetComponent<Boite>();
                scriptboite1.Initialisation(true, false, false, false, false);
                boite1.transform.SetParent(Update_grille3d.transform);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))  //Code de création
        {
            if(!Update_grille3d.GetComponent<Grille_3d>().trouve_boit(this.transform.position))
            {
                GameObject boite = Instantiate(prefabBoite,this.transform.position,Quaternion.identity);
                Boite scriptboite=boite.GetComponent<Boite>();
                boite.transform.SetParent(Update_grille3d.transform);
                scriptboite.Initialisation(false,false,false,false,false);
                boite.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                Boite b=Update_grille3d.GetComponent<Grille_3d>().trouve_boit(this.transform.position);
                b.transform.GetChild(0).gameObject.SetActive(true);
                b.transform.GetComponent<Boite>().libre = false;
            }
            
            if(!Update_grille3d.GetComponent<Grille_3d>().Estprit_basique(this.transform.position + new Vector3(0,1,0)))
            {
                GameObject boite1 = Instantiate(prefabBoite,this.transform.position + new Vector3(0,1,0),Quaternion.identity);
                Boite scriptboite1=boite1.GetComponent<Boite>();
                scriptboite1.Initialisation(true,false,false,false,false);
                boite1.transform.SetParent(Update_grille3d.transform);
            }
        }
        if (Input.GetKeyDown(KeyCode.G))//Code de destruction
        {
            foreach(Transform t in Update_grille3d.transform)
            {
                if (t.transform.position == this.transform.position)
                {                                                                                                                               //ON s'aintéressse en premier lieu à la boite du dessus'
                    if(Update_grille3d.GetComponent<Grille_3d>().trouve_boit(this.transform.position + new Vector3(0,1,0)))//Si trouveBoite rend quelque chose, alors fait sa
                    {//rend ça variable libre fausse(à la boite du dessus), car il y n'y a plus de blocs en dessous'
                        Boite b=Update_grille3d.GetComponent<Grille_3d>().trouve_boit(t.transform.position + new Vector3(0,1,0));
                            b.Initialisation(false,false,false,false,false);
                        }
                    t.transform.GetChild(0).gameObject.SetActive(false);//ici on s'aintéresse à la boite en question  
                    t.transform.GetChild(1).gameObject.SetActive(false);

                    //ici, on s'aintéresse à la boite du dessous
                    if (Update_grille3d.GetComponent<Grille_3d>().trouve_boit(this.transform.position + new Vector3(0,-1,0)))//Si trouveBoite rend quelque chose
                        {
                            if(!Update_grille3d.GetComponent<Grille_3d>().trouve_boit(this.transform.position + new Vector3(0,-1,0)).libre)// et si ce quelque chose est une boite avec sa variable libre faus, alors fait sa
                        {                                                                           //ici, on s'aintéresse à la boite en question (du milieu)
                            t.GetComponent<Boite>().Initialisation(true,false,false,false,false);//rend sa variable libre vrai(à la boite en question  (du milieu)), car il y a un blocs compacte en dessous
                        }
                        else
                                {
                                    t.GetComponent<Boite>().Initialisation(false,false,false,false,false);//rend sa variable libre fausse(à la boite en question (du milieu) ), car il n'y pas de blocs en dessous'
                        }
                        }
                }
            }
        } 
    }
}
