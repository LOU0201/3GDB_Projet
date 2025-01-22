using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetTom : MonoBehaviour
{
    public Transform joueur;
    public Constructeur cons;
    void Start()
    {
        if (cons.isActive)
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

        }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("Space");

        }
    }
    public void Rappatriment()
    {
        joueur.transform.position= this.transform.position;
        print("rapatrimenyyyyyyyyyyyt");
    }
}
