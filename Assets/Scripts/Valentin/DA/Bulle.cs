using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulle : MonoBehaviour
{
    public GameObject apparence;
    public GameObject _parent;
    public Animator sortie;
    public float triggerDistance = 0.1f;

    // Update is called once per frame
    void Update()
    {
        Vector3 coordonnees = _parent.transform.position;
        Vector3 CJ = apparence.transform.position;
        if (Vector3.Distance(CJ, coordonnees) < triggerDistance)
        {
            sortie.SetTrigger("Exit");
            apparence.SetActive(false);
        }
    }
}
