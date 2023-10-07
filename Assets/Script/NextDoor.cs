using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDoor : MonoBehaviour
{

    public bool isOpen;

    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        isOpen = true;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimCon();
    }

    void AnimCon()
    {
        if (isOpen)
        {
            _animator.SetBool("isOpen",true);
        }
        else
        {
            _animator.SetBool("isOpen",false);
        }
            
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isOpen)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("下一层");
            }
        }
    }
}
