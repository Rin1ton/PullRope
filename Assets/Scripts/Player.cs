using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public static Dictionary<ushort, Player> list = new Dictionary<ushort, Player>();

	public ushort Id { get; private set; }
	
	//is this the local player?
	public bool IsLocal { get; private set; }

	private string username;

	private void OnDestroy()
	{
		list.Remove(Id);
	}

	public static void Spawn(ushort id, string username, Vector3 vector3)
	{

	}

}
