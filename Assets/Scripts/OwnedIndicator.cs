using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OwnedIndicator : MonoBehaviour
{
    public Text skin1Owned;
    public Text skin2Owned;
    public Text skin3Owned;
    public Text skin4Owned;
    public Text skin5Owned;
    public Text skin6Owned;
    public Text skin7Owned;
    public Text skin8Owned;


    GameObject player;
    private References.localPlayerData _myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _myPlayer = DatabaseManager.MyPlayer;

        if(_myPlayer.cosmetic_dirt == 0)
        {
            skin1Owned.text = "$25";
        }
        else
        {
            skin1Owned.text = "OWNED";
        }

        if (_myPlayer.cosmetic_copper == 0)
        {
            skin2Owned.text = "$50";
        }
        else
        {
            skin2Owned.text = "OWNED";
        }

        if (_myPlayer.cosmetic_gold == 0)
        {
            skin3Owned.text = "$100";
        }
        else
        {
            skin3Owned.text = "OWNED";
        }

        if (_myPlayer.cosmetic_sapphire == 0)
        {
            skin4Owned.text = "$125";
        }
        else
        {
            skin4Owned.text = "OWNED";
        }

        if (_myPlayer.cosmetic_purple == 0)
        {
            skin5Owned.text = "$55";
        }
        else
        {
            skin5Owned.text = "OWNED";
        }

        if (_myPlayer.cosmetic_grass == 0)
        {
            skin6Owned.text = "$40";
        }
        else
        {
            skin6Owned.text = "OWNED";
        }

        if (_myPlayer.cosmetic_matrix == 0)
        {
            skin7Owned.text = "$200";
        }
        else
        {
            skin7Owned.text = "OWNED";
        }

        if (_myPlayer.cosmetic_sus == 0)
        {
            skin8Owned.text = "$999";
        }
        else
        {
            skin8Owned.text = "OWNED";
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
