using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTreasure : DropThing
{
    public GameObject[] treasures;
    // Start is called before the first frame update
    void Start()
    {
        Start();
    }

    void OpenTreasures()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (player.keyCount > 0)
            {
                OpenTreasures();
                player.keyCount--;
            }
        }
    }

}
