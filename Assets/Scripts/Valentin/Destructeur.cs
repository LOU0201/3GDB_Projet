using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructeur : MonoBehaviour
{
    public bool casse_bloc= false;
    public Transform joueur;
    public float tempsmax = 1f;
    public float temps = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Coordonnées que le destructeur doit suivre
        transform.position = new Vector3(joueur.position.x, 0.5f, joueur.position.z);
        //Lancement du timer quand la fonction destructrice s'active
        if (casse_bloc == true)
        {
            temps += 0.2f;
        }
        //Désactivation de la fonction destructrice quand le timer atteint sa limite
        if (temps >= tempsmax)
        {
            casse_bloc = false;
        }
        //Reset du timer
        if (casse_bloc == false)
        {
            temps = 0;
        }
    }
}
