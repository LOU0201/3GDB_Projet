using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caméra : MonoBehaviour
{
    public float speed = 100f;
    private float RotationX;
    private float hauteur;
    [SerializeField] private Camera cam;
    private float zoom;
    private float ZoomMultiplier= 2f;
    public float minZoom= 2f;
    public float maxZoom= 8f;
    private float velocity= 0f;
    private float smoothTime= 0.25f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        //zoom = cam.orthographicSize;
        rb= GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        rb.velocity += transform.forward * scroll;
        rb.velocity -= transform.up * scroll;
        //zoom -= scroll * ZoomMultiplier;
        //zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        //cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);
        if (Input.GetMouseButton(0))
        {
            RotationX = Input.GetAxis("Mouse X");
            //hauteur = Input.GetAxis("Mouse Y");
            transform.Rotate(0, RotationX * speed * Time.deltaTime, 0);
            //transform.Translate(0, hauteur * 2 * Time.deltaTime, 0);
        }
    }
}