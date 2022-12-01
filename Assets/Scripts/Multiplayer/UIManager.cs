using Riptide;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using static References;

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

	public static bool isPaused { get; private set; } = false;
	private KeyCode PauseButton = KeyCode.Escape;

	[SerializeField] private TextMeshProUGUI messageBox;
	[Space(2)]

	//login screen
	[Header("Login Screen")]
	[SerializeField] private InputField usernameField;
	[SerializeField] private InputField passwordField;
	[SerializeField] private InputField usernameCreateField;
	[SerializeField] private InputField passwordCreateField;
	[SerializeField] private InputField passwordConfirmField;
	[Space(2)]

	//connect screen
	[Header("Connect Screen")]
	[SerializeField] private GameObject connectUI;
	[SerializeField] private GameObject inGameHUD;
	[SerializeField] private InputField ipAddressField;
	[SerializeField] private InputField portField;
	[SerializeField] private TextMeshProUGUI myUsernameHUD;
	[Space(2)]

	//options
	[Header("Options")]
	[SerializeField] private Slider sensSlider;
	[SerializeField] private InputField sensInputBox;

	// UIManager is in the very first menu of the very first scene when the game is started.
	// if something needs to be initialized from the beginning, it should be done from this
	// awake function.
	private void Awake()
	{
		Singleton = this;

		SkinLoader.defaultSkin = Resources.Load("skin_default") as Material;
		SkinLoader.skin1 = Resources.Load("skin_dirt") as Material;
		SkinLoader.skin2 = Resources.Load("skin_copper") as Material;
		SkinLoader.skin3 = Resources.Load("skin_gold") as Material;
		SkinLoader.skin4 = Resources.Load("skin_sapphire") as Material;
		SkinLoader.skin5 = Resources.Load("skin_purple") as Material;
		SkinLoader.skin6 = Resources.Load("skin_grass") as Material;
		SkinLoader.skin7 = Resources.Load("skin_matrix") as Material;
		SkinLoader.skin8 = Resources.Load("skin_sus") as Material;

		if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Login Screen"))
		{
			currentSkinName = "skin_default";
			currentSkin = SkinLoader.SkinNameToMaterial("skin_default");
		}

		if (inGameHUD != null)
			inGameHUD.SetActive(false);
	}

	private void Update()
	{
		if (Input.GetKeyDown(PauseButton))
			Pause();
	}

	private void Pause()
	{
		//pause the game
		if (!isPaused)
		{
			isPaused = true;
			connectUI.SetActive(true);
			inGameHUD.SetActive(false);
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		} 
		//un-pause the game
		else
		{
			isPaused = false;
			connectUI.SetActive(false);
			inGameHUD.SetActive(true);
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

	public void SensSliderMoved()
	{
		sensInputBox.text = sensSlider.value.ToString();
		playerSensitivityX = sensSlider.value;
		playerSensitivityY = sensSlider.value;
	}

	public void SensInputBoxChanged()
	{
		sensSlider.value = float.Parse(sensInputBox.text);
		playerSensitivityX = sensSlider.value;
		playerSensitivityY = sensSlider.value;
	}

	public void ConnectClicked()
	{
		//ipAddressField.interactable = false;
		//portField.interactable = false;
		connectUI.SetActive(false);
		inGameHUD.SetActive(true);
		myUsernameHUD.enabled = true;
		myUsernameHUD.text = DatabaseManager.MyPlayer.username;

		NetworkManager.Singleton.Connect(ipAddressField.text, portField.text);
	}

	public void SignInClicked()
	{
		// this line will pull the data for the user from the database, if it exists,
		// and put it into the DatabaseManager.MyPlayer variable
		string loginMessage = DatabaseManager.AttemptLogin(usernameField.text, passwordField.text);
		messageBox.text = loginMessage; 

		if (loginMessage == "Login Successful!" || loginMessage == "Logging in as Guest!")
		{
			//set our skin
			currentSkinName = DatabaseManager.MyPlayer.equipped;
			currentSkin = SkinLoader.SkinNameToMaterial(DatabaseManager.MyPlayer.equipped);

			SceneManager.LoadScene("Main Menu");
		}

	}

	public void CreateAccountClicked()
	{
		//if the password across the create and confirm fields doesn't match...
		if (passwordCreateField.text != passwordConfirmField.text)
		{
			messageBox.text = "Passwords don't match!";
			return;
		}

		//string accCreateMessage = "Account Created!";
		string accCreateMessage = DatabaseManager.AttemptCreate(usernameCreateField.text, passwordCreateField.text);
		messageBox.text = accCreateMessage;
	}

	public void BackToConnectMenu()
	{
		ipAddressField.interactable = true;
		portField.interactable = true;
		connectUI.SetActive(true);
		inGameHUD.SetActive(false);

		//unlock the mouse and make it disappear
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void BackButtonClicked()
	{
		BackToMain();
	}

	private void BackToMain()
	{
		LevelOneUIScript.KillMe();
		SceneManager.LoadScene("Main Menu");
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	private static localPlayerData _myPlayer;


	public void SendName()
	{
		Message message = Message.Create(MessageSendMode.Reliable, ClientToServerId.name);
		message.AddString(DatabaseManager.MyPlayer.username);
		message.AddString(currentSkinName);
		NetworkManager.Singleton.Client.Send(message);
	}

	public void GuestAccountClicked()
	{
		string loginMessage = DatabaseManager.AttemptLogin("guest", passwordField.text);
		messageBox.text = loginMessage;

		if (loginMessage == "Logging in as Guest!")
		{
			//set our skin
			currentSkinName = DatabaseManager.MyPlayer.equipped;
			currentSkin = SkinLoader.SkinNameToMaterial(DatabaseManager.MyPlayer.equipped);

			SceneManager.LoadScene("Main Menu");
		}
	}

}
