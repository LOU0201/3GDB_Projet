using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sol : MonoBehaviour
{
    public Destructeur des;
    public Transform casse;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 coordonnees = transform.position;
        Vector3 CC = casse.position;
        if (CC == coordonnees && des.casse_bloc == true)
        {
            Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }

}
