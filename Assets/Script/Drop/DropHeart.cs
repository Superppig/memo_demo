using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHeart : DropThing
{
    void Start()
    {
        StartThing();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (player.health<player.healthUp)
            {
                player.health += value;
                player.health = player.health <= player.healthUp ? player.health :player.healthUp ;
                Destroy(gameObject);
            }
        }
    }
}
