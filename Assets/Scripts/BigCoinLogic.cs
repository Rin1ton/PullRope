using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BigCoinLogic : MonoBehaviour
{
    GameObject player;
    private References.localPlayerData _myPlayer;
    public AudioClip coinSound;
    int coinAmount;
    void Start()
    {
        player = References.thePlayer;


    }
    private void OnCollisionEnter(Collision collision)
    {
        _myPlayer = DatabaseManager.MyPlayer;
        _myPlayer.coincount += 10;
        DatabaseManager.MyPlayer = _myPlayer;
        if (coinSound != null)
            AudioSource.PlayClipAtPoint(coinSound, transform.position, 1);
        Destroy(this.gameObject);
    }


    void Update()
    {
        transform.Rotate(0, (float).45, 0);
    }
}
