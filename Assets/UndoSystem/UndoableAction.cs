using UnityEngine;

public class UndoableAction
{
    public Vector3 position;
    public int currentIndex;
    public int playerExitCount;

    public bool isreturn;
    public UndoableAction(Vector3 position,int currentIndex, bool isreturn)
    {
        this.position = position;
        this.currentIndex = currentIndex;
        this.isreturn = isreturn;
        playerExitCount = LevelManager.playerExitCount;

    }
    public static UndoableAction  MakeUndoableAction(Vector3 vec, int index)
    {
        UndoableAction undo = new UndoableAction(vec, index, false);
        undo.position = vec;
        undo.currentIndex = index;
     undo.playerExitCount = LevelManager.playerExitCount;
        return undo;
    }
    public static UndoableAction MakeUndoableAction(Vector3 vec, int index, bool isreturn)
    {
        UndoableAction undo = new UndoableAction(vec, index, isreturn);
        undo.position = vec;
        undo.currentIndex = index;
        return undo;
    }
    public string Write()
    {
        return"position : " +  position + " currentIndex : " + currentIndex + " isreturn :  " + isreturn + " playerExitCount : " + playerExitCount;
    }
}