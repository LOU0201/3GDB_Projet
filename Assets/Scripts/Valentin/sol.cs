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
        // D�finition des coordonn�es du joueur et du destructeur
        Vector3 coordonnees = transform.position;
        Vector3 CC = casse.position;
        if (CC == coordonnees && des.casse_bloc == true)
        {
            //L'objet est d�truit si les coordonn�es du destructeur sont les m�mes que celle du blocs et si sa fonction
            //de destruction est activ�e
            Destroy(this.gameObject);
        }
    }

}
