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


    public void Skin1ButtonClicked()
    {
        player.GetComponent<MeshRenderer>().material = skin1;
        icon.GetComponent<MeshRenderer>().material = skin1;
        References.currentSkin = skin1;
    }
    public void Skin2ButtonClicked()
    {
        player.GetComponent<MeshRenderer>().material = skin2;
        icon.GetComponent<MeshRenderer>().material = skin2;
        References.currentSkin = skin2;
    }
    public void Skin3ButtonClicked()
    {
        player.GetComponent<MeshRenderer>().material = skin3;
        icon.GetComponent<MeshRenderer>().material = skin3;
        References.currentSkin = skin3;
    }
    public void Skin4ButtonClicked()
    {
        player.GetComponent<MeshRenderer>().material = skin4;
        icon.GetComponent<MeshRenderer>().material = skin4;
        References.currentSkin = skin4;
    }
    public void Skin5ButtonClicked()
    {
        player.GetComponent<MeshRenderer>().material = skin5;
        icon.GetComponent<MeshRenderer>().material = skin5;
        References.currentSkin = skin5;
    }
    public void Skin6ButtonClicked()
    {
        player.GetComponent<MeshRenderer>().material = skin6;
        icon.GetComponent<MeshRenderer>().material = skin6;
        References.currentSkin = skin6;
    }
    public void Skin7ButtonClicked()
    {
        player.GetComponent<MeshRenderer>().material = skin7;
        icon.GetComponent<MeshRenderer>().material = skin7;
        References.currentSkin = skin7;
    }
    public void Skin8ButtonClicked()
    {
        player.GetComponent<MeshRenderer>().material = skin8;
        icon.GetComponent<MeshRenderer>().material = skin8;
        References.currentSkin = skin8;
    }

}
