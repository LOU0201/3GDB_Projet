using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boite : MonoBehaviour
{

    public enum Type
    {
        Normal,
        Phantome,
        Fin,
        Debut,
        RedGhost,
        Stop
    }
    public Type type;
    private string stringType;
    public bool phantome = false;
    public bool fin = false;
    public bool début = false;
    public bool temporaire = false;
    public bool phantomeRouge=false;
    public int valeur;
    public GameObject reTurne;

    public MeshRenderer childRenderer;
    public Material Solide;
    public Material Phantome;
    public Material Sortie;
    public Material RedGhost;
    public Material Stop;
    public Material Debut;



    private void OnValidate()
    {
        switch (type)
        {
            case Type.Normal:
                childRenderer.sharedMaterial = Solide;
                gameObject.GetComponent<ResetTom>().enabled = false;
                stringType = "Normal";
                fin = false;
                break;
            case Type.Phantome:
                childRenderer.sharedMaterial = Phantome;
                gameObject.GetComponent<ResetTom>().enabled = false;
                stringType = "Phantome";
                fin = false;
                break;
            case Type.Fin:
                childRenderer.sharedMaterial = Sortie;
                gameObject.GetComponent<ResetTom>().enabled = false;
                fin = true;
                stringType = "Fin";
                break;
            case Type.Debut:
                stringType = "Debut";
                childRenderer.sharedMaterial = Debut;
                gameObject.GetComponent<ResetTom>().enabled = true;
                fin = false;
                break;
            case Type.RedGhost:
                childRenderer.sharedMaterial = RedGhost;
                gameObject.GetComponent<ResetTom>().enabled = false;
                fin = false;
                stringType = "RedGhost";
                break;
            case Type.Stop:
                childRenderer.sharedMaterial = Stop;
                gameObject.GetComponent<ResetTom>().enabled = false;
                fin = false;
                stringType = "Stop";
                break;
        }
    }

    public void SetType(string i)
    {
        switch (i)
        {
            case "Normal":
                this.type = Type.Normal;
                OnValidate();
                break;
            case "Phantome":
                this.type = Type.Phantome;
                OnValidate();
                break;
            case "Fin":
                this.type = Type.Fin;
                OnValidate();
                break;
            case "Debut":
                this.type = Type.Debut;
                OnValidate();
                break;
            case "RedGhost":
                this.type = Type.RedGhost;
                OnValidate();
                break;
            case "Stop":
                this.type= Type.Stop;
                OnValidate();
                break;
        }
    }
    public string getType() {
        return stringType;
    }
    public bool equalType(string i)
    {
        return String.Equals(i, stringType);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Initialisation (bool phantome, bool fin, bool début, bool temporaire) 
        //(Type newType)
    {
        //this.type = newType;
        this.phantome = phantome;
        this.fin = fin;
        this.début = début;
        this.temporaire = temporaire;
    }
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Call the Rappatriment() function on the ResetTom object
            if (reTurne != null) // Check if reTurne is assigned
            {
               // reTurne.GetComponent<ResetTom>().Rappatriment();
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
