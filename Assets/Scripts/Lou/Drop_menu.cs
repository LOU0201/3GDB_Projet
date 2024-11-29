using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Drop_menu : MonoBehaviour
{
    public TMP_Dropdown dropdown; // Reference to the Dropdown
    public string[] sceneNames;  // Optional scene names

    void Start()
    {
        dropdown.onValueChanged.AddListener(HandleDropdownChange);
    }

    private void HandleDropdownChange(int index)
    {
        if (sceneNames.Length > 0 && index < sceneNames.Length)
        {
            // Use scene name if available
            Debug.Log($"Loading scene: {sceneNames[index]}");
            SceneManager.LoadScene(sceneNames[index]);
        }
        else
        {
            // Use build index as fallback
            Debug.Log($"Loading scene by build index: {index}");
            SceneManager.LoadScene(index);
        }
    }

    void OnDestroy()
    {
        dropdown.onValueChanged.RemoveListener(HandleDropdownChange);
    }
}
