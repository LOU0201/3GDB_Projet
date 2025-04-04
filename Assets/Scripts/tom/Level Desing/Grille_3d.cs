using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Grille_3d : MonoBehaviour
{
    //Refactorisation
    public bool debug1 = false;

    public GameObject joueur;
    private GameObject prefabBoite;
    public bool CubeJaune;
    public GameObject prefabBoiteNormale;
    public GameObject prefabBoiteJaune;
    public bool Non_Blockeur = false;
    public Destructeur des;
    public LevelManager ResetTom;
    public GameObject prefabCubeRouge;
    public ListeTom listeTom;
    public float CS;
    private void Start()
    {
        if (CubeJaune)
        {
            prefabBoite = prefabBoiteJaune;
        }
        else
        {
            prefabBoite = prefabBoiteNormale;
        }
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
                    Rapatriment();
                }
            }
        }
    }//debug
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
        foreach(Transform t in this.transform)
        {
            if (t.gameObject.GetComponent<Boite>().equalType("Debut"))
            {
                t.gameObject.GetComponent<LevelManager>().Rappatriment(joueur.transform);
            }
        }
        if (debug1)
        {
            Debug.Log("ResetListe");
        }
        CS++;
        listeTom.RefrecheIndex();
    }
    public bool non_est_temporaire(Vector3 vec)// si N'est pas un freez padh
    {
        foreach (Transform t in this.transform)
        {
            if (t.transform.position == vec)
            {
                if (t.GetComponent<Boite>().temporaire)
                {
                    return false;
                }
                else
                {
                    if (t.GetComponent<Boite>().equalType("Debut") || t.GetComponent<Boite>().equalType("Fin"))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
        print("Erreure non_est_temporaire ne trouve pas de cube");
        return true;
    }

    public void RemoveGrille()
    {
        foreach (Transform t in this.transform)
        {

            if (t.GetComponent<Boite>().equalType("RedGhost"))
            {
                t.GetComponent<Boite>().SetType("Normal");
            }

            if (t.GetComponent<Boite>().equalType("Phantome"))
            {
                Destroy(t.gameObject);
            }
        }
    }

    public void Faire_carrer(Vector3 vec)// Fais un phantome(un obstacle donc) Sur la position du joueur !!!!
    {
        if (trouve_boit(vec))
        {
            if (trouve_boit(vec).equalType("Fin"))
            {
                if (debug1)
                {
                    Debug.Log("BOITE_FIN : " + vec);
                }
            }else
            {
                if (debug1)
                {
                    Debug.Log("ERREURE_BOITE_INCONNUE : " + vec);
                }
            }
        }else 
        {
                GameObject boite = Instantiate(prefabBoite, vec, Quaternion.identity);
                boite.transform.SetParent(this.transform);
                FMODUnity.RuntimeManager.PlayOneShot("event:/V2/Blocs/Place");
                print("fais_cube : "+vec);
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
                    if (debug1)
                    {
                        Debug.Log("RedGhost : " + (vec));
                    }
                }
                else
                {
                    if (t.transform.position == vec + new Vector3(0, -1, 0))
                    {
                        if (debug1)
                        {
                            Debug.Log("FIN : " + (vec));
                        }
                    }
                    else
                    {
                        if (debug1)
                        {
                            Debug.Log("ERREURE_Boite_absente : " + (vec));
                        }
                    }
                }
            }
        }
    }
}