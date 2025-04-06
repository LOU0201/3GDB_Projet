using UnityEngine;

public class UndoableAction
{
    public Vector3 position;
    public int currentIndex;
    public UndoableAction(Vector3 position,int currentIndex)
    {
        this.position = position;
        this.currentIndex = currentIndex;
    }
}