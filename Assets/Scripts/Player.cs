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

	[SerializeField] private Interpolator interpolator;

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
			if (SkinLoader.SkinNameToMaterial(mySkinName) != null)
				player.transform.GetChild(0).GetComponent<MeshRenderer>().material = SkinLoader.SkinNameToMaterial(mySkinName);
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
		//		Player ID			 Player Username	 Transform.position     playerSkinName
		Spawn(message.GetUShort(), message.GetString(), message.GetVector3(), message.GetString());
	}

	[MessageHandler((ushort)ServerToClientId.playerTransform)]
	private static void SetTransform(Message message)
	{
		//						Player ID
		if (list.TryGetValue(message.GetUShort(), out Player player) && !player.IsLocal)
			//				Server tick			Camera Forward		transform.position		transform.rotation
			player.Move(message.GetUShort(), message.GetVector3(), message.GetVector3(), message.GetQuaternion());
	}

	private void Move(ushort tick, Vector3 camera, Vector3 position, Quaternion rotation)
	{
		//transform.position = position;

		interpolator.NewUpdate(tick, position);
		transform.rotation = rotation;
	}

}