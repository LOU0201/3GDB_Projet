using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus2 : MonoBehaviour
{
    public GameObject pos1;
    public GameObject pos2;
    public GameObject pos3;
    public GameObject pos4;
    public float angle;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position == pos1.transform.position)
        {
            transform.rotation = Quaternion.Euler(angle, -9.975f, transform.rotation.z);
        }
        if (transform.position == pos2.transform.position)
        {
            transform.rotation = Quaternion.Euler(angle, 77.044f, transform.rotation.z);
        }
        if (transform.position == pos3.transform.position)
        {
            transform.rotation = Quaternion.Euler(angle, 171.623f, transform.rotation.z);
            transform.position = pos3.transform.position;
        }
        if (transform.position == pos4.transform.position)
        {
            transform.rotation = Quaternion.Euler(angle, 252.688f, transform.rotation.z);
            transform.position = pos4.transform.position;
        }
    }
}
