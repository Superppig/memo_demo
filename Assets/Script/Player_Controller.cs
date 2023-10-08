using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour
{
    public float healthUp;//生命值上限
    public float speed;//移动速度
    public float health;//生命值
    public float shootSpeed;//射速
    public float bulletSpeed;//弹速
    public float fire_lenth;//射程
    public IssacBullet oriBullet;//子弹
    public float damage;//眼泪伤害
    public int bombCount;//炸弹数量
    public int keyCount;//钥匙数量
    public int coinCount;//金币数量
    public int propType;//道具种类

    public Vector2Int whichRoom;

    private Bomb myBomb;
    private bool isShoot;
    private float time;
    public int lastProp;
    private bool isProp2;
    
    private Vector2Int bulletDir;

    public Rigidbody2D myRigidbody;
    private Collider2D mycollider;
    public bool isAuto;
    
    //动画器
    private Animator myAnim;
    private SpriteRenderer mySprite;
    
    private Animator headAnim;
    private SpriteRenderer headSprite;

    private Animator bodyAnim;
    private SpriteRenderer bodySprite;

    private Transform headTransform;
    private Transform bodyTransform;
    
    
    // Start is called before the first frame update
    void Start()
    {
        isShoot = false;
        time = 0f;
        bulletDir=new Vector2Int(0,0);
        propType = 0;
        lastProp = 0;
        isProp2 = false;
        isAuto = false;
        healthUp= PlayerPrefs.GetFloat("HealthUp");
        health= PlayerPrefs.GetFloat("Health");
        speed= PlayerPrefs.GetFloat("Speed");
        shootSpeed=PlayerPrefs.GetFloat("ShootSpeed");
        bulletSpeed= PlayerPrefs.GetFloat("BulletSpeed");
        fire_lenth= PlayerPrefs.GetFloat("FireLength");
        damage= PlayerPrefs.GetFloat("Damage");
        bombCount= PlayerPrefs.GetInt("BombCount"); 
        keyCount= PlayerPrefs.GetInt("KeyCount");
        coinCount= PlayerPrefs.GetInt("CoinCount");
        propType=PlayerPrefs.GetInt("PropType");

        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        mySprite = GetComponent<SpriteRenderer>();
        myAnim.enabled = false;
        mySprite.enabled = false;
        myBomb = Resources.Load<Bomb>("Perfabs/Bomb");
        mycollider = GetComponent<Collider2D>();
        
        headAnim = transform.Find("Head").GetComponent<Animator>();
        headSprite = headAnim.GetComponent<SpriteRenderer>();
        bodyAnim = transform.Find("Body").GetComponent<Animator>();
        bodySprite = bodyAnim.GetComponent<SpriteRenderer>();
        headTransform = headAnim.GetComponent<Transform>();
        bodyTransform = bodyAnim.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        PropCon();
        Run();
        Shoot();
        AnimController();
        UseBomb();
        if (isProp2)
        {
            bodyAnim.enabled = false;
            bodySprite.enabled = false;
        }
        else
        {
            bodyAnim.enabled = true;
            bodySprite.enabled = true;
        }
        Die();
    }


    void Run()
    {
        float VelDir = Input.GetAxis("Vertical");
        float HorDir = Input.GetAxis("Horizontal");
        Vector2 playerVel = new Vector2(HorDir * speed, VelDir * speed);
        myRigidbody.velocity = playerVel;
    }

    void Shoot()
    {
        if (isShoot)
        {
            time += Time.deltaTime;
        }
        if (time>shootSpeed)
        {
            isShoot=false;
            time = 0f;
        }
        //用矢量表示子弹方向
        if (!isShoot)
        {
            isShoot = true;
            if (Input.GetKey(KeyCode.I))
                bulletDir = new Vector2Int(0, 1);
            else if (Input.GetKey(KeyCode.K))
                bulletDir = new Vector2Int(0, -1);
            else if (Input.GetKey(KeyCode.L))
                bulletDir = new Vector2Int(1, 0);
            else if (Input.GetKey(KeyCode.J))
                bulletDir = new Vector2Int(-1, 0);
            else
                isShoot = false;
            if(isShoot)
                oriBullet.Fire(transform,bulletDir,bulletSpeed,fire_lenth,damage,isAuto);//调用子弹物体的接口
            
        }
    }

    void AnimController()
    {
        //复原旋转
        bodyTransform.localRotation=Quaternion.Euler(0, 0, 0);
        headTransform.localRotation=Quaternion.Euler(0, 0, 0);

        //翻转控制
        if (myRigidbody.velocity.x > 0.1f)
        {
            bodyTransform.localRotation=Quaternion.Euler(0, 0, 0);
            if (!isShoot)
                headTransform.localRotation=Quaternion.Euler(0, 0, 0);
        }
        else if (myRigidbody.velocity.x < -0.1f)
        {
            bodyTransform.localRotation=Quaternion.Euler(0, 180, 0);
            if(!isShoot)
                headTransform.localRotation=Quaternion.Euler(0, 180, 0);
        }
        //转换控制
        if (Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon || Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon)
        {
            bodyAnim.SetBool("IsRun",true);
        }
        else
        {
            bodyAnim.SetBool("IsRun",false);
            if (!isShoot)
            {
                headAnim.SetBool("Up",false);
                headAnim.SetBool("Down",false);
                headAnim.SetBool("Hor",false);
            }
        }
        
        if (Mathf.Abs(myRigidbody.velocity.x)>Mathf.Epsilon)
        {
            bodyAnim.SetBool("RunHor",true);
            if (!isShoot)
            {
                headAnim.SetBool("Hor",true);
            }
        }
        else
        {
            bodyAnim.SetBool("RunHor",false);
            if (!isShoot)
            {
                headAnim.SetBool("Hor",false);
            }
        }
        
        if (Mathf.Abs(myRigidbody.velocity.y)>Mathf.Epsilon)
        {
            if (!bodyAnim.GetBool("RunHor"))
            {
                bodyAnim.SetBool("RunVel",true);
            }
            if (!isShoot)
            {
                if (myRigidbody.velocity.y > 0.1f)
                {
                    headAnim.SetBool("Up",true);
                    headAnim.SetBool("Down",false);
                }
                else if (myRigidbody.velocity.y < -0.1f)
                {
                    headAnim.SetBool("Up",false);
                    headAnim.SetBool("Down",true);
                }
            }
        }
        else
        {            
            if (!bodyAnim.GetBool("RunHor"))
            {
                bodyAnim.SetBool("RunVel",false);
            }
            if (!isShoot)
            {
                headAnim.SetBool("Up",false);
                headAnim.SetBool("Down",false);
            }
        }
        //射击头部控制已经子弹
        if (isShoot)
        {
            if (bulletDir.x==1)
            {
                headAnim.SetBool("Up",false);
                headAnim.SetBool("Down",false);
                headAnim.SetBool("Hor",true);
                headTransform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (bulletDir.x==-1)
            {
                headAnim.SetBool("Up",false);
                headAnim.SetBool("Down",false);
                headAnim.SetBool("Hor",true);
                headTransform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else if (bulletDir.y==1)
            {
                headAnim.SetBool("Up",true);
                headAnim.SetBool("Down",false);
                headAnim.SetBool("Hor",false);
            }
            else if (bulletDir.y==-1)
            {
                headAnim.SetBool("Up",false);
                headAnim.SetBool("Down",true);
                headAnim.SetBool("Hor",false);
            }
        }
    }

    public void Hurt(float hurtDamage)
    {
        health -= hurtDamage;
        headAnim.enabled = false;
        headSprite.enabled = false;
        bodyAnim.enabled = false;
        bodySprite.enabled = false;
        mySprite.enabled = true;
        myAnim.enabled = true;
        StartCoroutine(StartHurt());
    }
    IEnumerator StartHurt()
    {
        yield return new WaitForSeconds(0.4f);
        headAnim.enabled = true;
        headSprite.enabled = true;
        bodyAnim.enabled = true;
        bodySprite.enabled = true;
        mySprite.enabled = false;
        myAnim.enabled = false;
    }

    void UseBomb()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (bombCount > 0)
            {
                Instantiate(myBomb, transform);
                bombCount--;
            }
        }
    }
    void PropCon()
    {
        if (propType != lastProp)
        {
            if (propType == 1)
                isAuto = true;
            if (propType == 2)
                isProp2 = true;
            if (propType == 3)
                Prop3(true);

            if (lastProp == 1)
                isAuto = false;
            if (lastProp == 2)
                isProp2 = false;
            if (lastProp == 3) 
                Prop3(false);
            lastProp = propType;
        }
    }
    void Prop3(bool isOpen)
    {
        if (isOpen)
        {
            healthUp += 2;
            speed += 1;
            shootSpeed -= 0.1f;
            bulletSpeed += 2;
            fire_lenth += 2;
            damage += 1;
        }
        else
        {
            healthUp -= 2;
            speed -= 1;
            shootSpeed += 0.1f;
            bulletSpeed -= 2;
            fire_lenth -= 2;
            damage -= 1;
        }
    }

    void Die()
    {
        if (health<=0)
        {
            headAnim.enabled = false;
            headSprite.enabled = false;
            bodyAnim.enabled = false;
            bodySprite.enabled = false;
            mySprite.enabled = true;
            myAnim.enabled = true;
            StartCoroutine(StartDie());
        }
    }

    IEnumerator StartDie()
    {
        myAnim.SetTrigger("Die");
        yield return new WaitForSeconds(0.3f);
        //死亡场景
        SceneManager.LoadScene("Scenes/Dead");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Hurt(other.gameObject.GetComponent<Enemy>().damage);
        }
        if (isProp2)
        {
            if (other.gameObject.CompareTag("Obstacle"))
            {
                mycollider.isTrigger = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isProp2)
        {
            if (other.gameObject.CompareTag("Obstacle"))
            {
                mycollider.isTrigger = false;
            }
        }
    }
}
