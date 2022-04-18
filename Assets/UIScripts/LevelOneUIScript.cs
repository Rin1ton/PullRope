using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelOneUIScript : MonoBehaviour
{
   

    public static bool GamePaused = false;

    public GameObject Crosshair;

    public GameObject PauseMenuObject;

    public UITimerScript timertext;

    
    public static LevelOneUIScript instance = null;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
       // UnityEngine.Cursor.lockState = CursorLockMode.Locked;
       // UnityEngine.Cursor.visible = false;
    }

    // Start is called before the first frame update
    void Update()
    {
        timertext.playing = true;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape Pressed");
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

    void Resume()
    {
       // Cursor.visible = false;
        PauseMenuObject.SetActive(false);
        Crosshair.SetActive(true);
        Time.timeScale = 1f;
        timertext.playing = true;
        GamePaused = false;
        //UnityEngine.Cursor.lockState = CursorLockMode.Locked;
       // UnityEngine.Cursor.visible = false;
        Debug.Log("Resuming Game");
    }

    void Pause()
    {
       // Cursor.visible = true;
        PauseMenuObject.SetActive(true);
        Crosshair.SetActive(false);
        Time.timeScale = 0f;
        timertext.playing = false;
        GamePaused = true;
       // UnityEngine.Cursor.lockState = CursorLockMode.None;
       // UnityEngine.Cursor.visible = true;
        Debug.Log("Pausing Game");
    }
}
