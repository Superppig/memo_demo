using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropTreasureBox : DropThing
{
    public GameObject[] treasures;

    private Animator myAnim;

    private Collider2D myCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        StartThing();
        myAnim = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
    }

    void OpenTreasures()
    {
        myAnim.SetBool("isOpen",true);
        StartCoroutine(StartOpen());
    }
    IEnumerator StartOpen()
    {
        myCollider2D.enabled = false;
        yield return new WaitForSeconds(0.5f);
        int num = Random.Range(1, 5);
        for (int i = 0; i < num; i++)
        {
            Vector2 place = (Vector2)transform.position + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) ;
            Instantiate(treasures[Random.Range(0,treasures.Length-1)],place,quaternion.identity);
        }
        Destroy(gameObject);
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
