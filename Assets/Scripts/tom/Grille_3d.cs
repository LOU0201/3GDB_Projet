using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grille_3d : MonoBehaviour
{
    public GameObject joueur;
    public GameObject prefabBoite;
    public bool Blockeur = false;
    public Destructeur des;
    void Update()
    {

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
    public Boolean Estprit(Vector3 vec)// Si est libre, et vérifie si est fin pour pouvoir faire le Rapatriment  rend true si est libre
    {
        foreach (Transform t in this.transform)
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
        foreach (Transform t in this.transform)
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
        if (Blockeur)
        {
            return false;
        }
        foreach (Transform t in this.transform)//Atention les block sont toujours blockant, se n'est qu'ici que l'on sait si l'on respecte le caractaire blockant du block'
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
            if (t.GetComponent<Boite>().début)
            {
                joueur.transform.position = t.transform.position;
            }
        }
    }
    public bool est_temporaire(Vector3 vec)// si est un phantome
    {
        foreach (Transform t in this.transform)
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
                Boite b = child.GetComponent<Boite>();//Des boit donc
                b.libre = false;//Le cube est un obstacle
                b.transform.GetChild(1).gameObject.SetActive(true);//i est donc plein
                if (!Blockeur)
                {
                    b.transform.GetChild(0).transform.GetComponent<Renderer>().material.color = Color.yellow;
                }
                if (!Estprit_basique(vec + new Vector3(0, 1, 0)))//Si il y a un Block en haut, on passe, si non on fait ce-ci
                {
                    GameObject boite = Instantiate(prefabBoite, vec + new Vector3(0, 1, 0), Quaternion.identity);
                    Boite scriptboite = boite.GetComponent<Boite>();
                    scriptboite.transform.SetParent(this.transform);
                    scriptboite.Initialisation(true, false, false, false, false);//une boit libre donc
                }
            }
        }
    }
    public void Faire_Trou(Vector3 vec)
    {
        des.casse_bloc = true;
        foreach (Transform t in transform)
        {
            if (t.transform.position == vec + new Vector3(0, -1, 0))//On s'aintéresse ici, au bloc que l'on veux détruir c'est à dire celui juste endessous du joueur 
            {
                vec = new Vector3(0, -1, 0);//On baisse donc d'un crant
                //ON s'aintéressse en premier lieu à la boite du dessus'
                if (trouve_boit(vec + new Vector3(0, 1, 0)))//Si trouveBoite rend quelque chose 
                {//rend sa variable libre fausse, car il y n'y a plus de blocs en dessous'
                    Boite b = trouve_boit(vec + new Vector3(0, 1, 0));
                    b.Initialisation(false, false, false, false, false);
                }
                t.transform.GetChild(0).gameObject.SetActive(false);
                t.transform.GetChild(1).gameObject.SetActive(false);
                //joueur.transform.position = new Vector3(joueur.transform.position.x, joueur.transform.position.y-1f, joueur.transform.position.z);
                //ici, on s'aintéresse à la boite du dessous
                if (trouve_boit(t.transform.position + new Vector3(0, -1, 0)))//Si trouveBoite rend quelque chose
                {
                    if (!trouve_boit(t.transform.position + new Vector3(0, -1, 0)).libre)// et si ce quelque chose est une boite avec sa variable libre faus, alors fait sa
                    {
                        t.GetComponent<Boite>().Initialisation(true, false, false, false, false);//rend sa variable libre vrai (à la boite en question  (du milieu)), car il y a un blocs compacte en dessous
                        //joueur.GetComponent<Joueur>().ascention(joueur.transform.position);
                    }
                    else
                    {
                        t.GetComponent<Boite>().Initialisation(false, false, false, false, false);//rend sa variable libre fausse (à la boite en question  (du milieu)), car il n'y pas de blocs en dessous'
                    }
                }
            }
        }
    }
}