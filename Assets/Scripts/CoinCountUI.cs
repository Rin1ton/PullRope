using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCountUI : MonoBehaviour
{

    GameObject player;
    private References.localPlayerData _myPlayer;

    public Text coinCount;
    // Start is called before the first frame update
    void Start()
    {

        _myPlayer = DatabaseManager.MyPlayer;
        coinCount.text = "Balance: " + _myPlayer.coincount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
