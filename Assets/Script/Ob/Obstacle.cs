using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public int currentState;

    public bool canDes;
    public bool isDes;
    public bool hasDes;
    public Collider2D myCollider;
    public SpriteRenderer mySpr;

    public void StartThing()
    {
        myCollider = GetComponent<Collider2D>();
        mySpr = GetComponent<SpriteRenderer>();
    }

    public void Des()
    {
        currentState = 0;
    }
    public virtual void Hit()
    {
        
    }
}
