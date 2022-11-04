using Riptide;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public static Dictionary<ushort, Player> list = new Dictionary<ushort, Player>();

	public ushort Id { get; private set; }
	
	//is this the local player?
	public bool IsLocal { get; private set; }

	private string username;
	private string mySkin;

	private void OnDestroy()
	{
		list.Remove(Id);
	}

	public static void Spawn(ushort id, string username, Vector3 position, string mySkinName)
	{
		Player player;
		if (id == NetworkManager.Singleton.Client.Id)
		{
			//spawn local player
			player = Instantiate(GameLogic.Singleton.LocalPlayerPrefab, position, Quaternion.identity).GetComponent<Player>();
			player.transform.GetChild(0).GetComponent<MeshRenderer>().material = References.currentSkin;
			player.IsLocal = true;
		}
		else
		{
			//spawn remote players
			player = Instantiate(GameLogic.Singleton.PlayerPrefab, position, Quaternion.identity).GetComponent<Player>();
			if (SetRemotePlayerSkin(mySkinName) != null)
				player.transform.GetChild(0).GetComponent<MeshRenderer>().material = SetRemotePlayerSkin(mySkinName);
			player.IsLocal = false;
		}

		player.name = $"Player {id} ({(string.IsNullOrEmpty(username) ? "Guest" : username)})";
		player.Id = id;
		player.username = username;
		player.mySkin = mySkinName;

		list.Add(id, player);
	}

	[MessageHandler((ushort)ServerToClientId.playerSpawned)]
	private static void SpawnPlayer(Message message)
	{
		Spawn(message.GetUShort(), message.GetString(), message.GetVector3(), message.GetString());
	}

	[MessageHandler((ushort)ServerToClientId.playerTransform)]
	private static void SetTransform(Message message)
	{
		if (list.TryGetValue(message.GetUShort(), out Player player) && !player.IsLocal)
			player.Move(message.GetVector3(), message.GetVector3(), message.GetQuaternion());
	}

	private void Move(Vector3 camera, Vector3 position, Quaternion rotation)
	{
		transform.position = position;
		transform.rotation = rotation;
	}

	private static Material SetRemotePlayerSkin(string skinName)
	{
		switch (skinName)
		{
			case "skin_dirt":
				return SkinLoader.skin1;
			case "skin_copper":
				return SkinLoader.skin2;
			case "skin_gold":
				return SkinLoader.skin3;
			case "skin_sapphire":
				return SkinLoader.skin4;
			case "skin_purple":
				return SkinLoader.skin5;
			case "skin_grass":
				return SkinLoader.skin6;
			case "skin_matrix":
				return SkinLoader.skin7;
			case "skin_sus":
				return SkinLoader.skin8;
			default:
				return References.currentSkin;
		}
	}
}