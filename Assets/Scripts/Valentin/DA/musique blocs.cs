using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musiqueblocs : MonoBehaviour
{
    public string[] Lpath;
    private int index;
    private int ajout;
    // Start is called before the first frame update
    void Start()
    {
        index = Random.Range(0, 8);
    }

    // Update is called once per frame
    public void Note()
    {
        FMODUnity.RuntimeManager.PlayOneShot(Lpath[index]);
        if (index == 0)
        {
            index = 1;
        }
        if (index >= 7)
        {
            index = 6;
        }
        else
        {
            ajout = Random.Range(-1, 2);
            index += ajout;
        }
    }
}
