using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boite : MonoBehaviour
{
    public bool libre=true;
    public bool phantome = false;
    public bool fin = false;
    public bool d�but = false;
    public bool temporaire = false;
    public bool Stop = false;
    public int valeur;
    public Transform joueur;
    public Material opaque;
    // Start is called before the first frame update
    void Start()
    {
        if (Stop)
        {
            transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
        if (fin)
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }
        if(temporaire)
        {
            transform.GetChild(3).gameObject.SetActive(true);
        }
        if (Stop)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void Initialisation (bool libre, bool phantome, bool fin, bool d�but, bool temporaire)
    {
        this.libre = libre;
        this.phantome = phantome;
        this.fin = fin;
        this.d�but = d�but;
        this.temporaire = temporaire;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 coordonnees = transform.position;
        //Vector3 CJ = joueur.position;
        //if (CJ != coordonnees)
        //{
        //    transform.GetChild(0).transform.GetComponent<Renderer>().material = opaque;
        //}
    }
}
