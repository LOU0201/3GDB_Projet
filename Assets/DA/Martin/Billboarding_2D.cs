using UnityEngine;

public class Billboarding_2D : MonoBehaviour
{
    [SerializeField] bool freezeXZAxis = true;
    public Camera cam;
    void Update()
    {
        if(freezeXZAxis)
        {
            transform.rotation = Quaternion.Euler(0f, cam.transform.rotation.eulerAngles.y, 0f);
        }
        else
        {
            transform.rotation = cam.transform.rotation;
        }
        
    }
}
