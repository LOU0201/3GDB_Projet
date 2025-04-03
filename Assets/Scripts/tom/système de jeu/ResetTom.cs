using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class ResetTom : MonoBehaviour
{
    [Header("Challenge texts")]
    public TMP_Text scoreText;
    public TMP_Text scoreText3;
    public TMP_Text scoreText4;

    [Header("Min Max Sorties")]
    public int playerScore = 0;
    public int minsortie;
    public int maxsortie;
    public bool Max= false;
    public GameObject écran;

    [Header("Collectible")]
    public Collectible collec;
    public bool annule = false;
    public bool _return = false;
    public GameObject Space_bouton;
    public TMP_Text next_level;
    public ListeTom LT;
    public GameObject collectible_UI;

    void Start()
    {
        if(scoreText != null)
        {
            scoreText.text = "Sorties: " + playerScore.ToString() + "/" + maxsortie.ToString();
            scoreText3.text = "0/1";
            scoreText4.text = "Retour arriere: Non-utilise";
        }
        
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerScore == minsortie || Max)
        {
            écran.SetActive(true);
           
        }
        if(collec!=null && collec.collecté)
        {
            scoreText3.text = "1/1";
            collectible_UI.gameObject.SetActive(true);
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
        playerScore+= 1;
        joueur.transform.position = this.transform.position+new Vector3(0,1,0);
        if (scoreText != null) 
        {
            scoreText.text = "Sorties: " + playerScore.ToString() + "/" + maxsortie.ToString();
        }

        if (playerScore == minsortie)
        {
            if (!Max)
            {
                Space_bouton.SetActive(true);
                next_level.gameObject.SetActive(true);
               
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
    }
    private void CheckReturn()
    {
        if(!_return)
        {
            
        }
    }
}
