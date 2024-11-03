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
    public int valeur;
    // Start is called before the first frame update
    void Start()
    {

        if (fin)
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }
        if(temporaire)
        {
            transform.GetChild(3).gameObject.SetActive(true);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
