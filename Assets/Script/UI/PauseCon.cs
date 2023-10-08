using UnityEngine;

public class PauseCon : MonoBehaviour
{
    public SpriteRenderer pauseUI; // 暂停界面的UI对象

    private bool isPaused = false;

    void Start()
    {
        pauseUI = GetComponent<SpriteRenderer>();
        pauseUI.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        Time.timeScale = isPaused ? 0 : 1;

        if (isPaused)
        {
            pauseUI.enabled=true;
        }
        else
        {
            pauseUI.enabled=false;
        }
    }
}