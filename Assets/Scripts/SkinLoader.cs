using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class SkinLoader : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Material skin1;
    [SerializeField] private Material skin2;
    [SerializeField] private Material skin3;
    [SerializeField] private Material skin4;
    [SerializeField] private Material skin5;
    [SerializeField] private Material skin6;
    [SerializeField] private Material skin7;
    [SerializeField] private Material skin8;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject icon;


    public void Skin1ButtonClicked()//Dirt
    {
        player.GetComponent<MeshRenderer>().material = skin1;
        icon.GetComponent<MeshRenderer>().material = skin1;
        References.currentSkin = skin1;
        References.currentSkinName = "skin_dirt";
        DatabaseManager.EquipSkin("skin_dirt");
    }
    public void Skin2ButtonClicked()
    {
        player.GetComponent<MeshRenderer>().material = skin2;
        icon.GetComponent<MeshRenderer>().material = skin2;
        References.currentSkin = skin2;
        References.currentSkinName = "skin_copper";
        DatabaseManager.EquipSkin("skin_copper");
    }
    public void Skin3ButtonClicked()
    {
        player.GetComponent<MeshRenderer>().material = skin3;
        icon.GetComponent<MeshRenderer>().material = skin3;
        References.currentSkin = skin3;
        References.currentSkinName = "skin_gold";
        DatabaseManager.EquipSkin("skin_gold");
    }
    public void Skin4ButtonClicked()
    {
        player.GetComponent<MeshRenderer>().material = skin4;
        icon.GetComponent<MeshRenderer>().material = skin4;
        References.currentSkin = skin4;
        References.currentSkinName = "skin_sapphire";
        DatabaseManager.EquipSkin("skin_sapphire");
    }
    public void Skin5ButtonClicked()
    {
        player.GetComponent<MeshRenderer>().material = skin5;
        icon.GetComponent<MeshRenderer>().material = skin5;
        References.currentSkin = skin5;
        References.currentSkinName = "skin_purple";
        DatabaseManager.EquipSkin("skin_purple");
    }
    public void Skin6ButtonClicked()
    {
        player.GetComponent<MeshRenderer>().material = skin6;
        icon.GetComponent<MeshRenderer>().material = skin6;
        References.currentSkin = skin6;
        References.currentSkinName = "skin_grass";
        DatabaseManager.EquipSkin("skin_grass");
    }
    public void Skin7ButtonClicked()
    {
        player.GetComponent<MeshRenderer>().material = skin7;
        icon.GetComponent<MeshRenderer>().material = skin7;
        References.currentSkin = skin7;
        References.currentSkinName = "skin_matrix";
        DatabaseManager.EquipSkin("skin_matrix");
    }
    public void Skin8ButtonClicked()
    {
        player.GetComponent<MeshRenderer>().material = skin8;
        icon.GetComponent<MeshRenderer>().material = skin8;
        References.currentSkin = skin8;
        References.currentSkinName = "skin_sus";
        DatabaseManager.EquipSkin("skin_sus");
    }

}
