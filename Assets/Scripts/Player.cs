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

	private void OnDestroy()
	{
		list.Remove(Id);
	}

	public static void Spawn(ushort id, string username, Vector3 position)
	{
		Player player;
		if (id == NetworkManager.Singleton.Client.Id)
		{
			//spawn local player
			player = Instantiate(GameLogic.Singleton.LocalPlayerPrefab, position, Quaternion.identity).GetComponent<Player>();
			player.IsLocal = true;
		}
		else
		{
			//spawn remote players
			player = Instantiate(GameLogic.Singleton.PlayerPrefab, position, Quaternion.identity).GetComponent<Player>();
			player.IsLocal = false;
		}

		player.name = $"Player {id} ({(string.IsNullOrEmpty(username) ? "Guest" : username)})";
		player.Id = id;
		player.username = username;

		list.Add(id, player);
	}

	[MessageHandler((ushort)ServerToClientId.playerSpawned)]
	private static void SpawnPlayer(Message message)
	{
		Spawn(message.GetUShort(), message.GetString(), message.GetVector3());
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

}