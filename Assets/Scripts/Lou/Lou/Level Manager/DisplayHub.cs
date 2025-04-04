using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DisplayHub : MonoBehaviour
{
    public LevelData levelData;
    public TMP_Text levelNameText;
    public TMP_Text[] challengeTexts = new TMP_Text[3];
    public Image[] starImages; // Array of star Image components

    void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        levelNameText.text = "Level: " + levelData.name;

        for (int i = 0; i < challengeTexts.Length; i++)
        {
            switch (levelData.objectiveTypes[i])
            {
                case ObjectiveType.CompleteLevel:
                    challengeTexts[i].text = "Complete Level";
                    break;
                case ObjectiveType.CollectCollectable:
                    challengeTexts[i].text = "Collect Collectable";
                    break;
                case ObjectiveType.NoUndo:
                    challengeTexts[i].text = "Don t use the undo";
                    break;
                case ObjectiveType.None:
                    challengeTexts[i].text = "mmm";
                    break;
                default:
                    break;
            }

            starImages[i].gameObject.SetActive(i < levelData.CountStarsUnlocked());
        }
    }
}
