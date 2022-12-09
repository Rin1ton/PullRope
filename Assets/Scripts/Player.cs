using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.TeleTrust;
using Riptide;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
	public static Dictionary<ushort, Player> list = new Dictionary<ushort, Player>();
	[NonSerialized] public Rigidbody myRB;
	[SerializeField] TextMeshPro myBillboard;

	public ushort Id { get; private set; }
	
	//is this the local player?
	public bool IsLocal { get; private set; }

	public static int playerThatKilledMeID;

	public string username { get; private set; }
	private string mySkin;

	private int _score = 0;
	public int Score {
		get => _score;
		set
		{
			_score = value;
			UIManager.UpdateScoreboard();
		}
	}

	[SerializeField] private Interpolator interpolator;

	private void Awake()
	{
		myRB = GetComponent<Rigidbody>();
	}

	private void OnDestroy()
	{
		list.Remove(Id);
	}

	private void Update()
	{
		//respawn player
		if (Vector3.Distance(transform.position, Vector3.zero) >= 80 || transform.position.y <= -45) RespawnPlayer();
	}

	public static void Spawn(ushort id, string username, Vector3 position, string mySkinName)
	{
		Player player;
		if (id == NetworkManager.Singleton.Client.Id)
		{
			//spawn local player
			player = Instantiate(GameLogic.Singleton.LocalPlayerPrefab, GameLogic.Singleton.SpawnPoints[UnityEngine.Random.Range(0, GameLogic.Singleton.SpawnPoints.Count - 1)].transform.position, Quaternion.identity).GetComponent<Player>();
			References.thePlayer = player.gameObject;
			player.transform.GetChild(0).GetComponent<MeshRenderer>().material = References.currentSkin;
			player.IsLocal = true;
		}
		else
		{
			//spawn remote players
			player = Instantiate(GameLogic.Singleton.PlayerPrefab, new Vector3(0, -40, 0), Quaternion.identity).GetComponent<Player>();
			if (SkinLoader.SkinNameToMaterial(mySkinName) != null)
			{
				player.transform.GetChild(0).GetComponent<MeshRenderer>().material = SkinLoader.SkinNameToMaterial(mySkinName);
			}
			player.myBillboard.text = username;
			player.myRB = player.transform.GetChild(0).GetComponent<Rigidbody>();
			player.IsLocal = false;
		}

		player.name = $"Player {id} ({(string.IsNullOrEmpty(username) ? "Guest" : username)})";
		player.Id = id;
		player.username = username;
		player.mySkin = mySkinName;

		list.Add(id, player);
		UIManager.UpdateScoreboard();
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
			//				Server tick			Camera Forward		transform.position		transform.rotation			velocity
			player.Move(message.GetUShort(), message.GetVector3(), message.GetVector3(), message.GetQuaternion(), message.GetVector3());
	}

	private void Move(ushort tick, Vector3 camera, Vector3 position, Quaternion rotation, Vector3 velocity)
	{
		transform.position = position;
		//interpolator.NewUpdate(tick, position);
		transform.rotation = rotation;
		myRB.velocity = velocity;
	}

	private void RespawnPlayer()
	{
		myRB.velocity = Vector3.zero;
		transform.position = GameLogic.Singleton.SpawnPoints[UnityEngine.Random.Range(0, GameLogic.Singleton.SpawnPoints.Count - 1)].transform.position;
		Message message = Message.Create(MessageSendMode.Reliable, ClientToServerId.playerRespawned);

		message.AddInt(playerThatKilledMeID);

		NetworkManager.Singleton.Client.Send(message);

		playerThatKilledMeID = -1;
	}

}