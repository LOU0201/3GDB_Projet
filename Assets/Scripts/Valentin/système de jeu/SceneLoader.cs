using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static void LoadScene(string scenename)
    {
        if (Object.FindObjectOfType<Loading_Screen_Manager>() == null)
        {
            // Charger l'�cran de chargement s'il n'est pas pr�sent
            SceneManager.LoadScene(Loading_Screen_Manager.loadingSceneName);
        }

        // Mettre � jour la sc�ne � charger
        Loading_Screen_Manager.sceneToLoadName = scenename;
    }
}
