using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CommonBullet : Bullet
{
    private SpriteRenderer mySpri;
    // Start is called before the first frame update
    void Start()
    {
        mySpri = GetComponent<SpriteRenderer>();
        deadTime=0.51f;
        StartThings(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (isAuto)
        {
            mySpri.color = Color.blue;
            FindEnemy();
            Move();
        }
        else
        {
            MoveAx();
        }
        Dead();
        if (Vector2.Distance(trans,transform.position)>length)
        {
            isDead = true;
        }
    }

    void FindEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        if (enemies.Length==0)
        {
            dir = dirAx;
        }
        else
        {
            foreach (GameObject enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy.transform;
                }
            }
            dir = closestEnemy.position - transform.position;
        }

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
        if (other.gameObject.CompareTag("Enemy"))
        {
            //造成伤害接口
            other.GetComponent<Enemy>().TakeDamage(damage);
            isDead = true;
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            other.GetComponent<Obstacle>().Hit();
            isDead = true;
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            isDead = true;
        }
    }
}
