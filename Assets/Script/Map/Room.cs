using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.VisualScripting;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


public class Room : MonoBehaviour
{
    public int roomType;
    
    private Player_Controller player;
    private GameObject canmara;
    private Transform playerTrans;
    //房间中心的坐标
    public Vector2Int place;

    //怪物
    public Enemy[] enemies; 
    
    private Queue<Door> _doors;
    private bool hasDetect;
    public bool hasEnemy;

    private float height = 7.3125f;//长度
    private float width = 4.875f;//宽度

    void Start()
    {
        _doors = new Queue<Door>();
        hasDetect = false;
        hasEnemy = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
        canmara = GameObject.FindGameObjectWithTag("MainCamera");
        playerTrans = player.transform;
    }



    void Update()
    {
        if (isInRoom())
        {
            canmara.transform.position = transform.position + new Vector3(0, 0, -10);
            player.whichRoom = place;
            Detect();
            DoorCon();
        }
    }

    bool isInRoom()
    {
        if (playerTrans.position.x > transform.position.x - height &&
            playerTrans.position.x < transform.position.x + height &&
            playerTrans.position.y > transform.position.y - width &&
            playerTrans.position.y < transform.position.y + width)
        {
            return true;
        }

        return false;
    }
    void Detect()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(2 * height, 2 * width), 0f);
        hasEnemy = false;
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                // 房间内存在敌人
                hasEnemy = true;
            }

            if (!hasDetect)
            {
                if (collider.CompareTag("Door"))
                {
                    _doors.Enqueue(collider.GetComponent<Door>());
                }
            }
        }
        if (!hasDetect)
        {
            if(roomType==1)
                EnemySpawn();
            hasDetect = true;
        }
        // 房间内没有敌人
    }

    void EnemySpawn()
    {
        int n = Random.Range(3, 7);
        for (int i = 0; i < n; i++)
        {
            Vector3 place = transform.position + new Vector3(Random.Range(-height+1,height-1),Random.Range(-width+1,width-1),0);
            Instantiate(enemies[Random.Range(0, enemies.Length - 1)], place, quaternion.identity);
        }
    }

    void DoorCon()
    {
        foreach (Door door in _doors)
        {
            door.hasEnemy = hasEnemy;
        }
    }
    
    public Vector2Int GetPosition()//获取中心位置
    {
        return place;
    }
}
