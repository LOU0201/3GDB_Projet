using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUndo2 : MonoBehaviour
{
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            UndoSystem.Instance.RecordAction(new UndoableAction(transform.position));
            transform.position += transform.right * 2;
        }
    }
}
