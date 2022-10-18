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

	//login screen
	[SerializeField] private InputField usernameField;
	[SerializeField] private InputField passwordField;
	[SerializeField] private InputField usernameCreateField;
	[SerializeField] private InputField passwordCreateField;
	[SerializeField] private InputField passwordConfirmField;

	//connect screen
	[SerializeField] private GameObject connectUI;
	[SerializeField] private InputField ipAddressField;
	[SerializeField] private InputField portField;

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

	public void SignInClicked()
	{

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
