using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public UITimerScript timertext;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       // timertext.reset = false;
    }

    public void ToMain()
    {
        SceneManager.LoadScene("Main Menu");

        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;

       
    }

    public void ToSkins()
    {
        SceneManager.LoadScene("Skin Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting Game");
    }

    public void ToMultiplayer()
    {
        SceneManager.LoadScene("Multiplayer Menu");
    }

}
