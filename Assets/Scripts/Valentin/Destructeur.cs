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
        transform.position = new Vector3(joueur.position.x, 0.5f, joueur.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position = new Vector3(joueur.position.x-1, 0.5f, joueur.position.z);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(joueur.position.x+1, 0.5f, joueur.position.z);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position = new Vector3(joueur.position.x, 0.5f, joueur.position.z-1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position = new Vector3(joueur.position.x, 0.5f, joueur.position.z+1);
        }
        if (casse_bloc == true)
        {
            temps += 0.2f;
        }
        if (temps >= tempsmax)
        {
            casse_bloc = false;
        }
        if (casse_bloc == false)
        {
            temps = 0;
        }
    }
}
