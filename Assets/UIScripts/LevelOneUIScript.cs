using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelOneUIScript : MonoBehaviour
{


    public static bool GamePaused = false;

    public GameObject Crosshair;

    public GameObject PauseMenuObject;

    public GameObject EndScreenMenu;

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
        Scene scene = SceneManager.GetActiveScene();

        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        timertext.playing = true;
        // UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        // UnityEngine.Cursor.visible = false;
    }

    // Start is called before the first frame update
    void Update()
    {

        Scene scene = SceneManager.GetActiveScene();
       // Debug.Log("Name: " + scene.name);
        if (scene.name == "End Screen")
        {
          //  Debug.Log("End Screen");
            EndScreenMenu.SetActive(true);
            Crosshair.SetActive(false);
            timertext.playing = false;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;

        }

        if (scene.name == "Main Menu")
        {
            timertext.reset = true;
            EndScreenMenu.SetActive(false);
        }



        if ((scene.name == "level idea one") && (GamePaused == false))
        {
            timertext.reset = false;
            Crosshair.SetActive(true);
            timertext.playing = true;
        }

        

            //timertext.playing = true;

            if (Input.GetKeyDown(KeyCode.Escape) &&  (scene.name != "End Screen"))
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
        //Time.timeScale = 1f;
        timertext.playing = true;
        GamePaused = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }

    void Pause()
    {
        // Cursor.visible = true;
        PauseMenuObject.SetActive(true);
        Crosshair.SetActive(false);
        //Time.timeScale = 0f;
        timertext.playing = false;
        GamePaused = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
        Debug.Log("Pausing Game");
    }
}
