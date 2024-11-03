using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grille_3d : MonoBehaviour
{
    public GameObject joueur;
    public GameObject prefabBoite;
    public bool Blockeur=false;
    void Update()
    {
        
    }
    public Boite trouve_boit(Vector3 vec)
    {
        foreach(Transform t in this.transform)
        {
            if (t.transform.position == vec)
            {                
                return t.GetComponent<Boite>();
            }
        }
        return null;
    }
    public Boolean Estprit(Vector3 vec)// Si est libre, et vérifie si est fin pour pouvoir faire le Rapatriment  rend true si est libre
    {
        foreach(Transform t in this.transform)
        {
            if (t.transform.position == vec)
            {                
                if (t.GetComponent<Boite>().fin)
                {
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
    public Boolean EstStop(Vector3 vec)// Si est libre, mais plus basique
    {
        if(Blockeur)
        {
            return false;
        }
        foreach(Transform t in this.transform)
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
        joueur.GetComponent<Joueur>().compte_carré = 0;
        foreach (Transform t in this.transform)
        {
            if(t.GetComponent<Boite>().début)
            {
                joueur.transform.position=t.transform.position;
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
    public void Faire_carrer(Vector3 vec)// Fais un phantome
    {
        foreach (Transform child in this.transform)
        {
            if (child.transform.position == vec && !child.transform.GetComponent<Boite>().fin)
            {
                child.transform.GetChild(0).gameObject.SetActive(true);
                child.transform.GetComponent<Boite>().libre = false;
                child.transform.GetComponent<Boite>().Stop = true;
                GameObject boite = Instantiate(prefabBoite,vec + new Vector3(0,1,0),Quaternion.identity);
                Boite scriptboite=boite.GetComponent<Boite>();
                boite.transform.SetParent(this.transform);
                scriptboite.Initialisation(true,false,false,false,false);
            }
        }
    }
}