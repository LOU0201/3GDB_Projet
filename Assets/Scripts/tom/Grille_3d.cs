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
    public Boite trouve_boit(Vector3 vec)//Rend la boite au niveaux du vecteur demander, Atention peux rendre null
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
    public Boolean Estprit_basique(Vector3 vec)// Si est libre, mais plus basique que Estprit
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
        foreach(Transform t in this.transform)//Atention les block sont toujours blockant, se n'est qu'ici que l'on sait si l'on respecte le caractaire blockant du block'
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
    public void Faire_carrer(Vector3 vec)// Fais un phantome(un obstacle donc)
    {
        foreach (Transform child in this.transform)//Je prend la liste des emphant de Grille_3d
        {
            if (child.transform.position == vec && !child.transform.GetComponent<Boite>().fin)
            {
                Boite b=child.GetComponent<Boite>();//Des boit donc
                b.libre = false;//Le cube est un obstacle
                b.transform.GetChild(1).gameObject.SetActive(true);//i est donc plein
                b.Stop = true;//il ne peux pas être montée
                if(!Estprit_basique(vec + new Vector3(0,1,0)))//Si il y a un Block en haut, on passe, si non on fait ce-ci
                {
                    GameObject boite = Instantiate(prefabBoite,vec + new Vector3(0,1,0),Quaternion.identity);
                    Boite scriptboite=boite.GetComponent<Boite>();
                    scriptboite.transform.SetParent(this.transform);
                    scriptboite.Initialisation(true,false,false,false,false);//une boit libre donc
                }
            }
        }
    }
    public void Faire_Trou(Vector3 vec){
        return;
    }
}