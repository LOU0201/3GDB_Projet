using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotSpeed = 80f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float tmp = 0f;
        tmp = moveSpeed;
        if (Input.GetKey(KeyCode.Keypad8))
        {
            transform.Translate(Vector3.forward * tmp * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Keypad2))
        {
            transform.Translate(Vector3.back * tmp * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Keypad4))
        {
            transform.Translate(Vector3.left * tmp * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Keypad6))
        {
            transform.Translate(Vector3.right * tmp * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Keypad5))
        {
            transform.Translate(Vector3.up * tmp * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Keypad0))
        {
            transform.Translate(Vector3.down * tmp * Time.deltaTime);
        }
    }
}
