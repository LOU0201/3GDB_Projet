using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    public Transform target;
    public GameObject pos1;
    public GameObject pos2;
    public GameObject pos3;
    public GameObject pos4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position = new Vector3(transform.position.x, target.position.y+ 8.81111f, transform.position.z);
        if (transform.position == pos1.transform.position)
        {
            transform.rotation = Quaternion.Euler(26.369f, 0f, transform.rotation.z);
        }
        if (transform.position == pos2.transform.position)
        {
            transform.rotation = Quaternion.Euler(26.369f, 90f, transform.rotation.z);
        }
        if (transform.position == pos3.transform.position)
        {
            transform.rotation = Quaternion.Euler(26.369f, 180f, transform.rotation.z);
        }
        if (transform.position == pos4.transform.position)
        {
            transform.rotation = Quaternion.Euler(26.369f, 270f, transform.rotation.z);
        }
    }
}
