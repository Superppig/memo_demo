using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PropCon : MonoBehaviour
{
    private Player_Controller player;
    private SpriteRenderer mySpr;
    private int currentType;
    // Start is called before the first frame update
    void Start()
    {
        mySpr = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player").GetComponent<Player_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        currentType = player.propType;
        if (currentType != 0)
        {
            string a = string.Concat("UI/Prop", currentType.ToString());
            mySpr.sprite = Resources.Load<Sprite>(a);
        }
        else
        {
            mySpr.sprite = null;
        }
    }
}
