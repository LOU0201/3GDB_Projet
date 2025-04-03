using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }
    }
    public void RestartScene()
    {
        // Get the current scene's index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Reload the current scene
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void HUB()
    {
        SceneLoader.LoadScene("HUB");
    }
    public void MainMenu()
    {
        SceneLoader.LoadScene("MainMenu");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
