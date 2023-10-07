using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Obstacle
{
    // Start is called before the first frame update
    void Start()
    {
        StartThing();
        currentState = 1;
        canDes = true;
        isDes = false;
        hasDes = false;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState();
        StoneDes();
    }

    void StoneDes()
    {
        if (isDes&&!hasDes)
        {
            myCollider.enabled = false;
            hasDes = true;
        }
    }
    void ChangeState()
    {
        if (currentState == 0)
        {
            isDes = true;
        }
        string a = string.Concat("Perfabs/Ob/Stone", currentState.ToString());
        mySpr.sprite = Resources.Load<Sprite>(a);
    }
}
