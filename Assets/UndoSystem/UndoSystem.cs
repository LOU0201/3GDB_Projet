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


    public Stack<UndoableAction> undoStack = new();

    private void Start()
    {
        ///Exemple d'utilisation :
        UndoSystem.Instance.RecordAction(new UndoableAction(Vector3.zero,0));
        //UndoableAction action = UndoSystem.Instance.UndoAction(); 
    }

    public bool isFinich()
    {
        return undoStack.Count > 0;
    }

    public void RecordAction(UndoableAction action)
    {
        print("RecordAction");
        undoStack.Push(action);
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
