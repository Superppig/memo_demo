using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luker : Obstacle
{
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        canDes = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //造成伤害接口
            other.GetComponent<Enemy>().TakeDamage(damage);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player_Controller>().Hurt(damage);
        }
    }

}
