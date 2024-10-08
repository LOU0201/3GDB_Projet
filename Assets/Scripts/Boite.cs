using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boite : MonoBehaviour
{
    public bool libre=true;
    public bool phantome = false;
    public bool fin = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Joueur")
        {

        }
    }
}
