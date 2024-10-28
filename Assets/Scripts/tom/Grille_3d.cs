using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grille_3d : MonoBehaviour
{
    public GameObject joueur;

    void Update()
    {
        
    }
    public Boolean Estprit(Vector3 vec)// Si est libre, et vérifie si est fin pour pouvoir faire le Rapatriment  rend true si est libre
    {
        foreach(Transform t in this.transform)
        {
            if (t.transform.position == vec)
            {                
                if (t.GetComponent<Boite>().fin)
                {
                    Est_fin();
                    Rapatriment();
                    return false;
                }
                if (t.GetComponent<Boite>().libre)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public Boolean Estprit_basique(Vector3 vec)// Si est libre, mais plus basique
    {
        foreach(Transform t in this.transform)
        {
            if (t.transform.position == vec)
            {                
                if (t.GetComponent<Boite>().libre)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public void Rapatriment()// Rapatriment du joueur
    {
        joueur.GetComponent<Joueur>().compte_carré = 0;
        foreach (Transform t in this.transform)
        {
            if(t.GetComponent<Boite>().début)
            {
                joueur.transform.position=t.transform.position;
            }
        }
    }
    public void Est_fin()// Si est la fin
    {
        foreach (Transform t in this.transform)
        {
            if (t.GetComponent<Boite>().phantome)
            {
                t.GetComponent<Boite>().phantome = false;
                t.GetComponent<Boite>().libre = false;
                t.transform.GetChild(0).gameObject.SetActive(false);
                t.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }
    public bool est_temporaire(Vector3 vec)// si est un phantome
    {
        foreach(Transform t in this.transform)
        {
            if (t.transform.position == vec)
            {                
                return !t.GetComponent<Boite>().temporaire;
            }
        }
        return false;
    }
    public int est_acsenseur(Vector3 vec)
    {
        foreach(Transform t in this.transform)
        {
            if (t.transform.position == vec)
            {            
                return t.GetComponent<Boite>().acsenseur;
            }
        }
        return 0;
    }
    public Vector3 fait_ascenseur(Vector3 vec)
    {
        if(est_acsenseur(vec)==1)
        {
            print("monte");
            return vec += new Vector3(0, 1, 0);
        }
        else
        {
            if(est_acsenseur(vec)==-1)
            {
                return vec += new Vector3(0, -1, 0);
            }
        }
        return vec;
    }
    public void Faire_carrer(Vector3 vec)// Fais un phantome
    {
        foreach (Transform child in this.transform)
        {
            if (child.transform.position == vec && !child.transform.GetComponent<Boite>().fin)
            {
                child.transform.GetChild(0).gameObject.SetActive(true);
                child.transform.GetComponent<Boite>().phantome = true;
            }
        }
    }
}