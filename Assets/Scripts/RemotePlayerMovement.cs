using Riptide;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RemotePlayerMovement : MonoBehaviour
{
	[SerializeField] private Player player;
	//GameObject me = gameObject;

	[MessageHandler((ushort)ServerToClientId.playerTransform)]
	private static void SetTransform(Message message)
	{
		if (Player.list.TryGetValue(message.GetUShort(), out Player player))
			Move(message.GetVector3(), message.GetVector3(), message.GetQuaternion());
	}

	private static void Move(Vector3 camera, Vector3 position, Quaternion rotation)
	{
		//.rotation = rotation;
	}

}