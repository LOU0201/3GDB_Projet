using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_player : MonoBehaviour
{
    private Animator _animator;


    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _animator.SetTrigger("tempWalk");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _animator.SetTrigger("tempWalk");

        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _animator.SetTrigger("tempWalk");

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _animator.SetTrigger("tempWalk");

        }
    }

    public void Climb_Upfinish()
    {
        _animator.SetBool("Climbing", false);
        _animator.SetBool("Grounded", true);
    }
}
