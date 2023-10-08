using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditorInternal;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public GameObject[] drops;
    public float health;
    public float moveSpeed;//移动速度
    public float damage;//伤害
    private bool hasDrop=false;
    //引用的组件
    public Player_Controller player;
    public Transform playTrans;
    public Animator myAnimator;
    public Rigidbody2D myRigidbody;
    public Collider2D myCollider;

    public void StartThings()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
        playTrans = player.transform;
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }

    
    public virtual void Dead()
    {


    }
    public  void TakeDamage(float damage)
    {
        health -= damage;
    }
}
