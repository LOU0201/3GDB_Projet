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

            if (t.GetComponent<Boite>().phantomeRouge)
            {
                Destroy(t);
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
            if (t.transform.position == vec)
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
                if (t.GetComponent<Boite>().Stop)
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
        ResetTom.Rappatriment();
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
        foreach (Transform child in this.transform)//Je prend la liste des emphant de Grille_3d
        {
            if (child.transform.position == vec && !child.transform.GetComponent<Boite>().fin)
            {
                if (debug)
                {
                    Debug.Log("BOITE_FIN : " + (vec));
                }
            }
            if (child.transform.position == vec)
            {
                if (debug)
                {
                    Debug.Log("BOITE_INCONUE : " + (vec));
                }
            }
        }
        GameObject boite = Instantiate(prefabBoite, vec, Quaternion.identity);
        boite.transform.SetParent(this.transform);
        boite.GetComponent<Boite>().SetType("Normal");//une boit normal donc
        FMODUnity.RuntimeManager.PlayOneShot("event:/V2/Blocs/Place");
    }
    public void Faire_Trou(Vector3 vec) //Sur la position du joueur !!!!
    {
        print("FaireTroue: " + vec);
        FMODUnity.RuntimeManager.PlayOneShot("event:/V2/Blocs/Break");
        foreach (Transform t in transform)
        {
            if (t.transform.position == vec + new Vector3(0, -1, 0) && (!trouve_boit(vec).transform.GetComponent<Boite>().fin)) //On s'aintéresse ici, au bloc que l'on veux détruir c'est à dire celui juste endessous du joueur et on vérifie si le block ou l'on est est une sortie
            {
                t.gameObject.GetComponent<Boite>().SetType("RedGhost");
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