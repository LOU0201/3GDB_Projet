using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caméra : MonoBehaviour
{
    public Camera cam;
    public GameObject[] Lcam; // Liste des caméras
    private int index = 0; // Index de la caméra active

    void Start()
    {
        // Activer la première caméra
        if (Lcam.Length >= 0)
        {
            cam.transform.position = Lcam[0].transform.position;
            cam.transform.rotation = Quaternion.Euler(26.369f, 0f, cam.transform.rotation.z);
        }
    }

    void Update()
    {
        // Passer à la caméra précédente (A)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            cam.transform.position = Lcam[index].transform.position;
            // Calculer le nouvel index (cyclique)
            index = (index - 1 + Lcam.Length) % Lcam.Length;
        }

        // Passer à la caméra suivante (E)
        if (Input.GetKeyDown(KeyCode.E))
        {
            cam.transform.position = Lcam[index].transform.position;
            // Calculer le nouvel index (cyclique)
            index = (index + 1) % Lcam.Length;
        }
    }
}