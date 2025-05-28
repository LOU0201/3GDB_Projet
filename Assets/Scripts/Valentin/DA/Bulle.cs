using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulle : MonoBehaviour
{
    public GameObject apparence;
    public GameObject _parent;
    public Animator sortie;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 coordonnees = _parent.transform.position;
        Vector3 CJ = apparence.transform.position;
        if (CJ == coordonnees)
        {
            sortie.SetTrigger("Exit");
            apparence.SetActive(false);
        }
    }
}
