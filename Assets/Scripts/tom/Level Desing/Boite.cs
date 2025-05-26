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
    public Material[] Solides;
    public Material Solide;
    public Material Phantome;
    public Material Sortie;
    public Material RedGhost;
    public Material Stop;
    public Material Debut;
    public Material PhantomeJaune;
    public GameObject Trigger;
    public GameObject trappe;
    public GameObject bulle;
    public int theme;


    private void OnValidate()
    {
        switch (type)
        {
            case Type.Normal:
                childRenderer.enabled = true;
                childRenderer.sharedMaterial = Solides[theme];
                gameObject.GetComponent<LevelManager>().enabled = false;
                stringType = "Normal";
                if (Trigger && trappe && bulle)
                {
                    Trigger.SetActive(false);
                    trappe.SetActive(false);
                    bulle.SetActive(false);
                }
                fin = false;
                break;
            case Type.PhantomeJaune:
                childRenderer.enabled = true;
                childRenderer.sharedMaterial = PhantomeJaune;
                gameObject.GetComponent<LevelManager>().enabled = false;
                stringType = "PhantomeJaune";
                fin = false;
                break;
            case Type.Phantome:
                childRenderer.enabled = true;
                childRenderer.sharedMaterial = Phantome;
                gameObject.GetComponent<LevelManager>().enabled = false;
                stringType = "Phantome";
                fin = false;
                break;
            case Type.Fin:
                childRenderer.enabled = false;
                gameObject.GetComponent<LevelManager>().enabled = false;
                fin = true;
                stringType = "Fin";
                if (Trigger && trappe && bulle)
                {
                    Trigger.SetActive(false);
                    trappe.SetActive(false);
                    bulle.SetActive(true);
                }
                break;
            case Type.Debut:
                stringType = "Debut";
                childRenderer.enabled = true;
                childRenderer.sharedMaterial = Solides[theme];
                gameObject.GetComponent<LevelManager>().enabled = true;
                if (Trigger && trappe && bulle)
                {
                    Trigger.SetActive(true);
                    trappe.SetActive(true);
                    bulle.SetActive(false);
                }
                fin = false;
                break;
            case Type.RedGhost:
                childRenderer.enabled = true;
                childRenderer.sharedMaterial = RedGhost;
                gameObject.GetComponent<LevelManager>().enabled = false;
                if (Trigger && trappe && bulle)
                {
                    Trigger.SetActive(false);
                    trappe.SetActive(false);
                    bulle.SetActive(false);
                }
                fin = false;
                stringType = "RedGhost";
                break;
            case Type.Stop:
                childRenderer.enabled = true;
                childRenderer.sharedMaterial = Stop;
                gameObject.GetComponent<LevelManager>().enabled = false;
                if (Trigger && trappe && bulle)
                {
                    Trigger.SetActive(false);
                    trappe.SetActive(false);
                    bulle.SetActive(false);
                }
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
