using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static References;
using MySql.Data.MySqlClient;
using System;

// MAKE SURE TO UPDATE THE DATABASE INFORMATION WITH YOUR LOCAL HOST
// MAKE SURE TO ADD THE prData DATABASE TO YOUR MYSQL
//
// use pullrope
//
// create table if not exists prData (username varchar(30) not null primary key, password varchar(30) not null,
// coincount int not null, equipped varchar(30) not null, cosmetic_copper int not null, cosmetic_dirt int not null,
// cosmetic_gold int not null, cosmetic_grass int not null, cosmetic_matrix int not null, cosmetic_purple int not null,
// cosmetic_sapphire int not null, cosmetic_sus int not null);

public static class DatabaseManager
{
	static string universalPassword = DatabaseCredentials.databasePassword;

	public static bool LocalPlayerIsGuest { get; private set; } = false;

	private static localPlayerData _myPlayer;
	public static localPlayerData MyPlayer
	{
		get
		{
			return _myPlayer;
		}
		set
		{
			_myPlayer = value;
			if (!LocalPlayerIsGuest)
				UpdateDatabase(_myPlayer);
		}
	}

	public static void AdjustCoins(int coinChange) // Adjust the amount of coins the player has (send negative value to subtract)
	{
		_myPlayer = MyPlayer;
		_myPlayer.coincount += coinChange;
		MyPlayer = _myPlayer;
	}

	private static string Encrypt(string original) // Encrypt the inserted string
	{
		string encrypted = "";
		int length = original.Length - 1;

		for (int i = 0; i < length + 1; i++)
		{
			if (original[length - i] == 'a')
			{
				encrypted += "*";
			}
			else
			{
				encrypted += original[length - i];
			}
		}

		return encrypted;
	}

	private static string Decrypt(string original) // Decrypt the inserted string
	{
		string decrypted = "";
		int length = original.Length - 1;

		for (int i = 0; i < length + 1; i++)
		{
			if (original[length - i] == '*')
			{
				decrypted += "a";
			}
			else
			{
				decrypted += original[length - i];
			}
		}

		return decrypted;
	}

	public static void UnlockSkin(string skinName) //function to update database with the input skin unlocked
	{
		_myPlayer = MyPlayer; // Update myPlayer with current database info

		if (skinName == "skin_copper") // Check to see if skin exists, if so, mark it as earned
		{
			_myPlayer.cosmetic_copper = 1;
		}
		else if (skinName == "skin_dirt")
		{
			_myPlayer.cosmetic_dirt = 1;
		}
		else if (skinName == "skin_gold")
		{
			_myPlayer.cosmetic_gold = 1;
		}
		else if (skinName == "skin_grass")
		{
			_myPlayer.cosmetic_grass = 1;
		}
		else if (skinName == "skin_matrix")
		{
			_myPlayer.cosmetic_matrix = 1;
		}
		else if (skinName == "skin_purple")
		{
			_myPlayer.cosmetic_purple = 1;
		}
		else if (skinName == "skin_sapphire")
		{
			_myPlayer.cosmetic_sapphire = 1;
		}
		else if (skinName == "skin_sus")
		{
			_myPlayer.cosmetic_sus = 1;
		}

		MyPlayer = _myPlayer; // Add the information to the database
	}

	public static void EquipSkin(string skinName) //function to update database with the input skin equipped
	{
		_myPlayer = MyPlayer; // Update myPlayer with current database info

		if (skinName == "skin_copper") // Check to see if skin exists, if so, equip it
		{
			_myPlayer.equipped = skinName;
		}
		else if (skinName == "skin_dirt")
		{
			_myPlayer.equipped = skinName;
		}
		else if (skinName == "skin_gold")
		{
			_myPlayer.equipped = skinName;
		}
		else if (skinName == "skin_grass")
		{
			_myPlayer.equipped = skinName;
		}
		else if (skinName == "skin_matrix")
		{
			_myPlayer.equipped = skinName;
		}
		else if (skinName == "skin_purple")
		{
			_myPlayer.equipped = skinName;
		}
		else if (skinName == "skin_sapphire")
		{
			_myPlayer.equipped = skinName;
		}
		else if (skinName == "skin_sus")
		{
			_myPlayer.equipped = skinName;
		}

		MyPlayer = _myPlayer; // Add the information to the database
	}

	public static string AttemptLogin(string username, string password) // Try to log into server using a username and password. User object will be updated with player data from server
	{
		if (username.ToLower() == "guest") // If guest login
		{
			LocalPlayerIsGuest = true;
			MyPlayer = CreateDummy(); // Creates a guest account
			return "Logging in as Guest!";
		}

		try // If requires database usage
		{
            if (CheckDatabase(username, password)) // If username and password are correct
            {
                MyPlayer = UpdateUser(username); // Set the myPlayer object to the data from the database
                return "Login Successful!";
            }
            else
            {
                return "Incorrect Username and/or Password!";
            }
        }
		catch
		{
			return "Please create a guest account.";
		}

	}

