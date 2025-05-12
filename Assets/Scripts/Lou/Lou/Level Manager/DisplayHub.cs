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
    public Image[] starImages; 
    public int MinExitNum;
    public GameObject LevelInfo;
    private bool isShowing = false;

    private DisplayAnimation displayAnimation;
    void Start()
    {
        displayAnimation = LevelInfo.GetComponent<DisplayAnimation>();
        HideSheet();
    }
    public void UpdateUI()
    {
        if (isShowing) return;

        isShowing = true;
        LevelInfo.SetActive(true);
        Animator animator = LevelInfo.GetComponent<Animator>();

        levelNameText.text = " " + levelData.name;
        LevelInfo.GetComponent<Canvas>().enabled = true;

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
                    challengeTexts[i].text = "Didn’t use the undo button";
                    break;
                case ObjectiveType.MinExits:
                    challengeTexts[i].text = "Entered the exit " + MinExitNum + " times";
                    break;
                default:
                    challengeTexts[i].text = "Performed all possible exits";
                    break;
            }

            starImages[i].gameObject.SetActive(i < levelData.CountStarsUnlocked());
        }

        ShowSheet();
    }

    public void ShowSheet()
    {
        LevelInfo.SetActive(true);
        displayAnimation?.PlayEnterAnimation();
    }

    public void HideSheet()
    {
        isShowing = false;
        LevelInfo.SetActive(false);

        foreach (var cg in displayAnimation.uiElementsToShow)
        {
            if (cg != null) cg.alpha = 0;
        }
    }
}
