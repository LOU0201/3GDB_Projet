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
        Temp,
        RedGhost,
        Stop
    }
    public Type type;
    private string stringType;
    public bool phantome = false;
    public bool fin = false;
    public bool début = false;
    public bool temporaire = false;
    public bool Stop = false;
    public bool phantomeRouge=false;
    public int valeur;
    public GameObject reTurne;

    public MeshRenderer childRenderer;
    public Material matA;
    public Material matB;
        
    private void OnValidate()
    {
        switch (type)
        {
            case Type.Normal:
                childRenderer.sharedMaterial = matA;
                stringType = "Normal";
                break;
            case Type.Phantome:
                childRenderer.sharedMaterial = matA;
                stringType = "Phantome";
                break;
            case Type.Fin:
                childRenderer.sharedMaterial = matB;
                stringType = "Fin";
                break;
            case Type.Debut:
                stringType = "Debut";
                break;
            case Type.Temp:
                stringType = "Temp";
                break;
            case Type.RedGhost:
                stringType = "RedGhost";
                break;
            case Type.Stop:
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
            case "Temp":
                this.type = Type.Temp;
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
