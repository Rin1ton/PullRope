using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject PauseMenuObject;
    // Start is called before the first frame update
    void Start()
    {
        PauseMenuObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            QuitGame();
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main Menu"))
        {
            Destroy(PauseMenuObject);
            Destroy(this);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting Game");
    }
}
