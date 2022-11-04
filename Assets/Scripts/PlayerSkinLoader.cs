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
    void Start()
    {
        player = References.thePlayer;
        skin = References.currentSkin;

        player.GetComponent<MeshRenderer>().material = skin;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
