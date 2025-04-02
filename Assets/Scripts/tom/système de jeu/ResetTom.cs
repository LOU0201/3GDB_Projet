using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class ResetTom : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text scoreText2;
    public TMP_Text scoreText3;
    public TMP_Text scoreText4;
    public int playerScore = 0;
    public int minsortie;
    public int maxsortie;
    public bool Max= false;
    public GameObject écran;
    public Collectible collec;
    public bool annule = false;
    public bool _return = false;
    public ListeTom LT;
  
    void Start()
    {
        if(scoreText != null)
        {
            scoreText.text = "Sorties: " + playerScore.ToString() + "/" + minsortie.ToString();
            scoreText2.text = "Sorties Maximum: " + playerScore.ToString() + "/" + maxsortie.ToString();
            scoreText3.text = "Collectible: Non-obtenu";
            scoreText4.text = "Retour arriere: Non-utilise";
        }
        
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerScore == minsortie && Max)
        {
            écran.SetActive(true);
            Star();
        }
        if(collec!=null && collec.collecté)
        {
            scoreText3.text = "Collectible: Obtenu";
            Star();
            collec.collecté = false;
        }
        if(annule)
        {
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                _return = true;
                scoreText4.text = "Retour arriere: Utilise";
            }
        }
    }
    public void Rappatriment(Transform joueur)
    {
        LT.setIndex();
        playerScore += 1;
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
                Star();
                if(annule)
                {
                    CheckReturn();
                }
            }
            else
            {
                Debug.Log("INPUT DE FIN");
            }
        }
        if(playerScore == maxsortie)
        {
            écran.SetActive(true);
            Star();
            if (annule)
            {
                CheckReturn();
            }
        }

        if (playerScore >= maxsortie)
        {
            écran.SetActive(true);

            // Update Star Rating UI
            StarRating starSystem = écran.GetComponent<StarRating>();
            if (starSystem != null)
            {
                starSystem.UpdateStarRating();
            }
        }

        print("rapatrimenyyyyyyyyyyyt" + this.transform.position);
    }
    private void Star()
    {
        //GameManager.Instance.starsUp();
    }
    private void CheckReturn()
    {
        if(!_return)
        {
            Star();
        }
    }
}
