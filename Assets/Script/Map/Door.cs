using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //房间种类
    private Player_Controller player;//玩家

    public Door nextDoor;
    
    //逻辑变量
    public bool isLeave;

    public bool hasLock=false;//是否为锁着的门
    public bool isOpen=true;//是否打开
    public bool isLock=false;//是否锁着

    public bool hasEnemy;//是否有敌人

    //动画器
    private SpriteRenderer indoor;
    private SpriteRenderer left;
    private SpriteRenderer right;
    private SpriteRenderer locked;
    private SpriteRenderer keySpr;
    private Animator keyAnim;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
        isLeave = true;
        indoor = transform.Find("Indoor").GetComponent<SpriteRenderer>();
        left = transform.Find("left").GetComponent<SpriteRenderer>();
        right = transform.Find("right").GetComponent<SpriteRenderer>();
        locked = transform.Find("locked").GetComponent<SpriteRenderer>();
        keySpr = transform.Find("key").GetComponent<SpriteRenderer>();
        keyAnim = keySpr.GetComponent<Animator>();
        keySpr.enabled = false;
        keyAnim.enabled = false;
    }

    void Update()
    {
        DoorCon();
        AnimCon();
    }

    void DoorCon()
    {
        if (!hasEnemy && !isLock)
        {
            isOpen = true;
        }
        else
        {
            isOpen = false;
        }
    }
    void AnimCon()
    {
        if (!isLock)
        {
            if (isOpen)
            {
                indoor.enabled = true;
                left.enabled = false;
                right.enabled = false;
                locked.enabled = false;
                keySpr.enabled = false;
                keyAnim.enabled = false;
            }
            else
            {
                indoor.enabled = false;
                left.enabled = true;
                right.enabled = true;
                locked.enabled = false;
                keySpr.enabled = false;
                keyAnim.enabled = false;
            }
        }
        else
        {
            indoor.enabled = false;
            left.enabled = true;
            right.enabled = false;
            locked.enabled = true;
        }
        
    }

    IEnumerator OpenWithKey()
    {
        keySpr.enabled = true;
        keyAnim.enabled = true;
        yield return new WaitForSeconds(keyAnim.GetCurrentAnimatorStateInfo(0).length);
        isLock = false;
        isOpen = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isOpen)
            {
                if (isLeave)
                {
                    nextDoor.isLeave = false;
                    player.transform.position = nextDoor.transform.position;
                }
            }

            if (hasLock && isLock && !hasEnemy)
            {
                if (player.keyCount>0)
                {
                    player.keyCount--;
                    StartCoroutine(OpenWithKey());
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isLeave = true;
        }
    }
}
