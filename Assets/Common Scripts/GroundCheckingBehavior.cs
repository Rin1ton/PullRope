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
	Rigidbody myRB;
	Vector3 currentGround = Vector3.zero;
	float currentGroundAngle = 0;

	private void Awake()
	{
		myRB = GetComponent<Rigidbody>();
	}

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

	private void FixedUpdate()
	{
		CounterSlope();

		//reset our ground
		currentGround = Vector3.zero;
		currentGroundAngle = 0;
	}

	void CounterSlope()
	{
		if (currentGroundAngle != 0 && currentGround != Vector3.zero)
		{
			//set the direction of the vector to counter the force of gravity
			Vector3 counterForce = Vector3.ProjectOnPlane(Vector3.up, currentGround).normalized;

			//set the magnitude of the vector based on the angle of the ground
			counterForce *= Mathf.Sin(Mathf.Deg2Rad * currentGroundAngle) * Physics.gravity.magnitude * myRB.mass * 0.5f;
			myRB.AddForce(counterForce);
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		GroundAndWallNormal(collision);
	}

	private void OnCollisionExit(Collision collision)
	{
		GroundAndWallNormal(collision);
	}
	void GroundAndWallNormal(Collision other)
	{
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
