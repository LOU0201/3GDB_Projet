using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grille_3d : MonoBehaviour
{
    public float y;
    public float sux;
    public float suz;
    public Destructeur des;
    public Joueur SJ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Boolean Estprit(Vector3 vec)
    {
        foreach(Transform t in this.transform)
        {
            if (t.transform.position == vec)
            {                
                if (t.GetComponent<Boite>().fin)
                {
                    Est_fin();
                    return true;
                }
                if (t.GetComponent<Boite>().libre)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public void Est_fin()
    {
        foreach (Transform t in this.transform)
        {
            if (t.GetComponent<Boite>().phantome)
            {
                t.GetComponent<Boite>().phantome = false;
                t.GetComponent<Boite>().libre = false;
            }
        }
    }
    public void Faire_carrer(Vector3 vec)
    {
        foreach (Transform child in this.transform)
        {
            if (child.transform.position == vec)
            {
                child.transform.GetChild(0).gameObject.SetActive(true);
                child.transform.GetComponent<Boite>().libre = false;
            }
        }
    }
    public void Faire_Trou(Vector3 vec)
    {
        des.casse_bloc = true;
        //var bloc = GameObject.Find("sol");
        //Vector3 vec2 = new Vector3(transform.position.x+sux, y, transform.position.z+suz);
        //foreach (bloc in LD.transform)
        //{
        //    if (bloc.transform.position == vec2)
        //    {
        //        Destroy(bloc);
        //        Destroy(this.gameObject);
        //    }
        //}
        //foreach (Transform child in this.transform)
        //{
        //    if (child.transform.position == vec)
        //    {
        //        Destroy(child.gameObject);
        //    }
        //}
    }
}
