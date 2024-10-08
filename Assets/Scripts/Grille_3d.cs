using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grille_3d : MonoBehaviour
{
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
}
