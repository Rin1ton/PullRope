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
    private References.localPlayerData _myPlayer;
    public Text coinCount;
    private void Awake()
    {
        /*defaultSkin = Resources.Load("skin_default") as Material;
        skin1 = Resources.Load("skin_dirt") as Material;
        skin2 = Resources.Load("skin_copper") as Material;
        skin3 = Resources.Load("skin_gold") as Material;
        skin4 = Resources.Load("skin_sapphire") as Material;
        skin5 = Resources.Load("skin_purple") as Material;
        skin6 = Resources.Load("skin_grass") as Material;
        skin7 = Resources.Load("skin_matrix") as Material;
        skin8 = Resources.Load("skin_sus") as Material;
        */
    }

	public static Material SkinNameToMaterial(string skinName)
	{
		switch (skinName)
		{
			case "skin_dirt":
				return skin1;
			case "skin_copper":
				return skin2;
			case "skin_gold":
				return skin3;
			case "skin_sapphire":
				return skin4;
			case "skin_purple":
				return skin5;
			case "skin_grass":
				return skin6;
			case "skin_matrix":
				return skin7;
			case "skin_sus":
				return skin8;
			default:
				return defaultSkin;
		}
	}

	public void Skin1ButtonClicked()//Dirt
    {
        _myPlayer = DatabaseManager.MyPlayer;
        if (_myPlayer.cosmetic_dirt == 1)
        {
            player.GetComponent<MeshRenderer>().material = skin1;
            References.currentSkin = skin1;
            References.currentSkinName = "skin_dirt";
            DatabaseManager.EquipSkin("skin_dirt");
            _myPlayer.equipped = "skin_dirt";
            DatabaseManager.MyPlayer = _myPlayer;
        }
        else
        {
            return;
        }        
    }
    public void Skin1Purchase()
    {
        _myPlayer = DatabaseManager.MyPlayer;
        if (_myPlayer.cosmetic_dirt == 0)
        {
            if(_myPlayer.coincount >= 25)
            {
                _myPlayer.coincount -= 25;
                coinCount.text = "Balance: " + _myPlayer.coincount.ToString();
                _myPlayer.cosmetic_dirt = 1;
                DatabaseManager.MyPlayer = _myPlayer;
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
        
    }
    public void Skin2ButtonClicked()
    {
        _myPlayer = DatabaseManager.MyPlayer;
        if (_myPlayer.cosmetic_copper == 1)
        {
            player.GetComponent<MeshRenderer>().material = skin2;
            References.currentSkin = skin2;
            References.currentSkinName = "skin_copper";
            DatabaseManager.EquipSkin("skin_copper");
            _myPlayer.equipped = "skin_copper";
            DatabaseManager.MyPlayer = _myPlayer;
        }
        else
        {
            return;
        }
    }
    public void Skin2Purchase()
    {
        _myPlayer = DatabaseManager.MyPlayer;
        if (_myPlayer.cosmetic_copper == 0)
        {
            if (_myPlayer.coincount >= 25)
            {
                _myPlayer.coincount -= 50;
                coinCount.text = "Balance: " + _myPlayer.coincount.ToString();
                _myPlayer.cosmetic_copper = 1;
                DatabaseManager.MyPlayer = _myPlayer;
            }
            else
            {
                return;
            }
                
        }
        else
        {
            return;
        }
    }
    public void Skin3ButtonClicked()
    {
        _myPlayer = DatabaseManager.MyPlayer;
        if (_myPlayer.cosmetic_gold == 1)
        {
            player.GetComponent<MeshRenderer>().material = skin3;
            References.currentSkin = skin3;
            References.currentSkinName = "skin_gold";
            _myPlayer.equipped = "skin_gold";
            DatabaseManager.EquipSkin("skin_gold");
        }
        else
        {
            return;
        }
    }
    public void Skin3Purchase()
    {
        _myPlayer = DatabaseManager.MyPlayer;
        if (_myPlayer.cosmetic_gold == 0)
        {
            if (_myPlayer.coincount >= 100)
            {
                _myPlayer.coincount -= 100;
                coinCount.text = "Balance: " + _myPlayer.coincount.ToString();
                _myPlayer.cosmetic_gold = 1;
                DatabaseManager.MyPlayer = _myPlayer;
            }
            else
            {
                return;
            }
            
        }
        else
        {
            return;
        }
    }
    public void Skin4ButtonClicked()
    {
        _myPlayer = DatabaseManager.MyPlayer;
        if (_myPlayer.cosmetic_sapphire == 1)
        {
            player.GetComponent<MeshRenderer>().material = skin4;
            References.currentSkin = skin4;
            References.currentSkinName = "skin_sapphire";
            DatabaseManager.EquipSkin("skin_sapphire");
            _myPlayer.equipped = "skin_sapphire";
            DatabaseManager.MyPlayer = _myPlayer;
        }
        else
        {
            return;
        }
    }
    public void Skin4Purchase()
    {
        _myPlayer = DatabaseManager.MyPlayer;
        if (_myPlayer.cosmetic_sapphire == 0)
        {
            if (_myPlayer.coincount >= 125)
            {
                _myPlayer.coincount -= 125;
                coinCount.text = "Balance: " + _myPlayer.coincount.ToString();
                _myPlayer.cosmetic_sapphire = 1;
                DatabaseManager.MyPlayer = _myPlayer;
            }
            else
            {
                return;
            }
            
        }
        else
        {
            return;
        }
        
    }
    public void Skin5ButtonClicked()
    {
        _myPlayer = DatabaseManager.MyPlayer;
        if (_myPlayer.cosmetic_purple == 1)
        {
            player.GetComponent<MeshRenderer>().material = skin5;
            References.currentSkin = skin5;
            References.currentSkinName = "skin_purple";
            DatabaseManager.EquipSkin("skin_purple");
            _myPlayer.equipped = "skin_purple";
            DatabaseManager.MyPlayer = _myPlayer;
        }
        else
        {
            return;
        }
    }
    public void Skin5Purchase()
    {
        _myPlayer = DatabaseManager.MyPlayer;
        if (_myPlayer.cosmetic_purple == 0)
        {
            if (_myPlayer.coincount >= 55)
            {
                _myPlayer.coincount -= 55;
                coinCount.text = "Balance: " + _myPlayer.coincount.ToString();
                _myPlayer.cosmetic_purple = 1;
                DatabaseManager.MyPlayer = _myPlayer;
            }
            else
            {
                return;
            }
            
        }
        else
        {
            return;
        }
        
    }
    public void Skin6ButtonClicked()
    {
        _myPlayer = DatabaseManager.MyPlayer;
        if (_myPlayer.cosmetic_grass == 1)
        {
            player.GetComponent<MeshRenderer>().material = skin6;
            References.currentSkin = skin6;
            References.currentSkinName = "skin_grass";
            DatabaseManager.EquipSkin("skin_grass");
            _myPlayer.equipped = "skin_grass";
            DatabaseManager.MyPlayer = _myPlayer;
        }
        else
        {
            return;
        }
    }
    public void Skin6Purchase()
    {
        _myPlayer = DatabaseManager.MyPlayer;
        if (_myPlayer.cosmetic_grass == 0)
        {
            if (_myPlayer.coincount >= 40)
            {
                _myPlayer.coincount -= 40;
                coinCount.text = "Balance: " + _myPlayer.coincount.ToString();
                _myPlayer.cosmetic_grass = 1;
                DatabaseManager.MyPlayer = _myPlayer;
            }
            else
            {
                return;
            }
            
        }
        else
        {
            return;
        }
        
    }
    public void Skin7ButtonClicked()
    {
        _myPlayer = DatabaseManager.MyPlayer;
        if (_myPlayer.cosmetic_matrix == 1)
        {
            player.GetComponent<MeshRenderer>().material = skin7;
            References.currentSkin = skin7;
            References.currentSkinName = "skin_matrix";
            DatabaseManager.EquipSkin("skin_matrix");
            _myPlayer.equipped = "skin_matrix";
            DatabaseManager.MyPlayer = _myPlayer;
        }
        else
        {
            return;
        }
    }
    public void Skin7Purchase()
    {
        _myPlayer = DatabaseManager.MyPlayer;
        if (_myPlayer.cosmetic_matrix == 0)
        {
            if (_myPlayer.coincount >= 200)
            {
                _myPlayer.coincount -= 200;
                coinCount.text = "Balance: " + _myPlayer.coincount.ToString();
                _myPlayer.cosmetic_matrix = 1;
                DatabaseManager.MyPlayer = _myPlayer;
            }
            else
            {
                return;
            }
            
        }
        else
        {
            return;
        }
        
    }
    public void Skin8ButtonClicked()
    {
        _myPlayer = DatabaseManager.MyPlayer;
        if (_myPlayer.cosmetic_sus == 1)
        {
            player.GetComponent<MeshRenderer>().material = skin8;
            References.currentSkin = skin8;
            References.currentSkinName = "skin_sus";
            DatabaseManager.EquipSkin("skin_sus");
            _myPlayer.equipped = "skin_sus";
            DatabaseManager.MyPlayer = _myPlayer;
        }
        else
        {
            return;
        }
    }
    public void Skin8Purchase()
    {
        _myPlayer = DatabaseManager.MyPlayer;
        if (_myPlayer.cosmetic_sus == 0)
        {
            if (_myPlayer.coincount >= 999)
            {
                _myPlayer.coincount -= 999;
                coinCount.text = "Balance: " + _myPlayer.coincount.ToString();
                _myPlayer.cosmetic_sus = 1;
                DatabaseManager.MyPlayer = _myPlayer;
            }
            else
            {
                return;
            }
            
        }
        else
        {
            return;
        }
        
    }

}
