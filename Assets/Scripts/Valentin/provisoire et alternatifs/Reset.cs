using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;

public class Reset : MonoBehaviour
{
    public StudioEventEmitter emitterAStopper;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
            emitterAStopper.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(5);
            emitterAStopper.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(4);
            emitterAStopper.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene(7);
            emitterAStopper.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SceneManager.LoadScene(6);
            emitterAStopper.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SceneManager.LoadScene(14);
            emitterAStopper.Stop();
        }
    }
    public void RestartScene()
    {
        emitterAStopper.Stop();
        // Get the current scene's index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Reload the current scene
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void HUB()
    {
        emitterAStopper.Stop();
        SceneLoader.LoadScene("HUB");
        GameManager.Instance.étoiles = 0;
        GameManager.Instance.starsUp();
    }
    public void MainMenu()
    {
        emitterAStopper.Stop();
        SceneLoader.LoadScene("MainMenu");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
