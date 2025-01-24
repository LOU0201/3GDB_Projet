using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grille_3d : MonoBehaviour
{
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
        foreach (Transform t in this.transform)
        {
            if (t.GetComponent<Boite>())
            {
                Instantiate(prefabCubeRouge).transform.SetParent(t.transform, false);
            }
        }
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
                t.GetComponent<Boite>().phantomeRouge=false;
                t.transform.GetChild(4).gameObject.SetActive(false);//i est donc plein
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
    public Boolean Estprit(Vector3 vec)// Si est libre,   rend true si est libre
    {
        bool var=false;
        foreach (Transform t in this.transform)
        {
            if (t.transform.position == vec)
            {
                if (!t.GetComponent<Boite>().libre)
                {
                    return false;
                }
                if (t.GetComponent<Boite>().libre)
                {
                    var =true;
                }
            }
        }
        if (var) { return true; }
        return false;
    }
    public Boolean Estprit_basique(Vector3 vec)//Même chose
    {
        bool var = false;
        foreach (Transform t in this.transform)
        {
            if (t.transform.position == vec)
            {
                if (!t.GetComponent<Boite>().libre)
                {
                    return false;
                }
                if (t.GetComponent<Boite>().libre)
                {
                    var = true;
                }
            }
        }
        if (var) { return true; }
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
        //listeTom.setIndex();
    }
    public bool est_temporaire(Vector3 vec)// si n'est pas un freez padh
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
    public void Faire_carrer(Vector3 vec)// Fais un phantome(un obstacle donc) Sur la position du joueur !!!!
    {
        foreach (Transform child in this.transform)//Je prend la liste des emphant de Grille_3d
        {
            if (child.transform.position == vec && !child.transform.GetComponent<Boite>().fin)
            {
                Boite b = child.GetComponent<Boite>();//Des boit donc
                b.libre = false;//Le cube est un obstacle
                b.phantome = true;
                b.transform.GetChild(1).gameObject.SetActive(true);//i est donc plein
                if (!Non_Blockeur)
                {
                    b.transform.GetChild(0).transform.GetComponent<Renderer>().material.color = Color.yellow;
                    b.GetComponent<Boite>().Stop = true;
                }
                if (!Estprit_basique(vec + new Vector3(0, 1, 0)))//Si il y a un Block en haut, on passe, si non on fait ce-ci
                {
                    GameObject boite = Instantiate(prefabBoite, vec + new Vector3(0, 1, 0), Quaternion.identity);
                    Boite scriptboite = boite.GetComponent<Boite>();
                    scriptboite.transform.SetParent(this.transform);
                    scriptboite.Initialisation(true, false, false, false, false);//une boit libre donc
                    FMODUnity.RuntimeManager.PlayOneShot("event:/V1/Gameplay/blockplace");
                }
            }
        }
    }
    public void Faire_Trou(Vector3 vec) //Sur la position du joueur !!!!
    {
        print("FaireTroue: " + vec);
        FMODUnity.RuntimeManager.PlayOneShot("event:/V1/Gameplay/blockbreak");
        des.casse_bloc = true;
        foreach (Transform t in transform)
        {
            if (t.transform.position == vec + new Vector3(0, -1, 0) && (!trouve_boit(vec).transform.GetComponent<Boite>().fin)) //On s'aintéresse ici, au bloc que l'on veux détruir c'est à dire celui juste endessous du joueur et on vérifie si le block ou l'on est est une sortie
            {
                Vector3 vec2 = vec+new Vector3(0, -1, 0);//On baisse donc d'un crant
                //ON s'aintéressse en premier lieu à la boite du dessus'
                if (trouve_boit(vec2 + new Vector3(0, 1, 0)))//Si trouveBoite rend quelque chose 
                {//rend sa variable libre fausse, car il y n'y a plus de blocs en dessous'
                    Boite b = trouve_boit(vec2 + new Vector3(0, 1, 0));
                    b.Initialisation(false, false, false, false, false);
                }
                //Ici, on s'aintéresse au cube que l'on veux détruire
                //donc on éfface le cube
                t.GetComponent<Boite>().phantomeRouge = true;
                t.transform.GetChild(4).gameObject.SetActive(true);
                t.transform.GetChild(0).gameObject.SetActive(false);
                t.transform.GetChild(1).gameObject.SetActive(false);
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