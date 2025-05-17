using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDownWard : MonoBehaviour
{
    //Script qui sert � faire descendre plus doucement les visuels des fl�ches
    public GameObject _position;
    public float _distance;

    public float _speed;
    public bool son = false;
    public bool CanMove = true;

    void Update()
    {
        if (CanMove)
        {
            _distance = Vector3.Distance(transform.position, _position.transform.position);

            if (_distance > 0.01)
            {
                transform.position = Vector3.MoveTowards(this.transform.position, _position.transform.position, _distance * Time.deltaTime * _speed);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(son)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/V3/Level/PlayerUseHatch");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        son = false;
    }

    public void CantMove()
    {
        CanMove = false;
    }

    public void TPMove()
    {
        transform.position = _position.transform.position;
        CanMove = true;
    }
}