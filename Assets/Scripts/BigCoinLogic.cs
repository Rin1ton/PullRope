using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BigCoinLogic : MonoBehaviour
{
    GameObject player;
    private References.localPlayerData _myPlayer;

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
        Destroy(this.gameObject);
    }


    void Update()
    {
        transform.Rotate(0, (float).45, 0);
    }
}
