using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckReset : MonoBehaviour
{
    public bool reset;
    public GameObject bande;
    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
        if (!reset)
        {
            GetComponent<TMP_Text>().enabled = true;
            bande.SetActive(true);
        }
        else
        {
            GetComponent<TMP_Text>().enabled = false;
            bande.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
