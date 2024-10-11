using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verification : MonoBehaviour
{
    public GameObject[] collec;
    public GameObject sortie;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (collec[1] == null && collec[0] == null)
        {
            Instantiate(sortie, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

        }
        //for (int i = 0; i < collec.Length; i++)
        //{
            
        //}
    }
}
