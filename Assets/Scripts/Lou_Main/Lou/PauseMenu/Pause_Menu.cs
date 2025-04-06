using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause_Menu : MonoBehaviour
{
    public GameObject PausePanel;
    public void Pause()
    {
        
        Time.timeScale = 0f;
    }

    public void Resume()
    {

    }
}
