using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static References;
using MySql.Data.MySqlClient;

public static class DatabaseManager
{

	private static localPlayerData _myPlayer;
	public static localPlayerData MyPlayer
	{
		get
		{
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

	public static localPlayerData CreatePlayer(string username)
    {
        localPlayerData newPlayer = new localPlayerData();

        string server = "localhost";
        string database = "pullrope";
        string dbusername = "root";
        string password = "password";

        string connstring = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + dbusername + ";" + "PASSWORD=" + password + ";";

        /*
        string query; // SQL Statement
        MySqlConnection connection = new MySqlConnection(connstring);
        MySqlCommand command;
        MySqlDataReader reader;

        connection.Open();
        query = "SELECT * FROM prData WHERE username = @username";
        command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@username", username);

        reader = command.ExecuteReader();

        // While reading database
        while (reader.Read())
        {
            newPlayer.username = reader["username"];
            newPlayer.password = reader["password"];
            newPlayer.coincount = reader["coincount"];
            newPlayer.equipped = reader["equipped"];
            newPlayer.cosmetic_copper = reader["cosmetic_copper"];
            newPlayer.cosmetic_dirt = reader["cosmetic_dirt"];
            newPlayer.cosmetic_gold = reader["cosmetic_gold"];
            newPlayer.cosmetic_grass = reader["cosmetic_grass"];
            newPlayer.cosmetic_matrix = reader["cosmetic_matrix"];
            newPlayer.cosmetic_purple = reader["cosmetic_purple"];
            newPlayer.cosmetic_sapphire = reader["cosmetic_sapphire"];
            newPlayer.cosmetic_sus = reader["cosmetic_sus"];
        }
        connection.Close();
        */

        return newPlayer;
    }
}
