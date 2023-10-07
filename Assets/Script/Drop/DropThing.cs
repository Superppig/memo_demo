using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropThing : MonoBehaviour
{
    public float value;
    public Player_Controller player;
    public Rigidbody2D myRigidbody;

    public void StartThing()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player_Controller>();
        myRigidbody = GetComponent<Rigidbody2D>();
        
    }
    
}
