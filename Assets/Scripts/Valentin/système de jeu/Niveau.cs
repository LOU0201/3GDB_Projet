using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;

public class Niveau : MonoBehaviour
{
    public string LVname;
    public Transform joueur;
    public Transform[] alentours;
    public LevelData LD;
    public DisplayHub Display;
    public int MinExits;
    public StudioEventEmitter emitter;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 coordonnees = transform.position;
        Vector3 CJ = joueur.position;
        if (CJ == coordonnees)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/V3/Level/PlayerUseHatch");
            SceneLoader.LoadScene(LVname);
            UndoSystem.Instance.Reset();
            emitter.Stop();
        }

        foreach(Transform t in alentours)
        {
            Vector3 COT = t.transform.position;
            if (CJ == COT)
            {
                Transfert();
                if(Input.anyKey)
                {
                    if (CJ == COT)
                    {
                        Display.HideSheet();
                    }
                }
            }
        }
    }

    void Transfert()
    {
        Display.levelData = LD;
        Display.MinExitNum = MinExits;
        Display.UpdateUI();
    }
}
