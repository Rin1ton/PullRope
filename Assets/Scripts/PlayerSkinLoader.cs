using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Google.Protobuf.WellKnownTypes.Field.Types;

public class PlayerSkinLoader : MonoBehaviour
{
    GameObject player;
    Material skin;
    private References.localPlayerData _myPlayer;

    // Start is called before the first frame update
    [SerializeField] public static Material defaultSkin;
    void Start()
    {
        player = References.thePlayer;
        _myPlayer = DatabaseManager.MyPlayer;
        skin = References.currentSkin;
        if (_myPlayer.equipped == "skin_dirt")
        {
            player.GetComponent<MeshRenderer>().material = SkinLoader.skin1;
        }
        if (_myPlayer.equipped == "skin_copper")
        {
            player.GetComponent<MeshRenderer>().material = SkinLoader.skin2;
        }
        if (_myPlayer.equipped == "skin_gold")
        {
            player.GetComponent<MeshRenderer>().material = SkinLoader.skin3;
        }
        if (_myPlayer.equipped == "skin_sapphire")
        {
            player.GetComponent<MeshRenderer>().material = SkinLoader.skin4;
        }
        if (_myPlayer.equipped == "skin_purple")
        {
            player.GetComponent<MeshRenderer>().material = SkinLoader.skin5;
        }
        if (_myPlayer.equipped == "skin_grass")
        {
            player.GetComponent<MeshRenderer>().material = SkinLoader.skin6;
        }
        if (_myPlayer.equipped == "skin_matrix")
        {
            player.GetComponent<MeshRenderer>().material = SkinLoader.skin7;
        }
        if (_myPlayer.equipped == "skin_sus")
        {
            player.GetComponent<MeshRenderer>().material = SkinLoader.skin8;
        }


        //player.GetComponent<MeshRenderer>().material = skin;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
