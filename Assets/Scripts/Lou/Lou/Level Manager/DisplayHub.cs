using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DisplayHub : MonoBehaviour
{
    public LevelData levelData;
    public TMP_Text levelNameText;
    public TMP_Text challenge1Text;
    public TMP_Text challenge2Text;
    public TMP_Text challenge3Text;
    public Image[] starImages; // Array of star Image components

    void Start()
    {
        if (levelData != null && levelNameText != null && challenge1Text != null && challenge2Text != null && challenge3Text != null && starImages != null)
        {
            levelNameText.text = "Level: " + levelData.levelName;
            challenge1Text.text = "Challenge 1: " + levelData.challenges[0];
            challenge2Text.text = "Challenge 2: " + levelData.challenges[1];
            challenge3Text.text = "Challenge 3: " + levelData.challenges[2];

            // Activate star images based on stars achieved
            for (int i = 0; i < starImages.Length; i++)
            {
                starImages[i].gameObject.SetActive(i < levelData.starsAchieved);
            }
        }
    }
}