	public static localPlayerData CreateDummy() // Creates a "fake" player account
	{
		localPlayerData newPlayer = new localPlayerData();

		// Randomly generate username

		//string dummyUsername = "ph";

		string[] fakeUsernames = {
			"Rock",
			"Rat",
			"Bone",
			"Bulky Roll",
			"Big Cheese",
			"Stinky Binky",
			"Blimp",
			"Gob",
			"Peacock",
			"Kanye",
			"Jesus Christ Himself",
			"Orack Barama",
			"Skrunko",
			"Sam Serious Stone",
			"Corben McFree",
			"Mike Tyson",
			"Morbius",
			"BEan",
			"Cynder",
			"Griff the Grub",
			"Bill Nye the Chill Guy",
			"Krinko",
			"Big Mac",
			"two juicebox one straw",
			"evaporated bean",
			"cement-addicted",
			"three toes mcgee",
			"2006 toyota camry",
			"ankle biter",
			"Double Lettuce",
			"Angry Preg-nocerous",
			"Brian D",
			"Bobo Denkirk",
			"2006 toyota camry"
		};

		int nameIndex = UnityEngine.Random.Range(0, fakeUsernames.Length - 1);

		newPlayer.username = fakeUsernames[nameIndex];
		newPlayer.password = "N/A";
		newPlayer.coincount = 0;
		newPlayer.equipped = "skin_default";
		newPlayer.cosmetic_copper = 0;
		newPlayer.cosmetic_dirt = 0;
		newPlayer.cosmetic_gold = 0;
		newPlayer.cosmetic_grass = 0;
		newPlayer.cosmetic_matrix = 0;
		newPlayer.cosmetic_purple = 0;
		newPlayer.cosmetic_sapphire = 0;
		newPlayer.cosmetic_sus = 0;

		return newPlayer;
	}

	public static localPlayerData UpdateUser(string username) // Update user information to that of the database
	{
		localPlayerData newPlayer = new localPlayerData();

		string server = "127.0.0.01";
		string database = "pullrope";
		string dbusername = "root";
		string password = universalPassword;

		string connstring = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + dbusername + ";" + "PASSWORD=" + password + ";";

		string query; // SQL Statement
		MySqlConnection connection = new MySqlConnection(connstring);
		MySqlCommand command;
		MySqlDataReader reader;

		connection.Open(); // Connect to server
		query = "SELECT * FROM prData WHERE username = @username"; // Get all data from this particular user
		command = new MySqlCommand(query, connection);
		command.Parameters.AddWithValue("@username", username); // Add username to query

		reader = command.ExecuteReader();

		// While reading database
		while (reader.Read()) // Read all information from that particular user and insert it into the newPlayer object
		{
			newPlayer.username = reader["username"].ToString();
			newPlayer.password = Decrypt(reader["password"].ToString());
			newPlayer.coincount = Int32.Parse(reader["coincount"].ToString());
			newPlayer.equipped = reader["equipped"].ToString();
			newPlayer.cosmetic_copper = Int32.Parse(reader["cosmetic_copper"].ToString());
			newPlayer.cosmetic_dirt = Int32.Parse(reader["cosmetic_dirt"].ToString());
			newPlayer.cosmetic_gold = Int32.Parse(reader["cosmetic_gold"].ToString());
			newPlayer.cosmetic_grass = Int32.Parse(reader["cosmetic_grass"].ToString());
			newPlayer.cosmetic_matrix = Int32.Parse(reader["cosmetic_matrix"].ToString());
			newPlayer.cosmetic_purple = Int32.Parse(reader["cosmetic_purple"].ToString());
			newPlayer.cosmetic_sapphire = Int32.Parse(reader["cosmetic_sapphire"].ToString());
			newPlayer.cosmetic_sus = Int32.Parse(reader["cosmetic_sus"].ToString());
		}
		connection.Close(); // Close the connection

		return newPlayer; // Return the data
	}

