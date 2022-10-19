using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class References
{
	public static GameObject thePlayer;

	public static float playerSensitivityX = 1;
	public static float playerSensitivityY = 1;
	public static Color32 CrosshairColor;

	public struct localPlayerData
	{
		public string username;
		public string password;
		public int coincount;
		public string equipped;
		public int cosmetic_copper;
		public int cosmetic_dirt;
		public int cosmetic_gold;
		public int cosmetic_grass;
		public int cosmetic_matrix;
		public int cosmetic_purple;
		public int cosmetic_sapphire;
		public int cosmetic_sus;
	}

	//public static localPlayerData myPlayer = DatabaseManager.CreatePlayer("");

	private static localPlayerData _myPlayer;
	public static localPlayerData MyPlayer
	{
		get {
			if (_myPlayer.username == "")
			{
				//INSERT: code that updates the _myPlayer object with what's in the database
			}

			return _myPlayer;
		}
		set
		{
			_myPlayer = value;

			//INSERT: code that updates the player row in the database with what's in the _myPlayer object
		}
	}

	public static string AttemptLogin(string username, string password)
	{
		//INSERT: code that will set the MyPlayer [sic] object to the row specified by the username and password, if it exists.

		//return one of these strings (or any other messages you think we'd need) depending on the outcome of the login attempt.
		if (true)
		{
			return "Login Successful!";
		}
		else
		{
			return "Incorrect Username and Password!";
		}

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



}
