using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause_Menu : MonoBehaviour
{
    public GameObject pauseMenu; // The entire pause menu UI
    private Pause_menu_anim animator; // Reference to the animation script

    private bool isPaused = false;

    void Start()
    {
        animator = pauseMenu.GetComponent<Pause_menu_anim>(); // Get reference to the animator script
    }

    public void TogglePauseMenu()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/button");
        if (isPaused)
        {
            // Unpause the game and start fade-out before hiding the menu
            Time.timeScale = 1f;
            StartCoroutine(HidePauseMenu());
        }
        else
        {
            // Pause the game and show the menu
            Time.timeScale = 0f;
            pauseMenu.SetActive(true); // Activate the UI
            StartCoroutine(animator.FadeIn());
        }

        isPaused = !isPaused;
    }

    private IEnumerator HidePauseMenu()
    {
        // Start the fade-out animation
        yield return StartCoroutine(animator.FadeOut());

        // Wait for 2 seconds before deactivating the menu
        yield return new WaitForSeconds(2f);

        // After fade-out and wait, deactivate the menu
        pauseMenu.SetActive(false);
    }
}
