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
    public float dropRrobability = 0.5f;//概率
    
    private bool hasDrop=false;
    //引用的组件
    public GameObject player;
    public Transform playTrans;
    public Animator myAnimator;
    public Rigidbody2D myRigidbody;
    public Collider2D myCollider;

    public void StartThings()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playTrans = player.transform;
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        
        Object[] loadedObjects = Resources.LoadAll("Perfabs/DroppedThings", typeof(GameObject));
        
        drops = new GameObject[loadedObjects.Length];
        for (int i = 0; i < loadedObjects.Length; i++)
        {
            drops[i] = (GameObject)loadedObjects[i];
        }

    }

    
    public virtual void Dead()
    {
        if (!hasDrop)
        {
            if (Random.Range(0, 1f) <= dropRrobability)
            {
                myCollider.enabled = false;
                Vector3 spawnPosition = transform.position; // 使用敌人的位置作为生成位置
                Instantiate(drops[Random.Range(0, drops.Length - 1)], spawnPosition,quaternion.identity);
            }
            hasDrop = true;
        }

    }
    public  void TakeDamage(float damage)
    {
        health -= damage;
    }
}
