using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartControler : MonoBehaviour
{
    public GameObject tag01;

    public int stateUp;

    private int state;

    private Vector2 origin = new Vector2(-2.23f, 1.49f);

    private float y = -1;
    // Start is called before the first frame update
    void Start()
    {
        state = 0;
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            state--;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            state++;
        }
        state = state > 0 ? state : 0;
        state = state < stateUp ? state : stateUp;
        ConStart();
    }

    void ConStart()
    {
        tag01.transform.position = origin + new Vector2(0, state * y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (state==0)
            {
                SceneManager.LoadScene("Scenes/SampleScene");
            }
        }
    }
}
