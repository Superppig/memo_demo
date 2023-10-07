using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Count : MonoBehaviour
{
    public Player_Controller player;
    private Text coinCount;
    private Text keyCount;
    private Text bombCount;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player_Controller>();
        coinCount = GameObject.Find("CoinCount").GetComponent<Text>();
        keyCount = GameObject.Find("KeyCount").GetComponent<Text>();
        bombCount = GameObject.Find("BombCount").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textUpdate();
    }

    void textUpdate()
    {
        coinCount.text = player.coinCount.ToString();
        keyCount.text = player.keyCount.ToString();
        bombCount.text = player.bombCount.ToString();
    }
}
