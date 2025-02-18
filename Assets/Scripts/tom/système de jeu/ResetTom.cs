using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResetTom : MonoBehaviour
{
    public TMP_Text scoreText;

    private int playerScore = 0;
    public Popup popUpText;
    void Start()
    {


      //  popUpText = GetComponent<Popup>();

    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("Space");

        }
    }
    public void Rappatriment(Transform joueur)
    {
        joueur.transform.position = this.transform.position; ;
        playerScore++;
      //  scoreText.text = "Score: " + playerScore.ToString();
        //popUpText.ShowPopUpText("+1");
        print("rapatrimenyyyyyyyyyyyt");
    }
}
