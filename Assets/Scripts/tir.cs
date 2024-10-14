using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tir : MonoBehaviour
{
    public int munitions= 10;
    public GameObject projectile;
    public GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = parent.transform.rotation;
        if (Input.GetKeyDown(KeyCode.Space) && munitions > 0)
        {
            Instantiate(projectile, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            munitions-= 1;
        }
    }
}
