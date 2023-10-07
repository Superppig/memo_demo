using System;
using System.Collections;
using System.Collections.Generic;
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
    private bool isMove;
    private float moveTime = 1f;
    private float time;
    
    // Start is called before the first frame update
    void Start()
    {
        isMove = false;
        time = 0f;
        
        player = GameObject.FindGameObjectWithTag("Player");
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
        if (time>moveTime)
        {
            if (isMove)
            {
                isMove = false;
                Hide();

            }
            else
            {
                isMove = true;
                Out();

            }
            time = 0f;
        }
    }

    void Hide()
    {
        bodyAnim.enabled = true;
        headAnim.SetBool("isOut",false);
        headAnim.SetBool("Attack",false);
        SetAnimTime(headAnim,1f);
        bodyAnim.SetBool("isOut",false);
        SetAnimTime(bodyAnim,1f);
    }
    void Out()
    {
        headAnim.SetBool("isOut",true);
        headAnim.SetBool("Attack",false);
        SetAnimTime(headAnim,0.6f);
        bodyAnim.SetBool("isOut",true);
        SetAnimTime(bodyAnim,0.6f);
        
        StartCoroutine(StartOut());
    }

    IEnumerator StartOut()
    {
        yield return new WaitForSeconds(0.6f);
        Fire();
    }
    void Fire()
    { 
        headAnim.SetBool("isOut",true);
        headAnim.SetBool("Attack",true);
        SetAnimTime(headAnim,0.4f);
        bodyAnim.enabled = false;
        StartCoroutine(StartFire());
    }

    IEnumerator StartFire()
    {
        yield return new WaitForSeconds(0.2f);
        EnemyBullet oneBullet = Instantiate(myBullet, transform);
        oneBullet.transform.SetParent(transform);
        oneBullet.speed = fireSpeed;
        oneBullet.length = fireLength;
        oneBullet.damage = fireDamage;
        oneBullet.dir=playTrans.position-transform.position;
        oneBullet.isMoveAX = false;
    }

    void SetAnimTime(Animator animator, float Time)
    { 
        float currentTime = animator.GetCurrentAnimatorStateInfo(0).length;
        float speed = time / currentTime;
        animator.speed *= speed;
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
