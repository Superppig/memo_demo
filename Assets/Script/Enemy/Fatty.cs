using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fatty : Enemy
{
    private Vector2 dir;

    private bool isRunHor;

    private Transform body;
    public Animator bodyAnimator;

    public Animator fartAnim;
    private SpriteRenderer fartSpr;
    
    public float fartRad;//屁的范围
    public float fartForce = 10f; // 屁的推力
    public float fartTime;

    private float time;

    // Start is called before the first frame update
    void Start()
    {
        StartThings();
        time = 0f;
        body = bodyAnimator.GetComponent<Transform>();
        fartSpr = fartAnim.GetComponent<SpriteRenderer>();
        fartAnim.enabled = false;
        fartSpr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Anim();
        Fart();
        Dead();
    }

    void Move()
    {
        dir = player.transform.position - transform.position;
        myRigidbody.velocity = dir.normalized * moveSpeed;
    }
    void Anim()
    {
        if (myRigidbody.velocity.magnitude > Mathf.Epsilon)
        {
            bodyAnimator.SetBool("isRun",true);
        }
        else
        {
            bodyAnimator.SetBool("isRun",true);
        }
        
        if (myRigidbody.velocity.x>Mathf.Epsilon)
        {
            body.localRotation=Quaternion.Euler(0,0,0);
        }
        else if (myRigidbody.velocity.x<Mathf.Epsilon)
        {
            body.localRotation=Quaternion.Euler(0,180,0);
        }

        if (Mathf.Abs(myRigidbody.velocity.x)>Mathf.Epsilon)
        {
            bodyAnimator.SetBool("Hor",true);
        }
        else
        {
            bodyAnimator.SetBool("Hor",false);
        }
    }

    void Fart()
    {
        if (Vector2.Distance(player.transform.position,transform.position)<fartRad)
        {
            time += Time.deltaTime;
            if (time>fartTime)
            {
                Vector2 dir = player.transform.position - transform.position;
                player.myRigidbody.AddForce(dir.normalized*fartForce,ForceMode2D.Impulse);
                StartCoroutine(StartFart());
            }
        }
        else
        {
            time = 0f;
        }
    }

    IEnumerator StartFart()
    {
        fartAnim.enabled = true;
        fartSpr.enabled = true;
        yield return new WaitForSeconds(fartAnim.GetCurrentAnimatorStateInfo(0).length);
        fartAnim.enabled = false;
        fartSpr.enabled = false;
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
