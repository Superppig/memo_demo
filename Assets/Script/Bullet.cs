using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float length;
    public Vector2Int dirAx;
    public Vector2 dir;
    public float damage;
    public float deadTime;

    public Vector2 trans;//初始位置
    public Rigidbody2D myRigidbody2D;
    public Animator _animator;
    public Collider2D myCollider;
    public bool isDead;

    public bool isAuto;

    public void StartThings()
    {
        trans = transform.position;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
        isDead = false;
    }
    
    public void MoveAx()
    {
        Vector2 vel = new Vector2(0, 0);
        if (dirAx.x == 1)
            vel = new Vector2(speed, 0);
        else if (dirAx.x == -1)
            vel = new Vector2(-1*speed, 0);
        else if (dirAx.y == 1)
            vel = new Vector2(0, speed);
        else if (dirAx.y == -1)
            vel = new Vector2(0, -1*speed);
        myRigidbody2D.velocity = vel;
    }

    public void Move( )
    {
        Vector2 vel = dir.normalized * speed;
        myRigidbody2D.velocity = vel;
    }
    
}
