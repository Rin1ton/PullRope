using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipUI : MonoBehaviour
{
    GameObject player;
    private References.localPlayerData _myPlayer;

    public Text equip;

    // Start is called before the first frame update
    void Start()
    {
        _myPlayer = DatabaseManager.MyPlayer;

        if (_myPlayer.equipped == "skin_dirt")
        {
            equip.text = "Equipped: Dirt";
        }
        if (_myPlayer.equipped == "skin_copper")
        {
            equip.text = "Equipped: Copper";
        }
        if (_myPlayer.equipped == "skin_gold")
        {
            equip.text = "Equipped: Gold";
        }
        if (_myPlayer.equipped == "skin_sapphire")
        {
            equip.text = "Equipped: Sapphire";
        }
        if (_myPlayer.equipped == "skin_purple")
        {
            equip.text = "Equipped: Purple";
        }
        if (_myPlayer.equipped == "skin_grass")
        {
            equip.text = "Equipped: Grass";
        }
        if (_myPlayer.equipped == "skin_matrix")
        {
            equip.text = "Equipped: Matrix";
        }
        if (_myPlayer.equipped == "skin_sus")
        {
            equip.text = "Equipped: Sus";
        }
        if (_myPlayer.equipped == "skin_default")
        {
            equip.text = "Equipped: Default";
        }

        DatabaseManager.MyPlayer = _myPlayer;
    }
}
