using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapSpawn : MonoBehaviour
{
    private Room myRoom;
    private NextDoor nextDoor;
    private Door doorSpawn;
    private PropStage propStage;
    
    public Player_Controller player;
    private Vector2 oringRoom;//初始坐标屋子
    private int mapLength = 7;
    private int[,] littleMap =
    {
        {0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0},
        {0,0,1,0,4,0,0},
        {0,4,2,1,1,0,0},
        {0,0,1,0,3,0,0},
        {0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0}
    } ;//地图,1为普通房间,2为出生地,3为结束房间,4为道具房

    private Vector2 doorPlace = new Vector2(6.2f, 3.7f);
    
    // Start is called before the first frame update
    void Start()
    {
        myRoom = Resources.Load<Room>("Perfabs/Rooms/CommonRoom");
        doorSpawn = Resources.Load<Door>("Perfabs/Rooms/Door");
        nextDoor = Resources.Load<NextDoor>("Perfabs/Rooms/NextDoor");
        propStage = Resources.Load<PropStage>("Perfabs/Rooms/PropStage");
        RoomsSpawn();
        
    }


    void RoomsSpawn()
    {
        for (int i = 0; i < mapLength; i++)
        {
            for (int j = 0; j < mapLength; j++)
            {
                if (littleMap[i, j] != 0)
                {
                    Vector2 place = new Vector2(j*20, i * 20);
                    Room cunrrentRoom;
                    cunrrentRoom = Instantiate(myRoom,place,Quaternion.identity);
                    cunrrentRoom.place = new Vector2Int(j, i);
                    cunrrentRoom.roomType = littleMap[i, j];
                    //找到并传送到初始房间
                    if (littleMap[i, j] == 2)
                    {
                        oringRoom = new Vector2(20 * j, 20 * i);
                    }
                    //结束房间
                    if (littleMap[i,j]==3)
                    {
                        Instantiate(nextDoor, new Vector3(20 * j, 20 * i), Quaternion.identity);
                    }

                    if (littleMap[i,j]==4)
                    {
                        Instantiate(propStage, new Vector3(20 * j, 20 * i), Quaternion.identity);
                    }
                    //生成门逻辑
                    if (i != mapLength - 1 && j != mapLength - 1)
                    {
                        if (littleMap[i , j+1] != 0)
                        {
                            Door currenDoor,rightDoor;
                            Vector2 doorSpawnPlace = new Vector2(j * 20+doorPlace.x, i * 20);
                            currenDoor = Instantiate(doorSpawn, doorSpawnPlace, Quaternion.Euler(0, 0, 270));
                            doorSpawnPlace = new Vector2((j + 1) * 20 - doorPlace.x, i * 20);
                            rightDoor = Instantiate(doorSpawn, doorSpawnPlace, Quaternion.Euler(0, 0, 90));
                            currenDoor.nextDoor = rightDoor;
                            rightDoor.nextDoor = currenDoor;//建立双向关系
                            if (littleMap[i,j]==4)
                            {
                                rightDoor.hasLock = true;
                                rightDoor.isLock = true;
                                rightDoor.isOpen = false;
                            }

                            if (littleMap[i, j+1] == 4)
                            {
                                currenDoor.hasLock = true;
                                currenDoor.isLock = true;
                                currenDoor.isOpen = false;
                            }
                        }
                        if (littleMap[i+1, j] != 0)
                        {
                            Door currenDoor,downDoor;
                            Vector2 doorSpawnPlace = new Vector2(j * 20, i * 20-doorPlace.y);
                            currenDoor = Instantiate(doorSpawn, doorSpawnPlace, Quaternion.Euler(0, 0, 180));
                            doorSpawnPlace = new Vector2(j * 20, (i+1) * 20 + doorPlace.y);
                            downDoor = Instantiate(doorSpawn, doorSpawnPlace, Quaternion.Euler(0, 0, 0));
                            currenDoor.nextDoor = downDoor;
                            downDoor.nextDoor = currenDoor;//建立双向关系
                            if (littleMap[i,j] == 4)
                            {
                                downDoor.hasLock = true;
                                downDoor.isLock = true;
                                downDoor.isOpen = false;
                            }
                            if (littleMap[i+1, j] == 4)
                            {
                                currenDoor.hasLock = true;
                                currenDoor.isLock = true;
                                currenDoor.isOpen = false;
                            }
                        }
                    }
                }
            }
        }
        player.transform.position = oringRoom;
    }
}
