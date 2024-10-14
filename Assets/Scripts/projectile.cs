using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    private Rigidbody rb;
    public float impulse = 2f;
    public GameObject tireur;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(tireur.transform.forward * impulse, ForceMode.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = tireur.transform.rotation;
        if (rb.IsSleeping() == true)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "CamIgnore")
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
