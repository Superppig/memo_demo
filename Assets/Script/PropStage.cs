using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PropStage : MonoBehaviour
{
    public int propsCount=3;
    private GameObject[] props;
    private GameObject currentPropObj;
    private int currentProp;
    private bool isLeave;
    
    private Player_Controller _playerController;
    // Start is called before the first frame update
    void Start()
    {
        _playerController = GameObject.FindWithTag("Player").GetComponent<Player_Controller>();
        props = new GameObject[propsCount];
        for (int i = 1; i <= propsCount; i++)
        {
            string a = string.Concat("Perfabs/Prop/Prop", i.ToString());
            props[i-1] = Resources.Load<GameObject>(a);
        }
        currentProp = Random.Range(1, propsCount);
        currentPropObj = Instantiate(props[currentProp-1],transform);
        isLeave = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isLeave)
            {
                if (currentProp != 0)
                {
                    if (_playerController.propType != currentProp)
                    {
                        int t = _playerController.propType;
                        _playerController.propType = currentProp;
                        currentProp = t;
                    
                        Destroy(currentPropObj);
                        if (currentProp != 0)
                        {
                            currentPropObj = Instantiate(props[currentProp-1],transform);
                        }
                    }
                }
                isLeave = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isLeave = true;
        }
    }

}
