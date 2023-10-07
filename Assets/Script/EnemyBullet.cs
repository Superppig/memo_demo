using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    public bool isMoveAX;
    // Start is called before the first frame update
    void Start()
    {
        isMoveAX = true;
        deadTime = 0.51f;
        StartThings();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoveAX)
        {
            MoveAx();
        }
        else
        {
            Move();
        }
        Dead();
    }

    void Dead()
    {
        if (isDead)
        {
            myRigidbody2D.velocity = Vector2.zero;
            _animator.SetTrigger("Boon");
            StartCoroutine(StartDead()); 
        }

    }

    IEnumerator StartDead()
    {
        yield return new WaitForSeconds(deadTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //造成伤害接口
            other.GetComponent<Player_Controller>().Hurt(damage);
            isDead = true;
        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
            other.GetComponent<Obstacle>().Hit();
            isDead = true;
        }
    }
}
