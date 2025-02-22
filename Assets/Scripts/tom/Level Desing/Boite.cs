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
    public bool phantomeRouge=false;
    public int valeur;
    public GameObject reTurne;
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
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Call the Rappatriment() function on the ResetTom object
            if (reTurne != null) // Check if reTurne is assigned
            {
                reTurne.GetComponent<ResetTom>().Rappatriment();
            }
            else
            {
                Debug.LogError("reTurne is not assigned in Boite script.");
            }
        }
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