	public static void UpdateDatabase(localPlayerData player) // Update data information to that of the user
	{
		string server = "127.0.0.01";
		string database = "pullrope";
		string dbusername = "root";
		string password = universalPassword;

		string connstring = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + dbusername + ";" + "PASSWORD=" + password + ";";

		string query; // SQL Statement
		MySqlConnection connection = new MySqlConnection(connstring);
		MySqlCommand command;

		connection.Open(); // Connect to server
		query = "UPDATE prData SET equipped = @equipped, coincount = @coincount, cosmetic_copper = @cosmetic_copper, " +
			"cosmetic_dirt = @cosmetic_dirt, cosmetic_gold = @cosmetic_gold, cosmetic_grass = @cosmetic_grass, " +
			"cosmetic_matrix = @cosmetic_matrix, cosmetic_purple = @cosmetic_purple, " +
			"cosmetic_sapphire = @cosmetic_sapphire, cosmetic_sus = @cosmetic_sus WHERE username = @username";
		command = new MySqlCommand(query, connection); // Set all information of a particular username to the player object values

		command.Parameters.AddWithValue("@username", player.username);
		command.Parameters.AddWithValue("@equipped", player.equipped);
		command.Parameters.AddWithValue("@coincount", player.coincount);
		command.Parameters.AddWithValue("@cosmetic_copper", player.cosmetic_copper);
		command.Parameters.AddWithValue("@cosmetic_dirt", player.cosmetic_dirt);
		command.Parameters.AddWithValue("@cosmetic_gold", player.cosmetic_gold);
		command.Parameters.AddWithValue("@cosmetic_grass", player.cosmetic_grass);
		command.Parameters.AddWithValue("@cosmetic_matrix", player.cosmetic_matrix);
		command.Parameters.AddWithValue("@cosmetic_purple", player.cosmetic_purple);
		command.Parameters.AddWithValue("@cosmetic_sapphire", player.cosmetic_sapphire);
		command.Parameters.AddWithValue("@cosmetic_sus", player.cosmetic_sus);

		command.ExecuteNonQuery(); // Execute Query
		connection.Close(); // Close connection
	}

	public static bool CheckDatabase(string username, string pass) // See if a particular username and password is valid
	{
		string server = "127.0.0.01";
		string database = "pullrope";
		string dbusername = "root";
		string password = universalPassword;

		string connstring = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + dbusername + ";" + "PASSWORD=" + password + ";";

		string query; // SQL Statement
		MySqlConnection connection = new MySqlConnection(connstring);
		MySqlCommand command;

		connection.Open(); // Connect to server

		query = "SELECT password FROM prData WHERE username = @username"; // Get password from user object
		command = new MySqlCommand(query, connection);

		command.Parameters.AddWithValue("@username", username);

		var nId = command.ExecuteScalar();

		if (nId != null) // If user exists
		{
			MySqlDataReader reader = command.ExecuteReader();
			reader.Read();
			if (pass == Decrypt(reader["password"].ToString())) // If password is correct
			{
				return true;
			}
			else
				return false;
		}
		else // If user does not exist
		{
			return false;
		}
	}

	public static string AttemptCreate(string username, string pass) // Create an account. Checks if username is valid.
	{
		string output = "Account Created!";

		//INSERT: code to create an account with username and password if an account with that username doesn't already exist
		string server = "127.0.0.01";
		string database = "pullrope";
		string dbusername = "root";
		string password = universalPassword;

		string connstring = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + dbusername + ";" + "PASSWORD=" + password + ";";

		string query; // SQL Statement
		MySqlConnection connection = new MySqlConnection(connstring);
		MySqlCommand command;

		connection.Open(); // Connect to server

		query = "SELECT password FROM prData WHERE username = @username"; // Get password from user object
		command = new MySqlCommand(query, connection);

		if (username == "Guest" || username == "guest" || username == "GUEST") // If trying to overwrite guest user
		{
			return "Invalid username.";
		}

		for (int i = 0; i < username.Length; i++) // For length of username
		{
			if (username[i] == ' ' || username[i] == ';' || username[i] == '*') // If username has invalid letter
			{
				return "Invalid username."; // Cancel process
			}
		}

		for (int i = 0; i < pass.Length; i++) // For length of password
		{
			if (pass[i] == ' ' || pass[i] == ';' || pass[i] == '*') // If password has invalid letter
			{
				return "Invalid password."; // Cancel process
			}
		}

		command.Parameters.AddWithValue("@username", username); // Only valid usernames get to this point

		var nId = command.ExecuteScalar();

		if (nId == null && !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(pass)) // If user does not exist OR the username or password are blank
		{
			query = "insert into prData values(@username, @password, 0, 'skin_default', 0, 0, 0, 0, 0, 0, 0, 0);"; // Insert default values + user and pass
			command = new MySqlCommand(query, connection);

			command.Parameters.AddWithValue("@username", username);
			command.Parameters.AddWithValue("@password", Encrypt(pass));

			command.ExecuteNonQuery(); // Execute Query, Create New Account
			connection.Close(); // Close connection
		}
		else
		{
			output = "Username already taken.";
		}
		if (string.IsNullOrWhiteSpace(username))
		{
			output = "Username is invalid.";
		}
		else if (string.IsNullOrWhiteSpace(password))
		{
			output = "Password is invalid.";
		}
		return output;
	}

}
