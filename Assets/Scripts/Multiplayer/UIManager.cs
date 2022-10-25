using Riptide;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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

	[SerializeField] private TextMeshProUGUI messageBox;

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
		ipAddressField.interactable = false;
		portField.interactable = false;
		connectUI.SetActive(false);

		NetworkManager.Singleton.Connect(ipAddressField.text, portField.text);
	}

	public void SignInClicked()
	{
		/*string loginMessage = DatabaseManager.AttemptLogin(usernameField.text, passwordField.text);
		messageBox.text = loginMessage;

		if (loginMessage == "Login Successful!")*/
		Debug.Log("TBI \"CheckDatabase(string username, string pass)\" function");
			SceneManager.LoadScene("Main Menu");
	}

	public void CreateAccountClicked()
	{
		//if the password across the create and confirm fields doesn't match...
		if (passwordCreateField.text != passwordConfirmField.text)
		{
			messageBox.text = "Passwords don't match!";
			return;
		}

		string accCreateMessage = "Account Created!";
		//string accCreateMessage = DatabaseManager.AttemptCreate(usernameCreateField.text, passwordCreateField.text);
		Debug.Log("TBI Account Creation");
		messageBox.text = accCreateMessage;
	}

	public void BackToConnectMenu()
	{
		ipAddressField.interactable = true;
		portField.interactable = true;
		connectUI.SetActive(true);
	}

	public void BackButtonClicked()
	{
		BackToMain();
	}

	private void BackToMain()
	{
		SceneManager.LoadScene("Main Menu");
	}

	public void SendName()
	{
		Message message = Message.Create(MessageSendMode.Reliable, (ushort)ClientToServerId.name);
		//message.AddString(DatabaseManager.MyPlayer.username);
		//message.AddString(usernameField.text);
		message.AddString("Tom");
		Debug.Log("TBI Account Login");
		NetworkManager.Singleton.Client.Send(message);
	}



}
