using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoCheck : MonoBehaviour
{
    public Transform playerTsfm;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            UndoableAction previousAction = UndoSystem.Instance.UndoAction();
            if (previousAction != null)
            {
                playerTsfm.position = previousAction.position;
            }
        }
    }
}
