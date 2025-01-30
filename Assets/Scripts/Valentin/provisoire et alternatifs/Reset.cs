using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    //public Transform joueur;
    //[SerializeField] public bool exit;
    //public Joueur_V scriptJ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 coordonnees = transform.position;
        //Vector3 CJ = joueur.position;
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
            //scriptJ.Debut();
        }
    }
    void RestartScene()
    {
        // Get the current scene's index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Reload the current scene
        SceneManager.LoadScene(currentSceneIndex);
    }
}
