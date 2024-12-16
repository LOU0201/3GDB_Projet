using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public Transform joueur;
    private int x;
    private int z;
    private int y;
    private float tempx;
    private float tempz;
    private float tempy;
    public int max;
    // Start is called before the first frame update
    void Start()
    {
        z = Random.Range(2,16);
        tempz = z;
        tempz += 0.5f;
        x = Random.Range(1, 16);
        tempx = x;
        tempx += 0.5f;
        y = Random.Range(1, 7);
        tempy = y;
        tempy += 0.5f;
        transform.position = new Vector3(tempx, tempy, tempz);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 coordonnees= transform.position;
        Vector3 CJ = joueur.position;
        if (CJ== coordonnees)
        {
            Instantiate(this.gameObject, new Vector3(x, y, z), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
