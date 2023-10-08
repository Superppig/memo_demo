using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadCon : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("Map", 1);
        
        PlayerPrefs.SetFloat("HealthUp",16);
        PlayerPrefs.SetFloat("Health",16);
        PlayerPrefs.SetFloat("Speed",5);
        PlayerPrefs.SetFloat("ShootSpeed",0.5f);
        PlayerPrefs.SetFloat("BulletSpeed",10);
        PlayerPrefs.SetFloat("FireLength",5);
        PlayerPrefs.SetFloat("Damage",2);
        PlayerPrefs.SetInt("BombCount",1);
        PlayerPrefs.SetInt("KeyCount",0);
        PlayerPrefs.SetInt("CoinCount",0);
        PlayerPrefs.SetInt("PropType",0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("SampleScene");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
