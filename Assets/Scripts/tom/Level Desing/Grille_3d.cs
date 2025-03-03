using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Grille_3d : MonoBehaviour
{
    //Refactorisation
    public bool debug = false;


    public GameObject joueur;
    public GameObject prefabBoite;
    public bool Non_Blockeur = false;
    public Destructeur des;
    public ResetTom ResetTom;
    public GameObject prefabCubeRouge;
    public ListeTom listeTom;
    public float CS;
    private void Start()
    {

    }
    void Update()
    {

    }
    public void refreche()
    {
        foreach (Transform t in this.transform)
        {

            if (t.GetComponent<Boite>().equalType("RedGhost"))
            {
                Destroy(t.gameObject);
            }
        }
    }
    public void isFin(Vector3 vec)
    {
        foreach (Transform t in this.transform)
        {
            if (t.transform.position == vec)
            {
                if (t.GetComponent<Boite>().fin)
                {
                    Debug.Log("Player reached the end!");
                    Rapatriment();
                }
            }
        }
    }
    public Boite trouve_boit(Vector3 vec)//Rend la boite au niveaux du vecteur demander, Atention peux rendre null
    {
        foreach (Transform t in this.transform)
        {
            if (t.transform.position == vec)
            {
                return t.GetComponent<Boite>();
            }
        }
        return null;
    }

    public Boolean isPlein(Vector3 vec)
    {
        foreach (Transform t in this.transform)
        {
            if (t.transform.position == vec && !t.gameObject.GetComponent<Boite>().equalType("Fin"))
            {
                return true;
            }

        }
        return false;
    }


    public Boolean EstStop(Vector3 vec)
    {
        if (Non_Blockeur)
        {
            return false;
        }
        foreach (Transform t in this.transform)
        {
            if (t.transform.position == vec)
            {
                if (t.GetComponent<Boite>().equalType("Stop"))
                {
                    return true;
                }
            }
        }
        return false;
    }
    public void Rapatriment()// Rapatriment du joueur
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/V1/System/leveldone");
        foreach (Transform t in this.transform)
        {
            if (t.gameObject.GetComponent<Boite>().equalType("Debut"))
            {
                t.gameObject.GetComponent<ResetTom>().Rappatriment(joueur.transform);
            }
        }
        if (debug)
        {
            Debug.Log("ResetListe");
        }
        CS++;
        listeTom.setIndex();
    }
    public bool est_temporaire(Vector3 vec)// si N'est pas un freez padh
    {
        foreach (Transform t in this.transform)
        {
            if (t.transform.position == vec)
            {
                return !t.GetComponent<Boite>().temporaire;
            }
        }
        return true;
    }
    public void Faire_carrer(Vector3 vec)// Fais un phantome(un obstacle donc) Sur la position du joueur !!!!
    {
        if (trouve_boit(vec))
        {
            if (trouve_boit(vec).equalType("Fin"))
            {
                if (debug)
                {
                    Debug.Log("BOITE_FIN : " + vec);
                }
            }
            else
            {
                if (debug)
                {
                    Debug.Log("ERREURE_BOITE_INCONNUE : " + vec);
                }
            }
        }
        else
        {
            GameObject boite = Instantiate(prefabBoite, vec, Quaternion.identity);
            boite.transform.SetParent(this.transform);
            boite.GetComponent<Boite>().SetType("Phantome");//une boit normal donc
            FMODUnity.RuntimeManager.PlayOneShot("event:/V2/Blocs/Place");
            print("fais_cube : " + vec);
        }
    }
    public void Faire_Trou(Vector3 vec) //Sur la position du joueur !!!!
    {
        print("FaireTroue: " + vec);
        FMODUnity.RuntimeManager.PlayOneShot("event:/V2/Blocs/Break");
        foreach (Transform t in transform)
        {
            if (t.transform.position == vec + new Vector3(0, -1, 0))
            {
                if (!(trouve_boit(vec) && trouve_boit(vec).transform.GetComponent<Boite>().fin))//On s'aintéresse ici, au bloc que l'on veux détruir c'est à dire celui juste endessous du joueur et on vérifie si le block ou l'on est est une sortie
                {
                    t.gameObject.GetComponent<Boite>().SetType("RedGhost");
                    if (debug)
                    {
                        Debug.Log("RedGhost : " + (vec));
                    }
                }
                else
                {
                    if (t.transform.position == vec + new Vector3(0, -1, 0))
                    {
                        if (debug)
                        {
                            Debug.Log("FIN : " + (vec));
                        }
                    }
                    else
                    {
                        if (debug)
                        {
                            Debug.Log("ERREURE_Boite_absente : " + (vec));
                        }
                    }
                }
            }

        }
    }
}
