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
    public int MinExitNum= 0;

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
                    challengeTexts[i].text = "Didn t use the undo button";
                    break;
                case ObjectiveType.MinExits:
                    challengeTexts[i].text = "Entered the exit "+ MinExitNum + " times";
                    break;
                default:
                    challengeTexts[i].text = "Performed all possible exits";
                    break;
            }

            starImages[i].gameObject.SetActive(i < levelData.CountStarsUnlocked());
        }
    }
}
