using Riptide;
using Riptide.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClientToServerId : ushort
{
	name = 1,
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

	[SerializeField] private string ip;
	[SerializeField] private string port;

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

	public void Connect()
	{
		Client.Connect($"{ip}:{port}");
	}

	private void DidConnect(object sender, EventArgs e)
	{
		UIManager.Singleton.SendName();
	}

	private void FailedToConnect(object sender, EventArgs e)
	{
		UIManager.Singleton.BackToMain();
	}

	private void DidDisconnect(object sender, EventArgs e)
	{
		UIManager.Singleton.BackToMain();
	}

}
