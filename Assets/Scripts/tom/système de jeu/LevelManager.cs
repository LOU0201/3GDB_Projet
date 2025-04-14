using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class LevelManager : MonoBehaviour
{
    [Header("Level Data")]
    public LevelData levelData;

    [Header("Challenge texts")]
    public TMP_Text scoreText;
    public TMP_Text scoreText3;
    public TMP_Text scoreText4;

    [Header("Min Max Sorties")]
    public int playerExitCount = 0;
    public int minExitCount;
    public int maxExitCount;
    public bool max = false;
    public GameObject screen;

    [Header("Collectible")]
    public Collectible collectable;
    public bool undoForbidden = false;
    public bool undoUsed = false;
    public GameObject spaceButton;
    public TMP_Text nextLevel;
    public ListeTom LT;

    void Start()
    {
        if (scoreText != null)
        {
            scoreText.text = "Sorties: " + playerExitCount.ToString() + "/" + maxExitCount.ToString();
            scoreText3.text = "0/1";
            scoreText4.text = "Retour arriere: Non-utilise";
        }
    }

    public void Update()
    {
        // if minExit is -1, only check max exit
        bool canComplete = minExitCount == -1
            ? playerExitCount >= maxExitCount
            : playerExitCount >= minExitCount && playerExitCount < maxExitCount;

        if (Input.GetKeyDown(KeyCode.Space) && canComplete)
        {
            screen.SetActive(true);
        }

        if (collectable != null && collectable.collected)
        {
            scoreText3.text = "1/1";
            collectable.collected = false;
        }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            undoUsed = true;
            scoreText4.text = "Retour arriere: Utilise";
        }
    }

    public void Rappatriment(Transform joueur)
    {
        LT.RefrecheIndex();
        playerExitCount += 1;
        joueur.transform.position = this.transform.position + new Vector3(0, 1, 0);

        if (scoreText != null)
        {
            scoreText.text = "Sorties: " + playerExitCount.ToString() + "/" + maxExitCount.ToString();
        }

        bool levelEnded = false;

        // Modified victory conditions
        if (minExitCount == -1)
        {
            if (playerExitCount >= maxExitCount)
            {
                screen.SetActive(true);
                levelEnded = true;
            }
        }
        else
        {
            if (playerExitCount == minExitCount && !max)
            {
                spaceButton.SetActive(true);
                nextLevel.gameObject.SetActive(true);
            }

            if (playerExitCount >= maxExitCount)
            {
                screen.SetActive(true);
                levelEnded = true;
            }
        }

        if (levelEnded)
        {
            CheckObjectives();
        }
    }

    public void CheckObjectives()
    {
        for (int i = 0; i < levelData.objectiveTypes.Length; i++)
        {
            ObjectiveType objectiveType = levelData.objectiveTypes[i];
            switch (objectiveType)
            {
                case ObjectiveType.CompleteLevel:
                    levelData.objectivesCompleted[i] = true;
                    break;
                case ObjectiveType.CollectCollectable:
                    if (collectable.collected == true)
                    {
                        levelData.objectivesCompleted[i] = true;
                    }
                    break;
                case ObjectiveType.NoUndo:
                    if (undoUsed == false)
                    {
                        levelData.objectivesCompleted[i] = true;
                    }
                    break;
                case ObjectiveType.MinExits:
                    // Skip min exit check if set to -1
                    if (minExitCount != -1)
                    {
                        levelData.objectivesCompleted[i] = playerExitCount >= minExitCount;
                    }
                    break;
                case ObjectiveType.MaxExits:
                    levelData.objectivesCompleted[i] = playerExitCount >= maxExitCount;
                    break;
            }
        }
    }
}