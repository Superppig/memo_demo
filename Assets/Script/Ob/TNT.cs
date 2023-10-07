using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : Obstacle
{
    public float boonRad;

    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        StartThing();
        currentState = 3;
        canDes = true;
        isDes = false;
        hasDes = false;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState();
        TNTDes();
    }

    void TNTDes()
    {
        if (isDes&&!hasDes)
        {
            myCollider.enabled = false;
            Boon();
            hasDes = true;
        }
    }
    void ChangeState()
    {
        if (currentState == 0)
        {
            isDes = true;
        }
        string a = string.Concat("Perfabs/Ob/TNT", currentState.ToString());
        mySpr.sprite = Resources.Load<Sprite>(a);
        
    }

    void Boon()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, boonRad);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                collider.GetComponent<Player_Controller>().Hurt(damage);
            }

            if (collider.gameObject.CompareTag("Enemy"))
            {
                collider.GetComponent<Enemy>().TakeDamage(damage);
            }

            if (collider.gameObject.CompareTag("Obstacle"))
            {
                Obstacle obstacle = collider.GetComponent<Obstacle>();
                if (obstacle.canDes)
                {
                    obstacle.Des();
                }
            }
        }
    }
    public override void Hit()
    {
        currentState--;
    }
}
