using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUndo : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            UndoSystem.Instance.RecordAction(new UndoableAction(transform.position,0));
            transform.position += transform.forward;
        }
    }
}
