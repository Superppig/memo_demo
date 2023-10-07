using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{

    public float attackRange;//检测范围
    public float moveTime;//每次移动时间
    //属性
    private float dieTime=0.21f;
    private bool isMove;
    private float time;
    private float rangetime;
    private float changeTime = 0.5f;


    private Vector2 moveDir;
    
    
    // Start is called before the first frame update
    void Start()
    {
        StartThings();
        time = 0f;
        rangetime = 0f;
        isMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCtr();
        Dead();
    }

    void MoveCtr()
    {
        time += Time.deltaTime;
        if (time > moveTime)
        {
            time = 0f;
            //转换移动
            if (isMove)
                isMove = false;
            else
                isMove = true;
        }
        if (isMove)
        {
            Move();
            myAnimator.SetBool("Run",true);
        }
        else
        {
            myRigidbody.velocity = Vector2.zero;
            myAnimator.SetBool("Run",false);
        }
    }

    void Move()
    {
        moveDir = (playTrans.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDir, moveSpeed * Time.deltaTime);
        if ((hit.collider != null && hit.collider.CompareTag("Obstacle"))||Vector2.Distance(playTrans.position,transform.position)>=attackRange)
        {
            //随机移动
            rangetime += Time.deltaTime;
            if (rangetime > changeTime)
            {
                myRigidbody.velocity = new Vector2(moveSpeed * Random.Range(-1f,1f), moveSpeed * Random.Range(-1f,1f));
                rangetime = 0f;
            }
        }
        else
        {
            //向player移动
            myRigidbody.velocity = new Vector2(moveSpeed * moveDir.x, moveSpeed * moveDir.y);
        }
    }

    public override void Dead()
    {
        if (health <= 0)
        {
            base.Dead();
            myAnimator.SetTrigger("Die");
            StartCoroutine(StartDie());
        }
    }
    IEnumerator StartDie()
    {
        yield return new WaitForSeconds(dieTime);
        Destroy(gameObject);
    }
}
