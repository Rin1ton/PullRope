using Riptide;
using Riptide.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ServerToClientId : ushort
{
	playerSpawned = 1,
	playerTransform, 
}

public enum ClientToServerId : ushort
{
	name = 1,
	playerTransform, 
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
	
	public Client Client { get; private set; }

	private string ip;
	private string port;

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
	}

	private void FixedUpdate()
	{
		Client.Update();
	}

	private void OnApplicationQuit()
	{
		Client.Disconnect();
	}

	public void Connect(string thisIp, string thisPort)
	{
		if (thisPort == "") {
			thisPort = "7777";
			port = "7777";
		}

		if (thisIp == "")
		{
			thisIp = "127.0.0.1";
			ip = "127.0.0.1";
		}

		if (this)

		Client.Connect($"{thisIp}:{thisPort}");
		ip = thisIp;
		port = thisPort;
	}

	private void DidConnect(object sender, EventArgs e)
	{
		UIManager.Singleton.SendName();
	}

	private void FailedToConnect(object sender, EventArgs e)
	{
		UIManager.Singleton.BackToConnectMenu();
	}

	private void PlayerLeft(object sender, ClientDisconnectedEventArgs e)
	{
		if (Player.list.TryGetValue(e.Id, out Player player))
			Destroy(player.gameObject);
	}

	private void DidDisconnect(object sender, EventArgs e)
	{
		UIManager.Singleton.BackToConnectMenu();
		foreach (Player player in Player.list.Values)
			Destroy(player.gameObject);
	}

}
