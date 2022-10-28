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

	private static localPlayerData _myPlayer;
	public static localPlayerData MyPlayer
	{
		get
		{
            UpdateUser(_myPlayer.username);
			return _myPlayer;
		}
		set
		{
			_myPlayer = value;
            UpdateDatabase(_myPlayer);
		}
	}

	public static string AttemptLogin(string username, string password) // Try to log into server using a username and password. User object will be updated with player data from server
	{
		if (CheckDatabase(username, password)) // If username and password are correct
		{
            UpdateUser(_myPlayer.username); // Set the myPlayer object to the data from the database
            return "Login Successful!";
		}
		else
		{
			return "Incorrect Username and/or Password!";
		}

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
            newPlayer.password = reader["password"].ToString();
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
            if (pass == reader["password"].ToString()) // If password is correct
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

        command.Parameters.AddWithValue("@username", username);

        var nId = command.ExecuteScalar();

        if (nId == null) // If user does not exist
        {
            query = "insert into prData values(@username, @password, 0, 'skin_default', 0, 0, 0, 0, 0, 0, 0, 0);"; // Insert default values + user and pass
            command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", pass);

            command.ExecuteNonQuery(); // Execute Query, Create New Account
            connection.Close(); // Close connection
        }
        else
        {
            output = "Username already taken.";
        }
        return output;
    }
}
