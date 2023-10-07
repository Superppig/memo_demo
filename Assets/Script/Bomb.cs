using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float animationTime = 0.1f; // 设置动画播放速度
    public float boonTime;
    public float damage;
    

    private Color[] bombColors; // 存储不同颜色
    private int currentColor = 0; // 当前颜色的索引
    private float time;
    private float currentTime;

    void Start()
    {
        time = 0f;
        currentTime = 0f;
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        // 初始化 bombColors 数组
        bombColors = new Color[]
        {
            Color.white,  // 原图
            Color.yellow, // 黄色
            Color.red     // 红色
        };
    }

    void Update()
    {
        time += Time.deltaTime;
        currentTime += Time.deltaTime;
        if (time > animationTime)
        {
            time = 0f;
            PlayAnimation();
        }
        if (currentTime > boonTime)
        {
            Boon();
        }
    
}

    void PlayAnimation()
    {
        // 切换到下一个颜色
        currentColor = (currentColor + 1) % bombColors.Length;
        spriteRenderer.color = bombColors[currentColor];
    }

    public float explosionRadius;
    public void Boon()
    {
        // 创建一个盒子Collider2D
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(explosionRadius, explosionRadius), 0f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                collider.GetComponent<Player_Controller>().Hurt(damage);
            }
            else if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<Enemy>().TakeDamage(damage);
            }
            else if (collider.gameObject.CompareTag("Obstacle"))
            {
                Obstacle obstacle = collider.GetComponent<Obstacle>();
                if (obstacle.canDes)
                {
                    obstacle.Des();
                }
            }
        }

        // 销毁盒子Collider2D
        Destroy(gameObject);
    }
}
