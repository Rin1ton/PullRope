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

    // Start is called before the first frame update
    void Update()
    {

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
        PauseMenuObject.SetActive(false);
        Crosshair.SetActive(true);
        Time.timeScale = 1f;
        timertext.playing = true;
        GamePaused = false;
        Debug.Log("Resuming Game");
    }

    void Pause()
    {
        PauseMenuObject.SetActive(true);
        Crosshair.SetActive(false);
        Time.timeScale = 0f;
        timertext.playing = false;
        GamePaused = true;
        Debug.Log("Pausing Game");
    }
}
