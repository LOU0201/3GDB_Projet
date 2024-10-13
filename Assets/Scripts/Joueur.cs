using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur : MonoBehaviour
{
    public int compte_carré = 0;
    public int variable_compte_carré = 3;
    public GameObject Update_grille3d;
    private int x;
    private int z;
    private float tempx;
    private float tempz;
    // Start is called before the first frame update
    void Start()
    {
        Debut();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Update_grille3d.GetComponent<Grille_3d>().Estprit(transform.position+ new Vector3(1, 0, 0)))
            {
                this.transform.position += new Vector3(1, 0, 0);
                compte_carré++;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            if (Update_grille3d.GetComponent<Grille_3d>().Estprit(transform.position + new Vector3(-1, 0, 0)))
            {
                this.transform.position += new Vector3(-1, 0, 0);
                compte_carré++;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Update_grille3d.GetComponent<Grille_3d>().Estprit(transform.position + new Vector3(0, 0, 1)))
            {
                this.transform.position += new Vector3(0, 0, 1);
                compte_carré++;
            }    
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Update_grille3d.GetComponent<Grille_3d>().Estprit(transform.position + new Vector3(0, 0, -1)))
            {
                this.transform.position += new Vector3(0, 0, -1);
                compte_carré++;
            } 
        }
        if(compte_carré>=variable_compte_carré)
        {
            Update_grille3d.GetComponent<Grille_3d>().Faire_carrer(transform.position);
            compte_carré = 0;
            print("fais");
        }
    }
    public void Debut()
    {
        z = Random.Range(2, 16);
        tempz = z;
        tempz += 0.5f;
        x = Random.Range(1, 16);
        tempx = x;
        tempx += 0.5f;
        transform.position = new Vector3(tempx, 1.5f, tempz);
    }
}
