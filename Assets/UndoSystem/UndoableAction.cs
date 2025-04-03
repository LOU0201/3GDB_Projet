using UnityEngine;

public class UndoableAction
{
    public Vector3 position;

    public UndoableAction(Vector3 position)
    {
        this.position = position;   
    }
}