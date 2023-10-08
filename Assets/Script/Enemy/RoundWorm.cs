using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoundWorm : Enemy
{
    //动画器
    private Animator headAnim;
    private SpriteRenderer headSprite;
    private Animator bodyAnim;
    private SpriteRenderer bodySprite;

    private EnemyBullet myBullet;

    public float fireSpeed;
    public float fireLength;
    public float fireDamage;    //移动相关参数
    private float moveTime = 1f;
    private float AnimTime = 1f;
    private bool isOut=true;
    private float time;
    private Vector2 range = new Vector2(6.3125f,3.875f);
    
    // Start is called before the first frame update
    void Start()
    {
        StartThings();
        time = 0f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
        playTrans = player.transform;
        myRigidbody = GetComponent<Rigidbody2D>();

        myBullet = Resources.Load<EnemyBullet>("Perfabs/EnemyBullet");
        
        headAnim = transform.Find("Head").GetComponent<Animator>();
        headSprite = headAnim.GetComponent<SpriteRenderer>();
        bodyAnim = transform.Find("Body").GetComponent<Animator>();
        bodySprite = bodyAnim.GetComponent<SpriteRenderer>();
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
            if (isOut)
            {
                Anim();
                isOut = false;
            }
        }
    }

    void Anim()
    {
        headSprite.enabled = true;
        bodySprite.enabled = true;
        headAnim.enabled = true;
        bodyAnim.enabled = true;
        headAnim.SetBool("isOut",true);
        bodyAnim.SetBool("isOut",true);
        StartCoroutine(StartFire());
    }

    IEnumerator StartFire()
    {
        yield return new WaitForSeconds(AnimTime);
        Fire();
        headAnim.SetBool("isOut",false);
        bodyAnim.SetBool("isOut",false);
        StartCoroutine(StartIn());
    }

    IEnumerator StartIn()
    {
        yield return new WaitForSeconds(AnimTime);
        headSprite.enabled = false;
        bodySprite.enabled = false;
        headAnim.enabled = false;
        bodyAnim.enabled = false;
        time = 0;
        isOut = true;
        RandomMove();
    }
    void RandomMove()
    {
        Vector2 place = player.whichRoom;
        Vector2 currentPlace = new Vector2(place.x * 20 + Random.Range(-1 * range.x, range.x),
            place.y * 20 + Random.Range(-1 * range.y, range.y));
        transform.position = currentPlace;
    }

    void Fire()
    {
        EnemyBullet oneBullet = Instantiate(myBullet, transform.position, quaternion.identity);
        oneBullet.isMoveAX = false;
        oneBullet.dir = player.transform.position - transform.position;
        oneBullet.speed = fireSpeed;
        oneBullet.length = fireLength;
        oneBullet.damage = fireDamage;
    }


    
    public override void Dead()
    {
        if (health <= 0)
        {
            base.Dead();
            Destroy(gameObject);
        }
    }
}
