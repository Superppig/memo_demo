using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBomb : DropThing
{
    void Start()
    {
        StartThing();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.bombCount++;
            Destroy(gameObject);
            
        }
    }
}
