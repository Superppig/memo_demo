using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shit : Obstacle
{

    
    // Start is called before the first frame update
    void Start()
    {
        StartThing();
        currentState = 4;
        canDes = true;
        isDes = false;
        hasDes = false;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState();
        ShitDes();
    }

    void ShitDes()
    {
        if (isDes&&!hasDes)
        {
            myCollider.enabled = false;
            hasDes = true;
        }
    }

    void ChangeState()
    {
        currentState = currentState >= 0 ? currentState : 0;
        if (currentState == 0)
        {
            isDes = true;
        }
        string a = string.Concat("Perfabs/Ob/Shit", currentState.ToString());
        mySpr.sprite = Resources.Load<Sprite>(a);

    }

    public override void Hit()
    {
        currentState -= Random.Range(1, 3);
    }
}
