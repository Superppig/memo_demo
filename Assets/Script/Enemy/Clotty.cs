using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class Clotty : Enemy
{
    private EnemyBullet myBullet;
    public float fireSpeed;
    public float fireLength;
    public float fireDamage;
    private float moveTime;//每次发射子弹时间

    private float time;
    // Start is called before the first frame update
    void Start()
    {
        StartThings();
        moveTime = myAnimator.GetCurrentAnimatorStateInfo(0).length;

        time = 0f;
        myRigidbody.velocity = new Vector2(moveSpeed * Random.Range(-1f,1f), moveSpeed * Random.Range(-1f,1f));
        myBullet = Resources.Load<EnemyBullet>("Perfabs/EnemyBullet");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Dead();
        
    }

    void Move()
    {
        time += Time.deltaTime;
        if (time > moveTime)
        {
            myRigidbody.velocity = new Vector2(moveSpeed * Random.Range(-1f,1f), moveSpeed * Random.Range(-1f,1f));
            Fire();
            time = 0f;
        }
    }

    public override void Dead()
    {
        if (health <= 0)
        {
            base.Dead();
            Destroy(gameObject);
        }
    }

    void Fire()
    {
        for (int i = 0; i < 4; i++)
        {
            EnemyBullet oneBullet =Instantiate(myBullet, transform);
            oneBullet.transform.SetParent(transform);
            oneBullet.speed=fireSpeed;
            oneBullet.length = fireLength;
            oneBullet.damage = fireDamage;
            oneBullet.isMoveAX = true;
            if (i==0)
                oneBullet.dirAx = new Vector2Int(1, 0);
            else if (i==1)
                oneBullet.dirAx = new Vector2Int(-1, 0);
            else if (i==2)
                oneBullet.dirAx = new Vector2Int(0, 1);
            else if (i==3)
                oneBullet.dirAx = new Vector2Int(0, -1);


        }
    }
}
