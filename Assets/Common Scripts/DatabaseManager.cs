using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using MySql.Data.MySqlClient;

public static class DatabaseManager
{
    public static References.localPlayerData CreatePlayer(string username)
    {
        References.localPlayerData newPlayer = new References.localPlayerData();

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
