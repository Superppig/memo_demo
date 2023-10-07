using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IssacBullet : MonoBehaviour
{
    public CommonBullet bullet;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire(Transform fireTransform,Vector2Int fireDir,float Speed,float length,float damage,bool isAuto)
    {
        Bullet oneBullet =Instantiate(bullet, fireTransform);
        oneBullet.transform.SetParent(transform);
        oneBullet.speed=Speed;
        oneBullet.dirAx = fireDir;
        oneBullet.length = length;
        oneBullet.damage = damage;
        oneBullet.isAuto = isAuto;
    }
}
