using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class References
{
	public static GameObject thePlayer;

	public static float playerSensitivityX = 1;
	public static float playerSensitivityY = 1;
	public static Color32 CrosshairColor;

	public struct PlayerData
	{
		string username;
		string password;
		int coincount;
		string equipped;
		int cosmetic_copper;
		int cosmetic_dirt;
		int cosmetic_gold;
		int cosmetic_grass;
		int cosmetic_matrix;
		int cosmetic_purple;
		int cosmetic_sapphire;
		int cosmetic_sus;
	}

	/*
	string username;
	string password;
	int coincount;
	string equipped;
	int cosmetic_copper;
	int cosmetic_dirt;
	int cosmetic_gold;
	int cosmetic_grass;
	int cosmetic_matrix;
	int cosmetic_purple;
	int cosmetic_sapphire;
	int cosmetic_sus;
	*/

	public static PlayerData localPlayerData;

}
