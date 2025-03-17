using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float étoiles = 0f;

    private void Awake()
    {
        Instance = this;
    }
    public void Hello_World()
    {
        Debug.Log("Hello World!");
    }
    public void starsUp()
    {
        étoiles += 1;
    }
}
