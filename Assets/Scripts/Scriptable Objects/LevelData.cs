using UnityEngine;


[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    //public string levelName;
    public ObjectiveType[] objectiveTypes = new ObjectiveType[3];
    public bool[] objectivesCompleted = new bool[3];

    public int CountStarsUnlocked()
    {
        int starsUnlocked = 0;
        for (int i = 0; i < objectivesCompleted.Length; i++)
        {
            if (objectivesCompleted[i] == true)
            {
                starsUnlocked++;
            }
        }
        return starsUnlocked;
    }
}

