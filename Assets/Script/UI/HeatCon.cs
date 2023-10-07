using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatCon : MonoBehaviour
{
    public Vector2 onrign = new Vector2(-257f, 126f);
    private float x = 16.0f;

    public Player_Controller player;
    public GameObject HeartUi;

    private int heartCount;

    private int curruntHealth;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player_Controller>();
        HeartUi = Resources.Load<GameObject>("UI/Heart");
    }

    // Update is called once per frame
    void Update()
    {
        heartCount = (int)(player.healthUp / 2);
        ChildDes(transform);
        HeartUpdate();
    }
    void HeartUpdate()
    {
        curruntHealth = (int)player.health;
        for (int i = 0; i < heartCount; i++)
        {
            GameObject heart = Instantiate(HeartUi, transform.position, Quaternion.identity);
            heart.transform.SetParent(transform);
            Transform rt = heart.transform;
            rt.localPosition = onrign + new Vector2(i * x, 0);
            if (curruntHealth >= 2)
            {
                heart.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("UI/Heart2");
                curruntHealth -= 2;
            }
            else if(curruntHealth==1)
            {
                heart.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("UI/Heart1");
                curruntHealth -= 1;
            }
            else
            {
                heart.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("UI/Heart0");
            }
            
        }
    }
    void ChildDes(Transform parent)
    {
        foreach (Transform child in parent)
        {
            GameObject.Destroy(child.gameObject);
        }
            
    }
}
