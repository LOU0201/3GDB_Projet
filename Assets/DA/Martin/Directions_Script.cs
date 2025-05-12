using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Directions_Script : MonoBehaviour
{
    [SerializeField]private GameObject[] Player;
    [SerializeField] private GameObject OOB_Pos;
    [SerializeField] private int index;

    void Start()
    {
        foreach (GameObject i in Player)
        {
            i.GetComponent<SpriteRenderer>().enabled = false;
            Debug.Log(i + " has been disabled");           
        }
        Player[0].GetComponent<SpriteRenderer>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            index += 1;
            if (index > 3)
            {
                index = 0;
            }

            ChangeDirections();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            index -= 1;
            if (index < 0)
            {
                index = 3;
            }

            ChangeDirections();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            index = 3;
            ChangeDirections();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            index = 1;
            ChangeDirections();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            index = 2;
            ChangeDirections();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            index = 0;
            ChangeDirections();
        }
    }

    public void ChangeDirections()
    {
        foreach (GameObject i in Player)
        {
            i.GetComponent<SpriteRenderer>().enabled = false;
        }

        Player[index].GetComponent<SpriteRenderer>().enabled = true;
    }
}
