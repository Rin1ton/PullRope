using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Riptide;
using UnityEditor;

public class GameLogic : MonoBehaviour
{
	private static GameLogic _singleton;

	public static bool gameCommenced { get; private set; } = false;
	public static GameLogic Singleton
	{
		get => _singleton;
		private set
		{
			if (_singleton == null) _singleton = value;
			else
			{
				Debug.LogWarning($"{nameof(GameLogic)} instance already exists, destroying duplicate!");
				Destroy(value);
			}
		}
	}

	public void ReadyUp()
	{
		References.thePlayer.GetComponent<Player>().IsReady = true;
		UIManager.Singleton.GameStatusText = "Waiting for other players";
	}

	public GameObject LocalPlayerPrefab => localPlayerPrefab;
	public GameObject PlayerPrefab => playerPrefab;
	public List<SpawnPoint> SpawnPoints = new List<SpawnPoint>();

	[Header("Prefabs")]
	[SerializeField] private GameObject localPlayerPrefab;
	[SerializeField] private GameObject playerPrefab;

	private void Awake()
	{
		Singleton = this;

		UIManager.Singleton.GameStatusText = "F3 to ready up";
	}


	[MessageHandler((ushort)ServerToClientId.gameState)]
	private static void GameState(Message message)
	{
		float gameState = message.GetFloat();
		if (gameState <= 0)
		{
			gameCommenced = false;
			References.thePlayer.GetComponent<Player>().IsReady = false;
			UIManager.Singleton.GameStatusText = "Game Over! F3 to play again";
		}
		else
		{
			if (!gameCommenced)
			{
				gameCommenced = true;
				References.thePlayer.GetComponent<Player>().Score = 0;
				//PUT GAME START SOUND HERE
				//PUT GAME START SOUND HERE
				//PUT GAME START SOUND HERE
				//PUT GAME START SOUND HERE
				//PUT GAME START SOUND HERE
				//PUT GAME START SOUND HERE
				//PUT GAME START SOUND HERE
			}
			UIManager.UpdateTimer(gameState);
		}

	}
}