using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResetTom : MonoBehaviour
{
    public Transform joueur;
    public Constructeur cons;
    public TMP_Text scoreText;

    private int playerScore = 0;
    public Popup popUpText;
    void Start()
    {
        if (cons.isActive)
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

        popUpText = GetComponent<Popup>();

    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("Space");

        }
    }
    public void Rappatriment()
    {
        joueur.transform.position = this.transform.position; ;
        playerScore++;
        scoreText.text = "Score: " + playerScore.ToString();
        //popUpText.ShowPopUpText("+1");
        print("rapatrimenyyyyyyyyyyyt");
    }
}
