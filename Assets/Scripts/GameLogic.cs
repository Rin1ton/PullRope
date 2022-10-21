using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
	private static GameLogic _singleton;
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

	public GameObject LocalPlayerPrefab => localPlayerPrefab;
	public GameObject PlayerPrefab => PlayerPrefab;

	[Header("Prefabs")]
	[SerializeField] private GameObject localPlayerPrefab;
	[SerializeField] private GameObject playerPrefab;

	private void Awake()
	{
		Singleton = this;
	}

}
