using Riptide;
using Riptide.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum ServerToClientId : ushort
{
	playerSpawned = 1,
	playerTransform,
	sync,
	playerBooped,
}

public enum ClientToServerId : ushort
{
	name = 1,
	playerTransform,
	playerBooped,
}

public class NetworkManager : MonoBehaviour
{
	private static NetworkManager _singleton;
	public static NetworkManager Singleton
	{
		get => _singleton;
		private set
		{
			if (_singleton == null) _singleton = value;
			else
			{
				Debug.LogWarning($"{nameof(NetworkManager)} instance already exists, destroying duplicate!");
				Destroy(value);
			}
		}
	}

	public static bool isConnected { get; private set; } = false;
	
	public Client Client { get; private set; }

	private ushort _serverTick;
	public ushort ServerTick
	{
		get => _serverTick;
		private set 
		{ 
			_serverTick = value;
			InterpolationTick = (ushort)(value - TicksBetweenPositionUpdates);
		}
	}

	public ushort InterpolationTick { get; private set; }
	private ushort _ticksBetweenPositionUpdates = 2;
	public ushort TicksBetweenPositionUpdates
	{
		get => _ticksBetweenPositionUpdates;
		private set
		{
			_ticksBetweenPositionUpdates = value;
			InterpolationTick = (ushort)(ServerTick - value);
		}
	}

	private string ip;
	private string port;
	[SerializeField] private ushort tickDivergenceTolerance = 1;

	private void Awake()
	{
		Singleton = this;
	}

	private void Start()
	{
		RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

		Client = new Client();
		Client.Connected += DidConnect;
		Client.ConnectionFailed += FailedToConnect;
		Client.ClientDisconnected += PlayerLeft;
		Client.Disconnected += DidDisconnect;

		ServerTick = 2;
	}

	private void FixedUpdate()
	{
		Client.Update();

		ServerTick++;
	}

	private void OnApplicationQuit()
	{
		Client.Disconnect();
	}

	public void Connect(string thisIp, string thisPort)
	{
		if (thisPort == "")	thisPort = "7777";

		if (thisIp == "") thisIp = "127.0.0.1";

		Client.Connect($"{thisIp}:{thisPort}");
		ip = thisIp;
		port = thisPort;
	}

	private void DidConnect(object sender, EventArgs e)
	{
		UIManager.Singleton.SendName();
		isConnected = true;
	}

	private void FailedToConnect(object sender, EventArgs e)
	{
		UIManager.Singleton.BackToConnectMenu();
	}

	private void PlayerLeft(object sender, ClientDisconnectedEventArgs e)
	{
		if (Player.list.TryGetValue(e.Id, out Player player))
		{
			Destroy(player.gameObject);
		}
	}

	private void DidDisconnect(object sender, EventArgs e)
	{
		UIManager.Singleton.BackToConnectMenu();
		foreach (Player player in Player.list.Values)
			Destroy(player.gameObject);
		isConnected = false;
	}

	private void SetTick(ushort serverTick)
	{
		if (Mathf.Abs(ServerTick - serverTick) > tickDivergenceTolerance)
		{
			Debug.Log($"Client tick: {ServerTick} -> {serverTick}");
			ServerTick = serverTick;
		}
	}

	[MessageHandler((ushort)ServerToClientId.sync)]
	public static void Sync(Message message)
	{
		Singleton.SetTick(message.GetUShort());
	}

}
