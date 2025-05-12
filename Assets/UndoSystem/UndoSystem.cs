using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoSystem : MonoBehaviour
{
    private static UndoSystem instance;

    public static UndoSystem Instance//C'est un Getter GetInstance()
    {
        get
        {
            if(instance == null)
            {
                //Get or Create
                instance = FindAnyObjectByType<UndoSystem>();
                if (instance == null)
                {
                    instance = new GameObject("UndoSystem").AddComponent<UndoSystem>();
                    DontDestroyOnLoad(instance.gameObject);
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
    }


    public  Stack<UndoableAction> undoStack = new();

    public void Reset()
    {
        undoStack = new();
    }

    public  bool isFinich()
    {
        return undoStack.Count == 0;
    }

    public void RecordAction(UndoableAction action)
    {
        action.playerExitCount= LevelManager.playerExitCount;
        print("RecordAction: " + action.Write());
        undoStack.Push(action);
    }
    public  bool isBoucle()
    {
        if (undoStack.Count!=0)
        {
            return undoStack.Peek().isreturn;

        }
        else
        {
            return false;
        }
    }
    public UndoableAction UndoAction()
    {
        print("UndoAction");
        if (undoStack.Count > 0)
        {
            return undoStack.Pop();
        }
        else
        {
            return null;
        }
    }
}
