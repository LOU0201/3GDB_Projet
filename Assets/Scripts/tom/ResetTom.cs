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
    public void Rappatriment()
    {
        joueur.transform.position=new Vector3(0,100,0);
        print("rapatrimenyyyyyyyyyyyt");
    }
}
