using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constructeur : MonoBehaviour
{
    public GameObject prefabBoite;
    public GameObject Joueur;
    public GameObject Update_grille3d;//Ne pas confondre Boite et Cube, Les boite sont des invisibles au Joueur, et les cubes sont les mures du jeu !!
    public GameObject GrilleBlock;//Block = cube
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            print("fait-A");
             this.transform.GetChild(0).gameObject.SetActive(true);
             Joueur.gameObject.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!Update_grille3d.GetComponent<Grille_3d>().Estprit_basique(this.transform.position))
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
        if (Input.GetKeyDown(KeyCode.G))
        {
            foreach(Transform t in GrilleBlock.transform)
            {
                print("fait");
                if (t.transform.position == this.transform.position)
                {             
                    Destroy(t.gameObject);
                }
            }
            foreach(Transform t in Update_grille3d.transform)
            {
                if (t.transform.position == this.transform.position + new Vector3(0,1,0))
                {                
                    Boite scriptboiteduhaut=t.GetComponent<Boite>();
                    scriptboiteduhaut.Initialisation(false,false,false,false,false);
                }
            }
        } 
    }
}
