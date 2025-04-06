using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Niveau : MonoBehaviour
{
    public string LVname;
    public Transform joueur;
    public Transform[] alentours;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 coordonnees = transform.position;
        Vector3 CJ = joueur.position;
        if (CJ == coordonnees)
        {
            SceneLoader.LoadScene(LVname);
        }

        foreach(Transform t in alentours)
        {
            Vector3 COT = t.transform.position;
            if (CJ == COT)
            {
                Debug.Log("ça marche");
            }
        }
    }
}
