using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float étoiles = 0f;
    public ScriptableObject[] LevelsDataList;
    private int star;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    public void Hello_World()
    {
        Debug.Log("Hello World!");
    }

    public void Start()
    {
        foreach(LevelData niv in LevelsDataList)
        {
            star = 0;
            foreach (bool C in niv.objectivesCompleted)
            {
                if(C == true)
                {
                    star++;
                }
            }
            étoiles += star;
        }
    }
    public void starsUp()
    {
        étoiles += 1;
    }
}
