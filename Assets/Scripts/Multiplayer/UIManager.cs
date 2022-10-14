using Riptide;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	private static UIManager _singleton;
	public static UIManager Singleton
	{
		get => _singleton;
		private set
		{
			if (_singleton == null) _singleton = value;
			else
			{
				Debug.LogWarning($"{nameof(UIManager)} instance already exists, destroying duplicate!");
				Destroy(value);
			}
		}
	}

	[SerializeField] private GameObject connectUI;
	[SerializeField] private InputField usernameField;

	private void Awake()
	{
		Singleton = this;
	}

	public void ConnectClicked()
	{
		usernameField.interactable = false;
		connectUI.SetActive(false);

		NetworkManager.Singleton.Connect();
	}

	public void BackToMain()
	{
		usernameField.interactable = true;
		connectUI.SetActive(true);
	}

	public void SendName()
	{
		Message message = Message.Create(MessageSendMode.Reliable, (ushort)ClientToServerId.name);
		message.AddString(usernameField.text);
		NetworkManager.Singleton.Client.Send(message);
	}



}
