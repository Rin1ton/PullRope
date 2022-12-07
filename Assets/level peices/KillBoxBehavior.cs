using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillBoxBehavior : MonoBehaviour
{
    public AudioClip deathSound;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = References.thePlayer;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
  
        if (deathSound != null)
        {
            Debug.Log("Death");
            AudioSource.PlayClipAtPoint(deathSound, player.transform.position, 1);
        }
    }
    /*
     * Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
     * You forgot the parentheses at the end of GetActiveScene(); It actually works this way! :)     */

}
