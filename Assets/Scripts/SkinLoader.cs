using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class SkinLoader : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public static Material skin1;
    [SerializeField] public static Material skin2;
    [SerializeField] public static Material skin3;
    [SerializeField] public static Material skin4;
    [SerializeField] public static Material skin5;
    [SerializeField] public static Material skin6;
    [SerializeField] public static Material skin7;
    [SerializeField] public static Material skin8;
    [SerializeField] public static Material defaultSkin;
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject icon;

    private void Awake()
    {
        defaultSkin = Resources.Load("skin_default") as Material;
        skin1 = Resources.Load("skin_dirt") as Material;
        skin2 = Resources.Load("skin_copper") as Material;
        skin3 = Resources.Load("skin_gold") as Material;
        skin4 = Resources.Load("skin_sapphire") as Material;
        skin5 = Resources.Load("skin_purple") as Material;
        skin6 = Resources.Load("skin_grass") as Material;
        skin7 = Resources.Load("skin_matrix") as Material;
        skin8 = Resources.Load("skin_sus") as Material;
        
    }

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
