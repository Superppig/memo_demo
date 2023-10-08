using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextDoor : MonoBehaviour
{

    public bool isOpen;

    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        isOpen = true;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimCon();
    }

    void AnimCon()
    {
        if (isOpen)
        {
            _animator.SetBool("isOpen",true);
        }
        else
        {
            _animator.SetBool("isOpen",false);
        }
            
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isOpen)
        {
            if (other.CompareTag("Player"))
            {
                Player_Controller player = other.GetComponent<Player_Controller>();
                Debug.Log("下一层");
                if (PlayerPrefs.GetInt("Map")==1)
                {
                    PlayerPrefs.SetInt("Map", 2);
                }

                PlayerPrefs.SetFloat("HealthUp", player.healthUp);
                PlayerPrefs.SetFloat("Health", player.health);
                PlayerPrefs.SetFloat("Speed", player.speed);
                PlayerPrefs.SetFloat("ShootSpeed", player.shootSpeed);
                PlayerPrefs.SetFloat("BulletSpeed", player.bulletSpeed);
                PlayerPrefs.SetFloat("FireLength", player.fire_lenth);
                PlayerPrefs.SetFloat("Damage", player.damage); 
                PlayerPrefs.SetInt("BombCount", player.bombCount);
                PlayerPrefs.SetInt("KeyCount", player.keyCount);
                PlayerPrefs.SetInt("CoinCount", player.coinCount);
                PlayerPrefs.SetInt("PropType", player.propType);

                 SceneManager.LoadScene("SampleScene");
            }
        }
    }
}
