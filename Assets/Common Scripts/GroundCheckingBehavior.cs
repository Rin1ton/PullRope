using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckingBehavior : MonoBehaviour
{
	//public members
	public Vector3 Ground => currentGround;
	public bool IsGrounded => currentGround != Vector3.zero;
	public float GroundAngle => currentGroundAngle;

	//local vars
	readonly float maxGroundAngle = 46;
	Vector3 currentGround = Vector3.zero;
	float currentGroundAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void LateUpdate()
	{
		
	}

	private void OnCollisionEnter(Collision collision)
	{
		GroundAndWallNormal(collision);
	}

	private void OnCollisionExit(Collision collision)
	{
		GroundAndWallNormal(collision);
	}
	void GroundAndWallNormal(Collision other)
	{
		//start clean
		currentGround = Vector3.zero;
		currentGroundAngle = 0;

		//see if any of the contacts of this collision are shallow enough to be the ground
		float lowestNormalAngle = 180;
		Vector3 lowestNormal = Vector3.zero;
		for (int k = 0; k < other.contactCount; k++)
		{
			Vector3 normal = other.contacts[k].normal;
			if (Vector3.Angle(Vector3.up, normal) < lowestNormalAngle)
			{
				lowestNormalAngle = Vector3.Angle(Vector3.up, normal);
				lowestNormal = normal;
			}
		}

		//if any part of this collision is shallow enough to be the ground, then this is the collision with the ground
		if (lowestNormalAngle <= maxGroundAngle)
		{
			currentGroundAngle = lowestNormalAngle;
			currentGround = lowestNormal;
		}

	}

}
