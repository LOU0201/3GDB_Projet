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
        Stop,
        PhantomeJaune
    }
    public Type type;
    public string stringType;
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
    public Material PhantomeJaune;



    private void OnValidate()
    {
        switch (type)
        {
            case Type.Normal:
                childRenderer.sharedMaterial = Solide;
                gameObject.GetComponent<LevelManager>().enabled = false;
                stringType = "Normal";

                fin = false;
                break;
            case Type.PhantomeJaune:
                childRenderer.sharedMaterial = PhantomeJaune;
                gameObject.GetComponent<LevelManager>().enabled = false;
                stringType = "PhantomeJaune";

                fin = false;
                break;
            case Type.Phantome:
                childRenderer.sharedMaterial = Phantome;
                gameObject.GetComponent<LevelManager>().enabled = false;
                stringType = "Phantome";
                fin = false;
                break;
            case Type.Fin:
                childRenderer.sharedMaterial = Sortie;
                gameObject.GetComponent<LevelManager>().enabled = false;
                fin = true;
                stringType = "Fin";
                break;
            case Type.Debut:
                stringType = "Debut";
                childRenderer.sharedMaterial = Debut;
                gameObject.GetComponent<LevelManager>().enabled = true;
                fin = false;
                break;
            case Type.RedGhost:
                childRenderer.sharedMaterial = RedGhost;
                gameObject.GetComponent<LevelManager>().enabled = false;
                fin = false;
                stringType = "RedGhost";
                break;
            case Type.Stop:
                childRenderer.sharedMaterial = Stop;
                gameObject.GetComponent<LevelManager>().enabled = false;
                fin = false;
                stringType = "Stop";
                break;
        }
        if (temporaire)
        {
            transform.GetChild(3).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(3).gameObject.SetActive(false);
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
                this.type = Type.Stop;
                OnValidate();
                break;
            case "PhantomeJaune":
                this.type = Type.PhantomeJaune;
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
    public void Initialisation (bool phantome, bool fin, bool début, bool temporaire) 
    {
        this.phantome = phantome;
        this.fin = fin;
        this.début = début;
        this.temporaire = temporaire;
    }
}
