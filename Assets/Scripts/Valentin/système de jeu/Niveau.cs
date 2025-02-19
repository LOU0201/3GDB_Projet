using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Niveau : MonoBehaviour
{
    public string name;
    public Transform joueur;
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
            SceneManager.LoadScene(name);
        }
    }
}
