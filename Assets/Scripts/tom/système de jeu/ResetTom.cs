using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResetTom : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text scoreText2;
    public TMP_Text scoreText3;
    public int playerScore = 0;
    public int minsortie;
    public int maxsortie;
    public Popup popUpText;
    public bool Max= false;
    public GameObject écran;
    public Collectible collec;
    void Start()
    {
        if(scoreText != null)
        {
            scoreText.text = "Sorties: " + playerScore.ToString() + "/" + minsortie.ToString();
            scoreText2.text = "Sorties Maximum: " + playerScore.ToString() + "/" + maxsortie.ToString();
            scoreText3.text = "Collectible: Non-obtenu";
        }

    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerScore == minsortie && Max)
        {
            écran.SetActive(true);
        }
        if(collec!=null && collec.collecté)
        {
            scoreText3.text = "Collectible: Obtenu";
        }
    }
    public void Rappatriment(Transform joueur)
    {
        playerScore+= 1;
        joueur.transform.position = this.transform.position+new Vector3(0,1,0);
        if (scoreText != null) 
        {
            scoreText.text = "Sorties: " + playerScore.ToString() + "/" + minsortie.ToString();
            scoreText2.text = "Sorties Maximum: " + playerScore.ToString() + "/" + maxsortie.ToString();
        }

        if (playerScore == minsortie)
        {
            if (!Max)
            {
                écran.SetActive(true);
            }
            else
            {
                Debug.Log("INPUT DE FIN");
            }
        }
        if(playerScore == maxsortie)
        {
            écran.SetActive(true);
        }
        //popUpText.ShowPopUpText("+1");
        print("rapatrimenyyyyyyyyyyyt" + this.transform.position);
    }
}
