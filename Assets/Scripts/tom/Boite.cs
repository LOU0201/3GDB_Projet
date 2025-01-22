using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boite : MonoBehaviour
{
    public bool libre=true;
    public bool phantome = false;
    public bool fin = false;
    public bool début = false;
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

    public void Initialisation (bool libre, bool phantome, bool fin, bool début, bool temporaire)
    {
        this.libre = libre;
        this.phantome = phantome;
        this.fin = fin;
        this.début = début;
        this.temporaire = temporaire;
    }
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.position = reTurne.transform.position;
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
