using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetTom : MonoBehaviour
{
    public Transform joueur;
    //public Joueur_V scriptJ;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }
    public void Rappatriment()
    {
        joueur.transform.position=transform.position;

    }
}
