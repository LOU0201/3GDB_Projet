using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TP : MonoBehaviour
{
    public Transform joueur;
    public Joueur scriptJ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 coordonnees = transform.position;
        Vector3 CJ = joueur.position;
        if (CJ == coordonnees)
        {
            scriptJ.TP();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        scriptJ.TP();
    }
}
