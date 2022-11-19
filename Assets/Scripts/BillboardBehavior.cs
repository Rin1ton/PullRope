using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardBehavior : MonoBehaviour
{

	// Update is called once per frame
	void Update()
	{
		if (References.thePlayer != null)
		{
			transform.LookAt(References.thePlayer.transform);
		}
	}
}
