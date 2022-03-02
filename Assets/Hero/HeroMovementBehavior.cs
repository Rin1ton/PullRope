using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovementBehavior : MonoBehaviour
{
	//movement stats
	readonly float friction = 7.5f;
	readonly float topSpeed = 10;
	readonly float airAcceleration = 1.22f;
	readonly float groundAcceleration = 12;
	readonly KeyCode forwardButton = KeyCode.W;
	readonly KeyCode backwardButton = KeyCode.S;
	readonly KeyCode leftButton = KeyCode.A;
	readonly KeyCode rightButton = KeyCode.D;
	readonly float maxGroundAngle = 45;
	Vector3 moveInput = new Vector3();

	//mouselook stats
	readonly float maxVerticalRotaion = 90;
	public GameObject myCamera;
	Quaternion myRBOriginalRotation;
	Quaternion myCameraOriginalRotation;
	float rotationX = 0;
	float rotationY = 0;
	readonly float xSens = 1;
	readonly float ySens = 1;
	readonly bool invertY = false;

	//
	Rigidbody myRB;

	// Start is called before the first frame update
	void Start()
	{
		//get a reference to my rigidbody before the first frame
		myRB = GetComponent<Rigidbody>();

		//lock the mouse and make it disappear
		UnityEngine.Cursor.lockState = CursorLockMode.Locked;
		UnityEngine.Cursor.visible = false;

		//get my original rotation
		myRBOriginalRotation = myRB.transform.rotation;
		myCameraOriginalRotation = myCamera.transform.rotation;
	}

	// Update is called once per frame
	void Update()
	{
		MouseLook();
		Jump();
	}

	private void FixedUpdate()
	{
		MovementInput();
	}

	void MouseLook()
	{
		//read the mouse inputs
		rotationX += Input.GetAxisRaw("Mouse X") * xSens;
		if (!invertY)
			rotationY += Input.GetAxisRaw("Mouse Y") * ySens;
		else
			rotationY -= Input.GetAxisRaw("Mouse Y") * ySens;
   

		//clamp rotation and apply floats to quaternions
		rotationY = Mathf.Clamp(rotationY, -maxVerticalRotaion, maxVerticalRotaion);
		Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
		Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);

		//rotate player and camera
		myRB.transform.localRotation = myRBOriginalRotation * xQuaternion;
		myCamera.transform.localRotation = myCameraOriginalRotation * yQuaternion;
	}

	void MovementInput()
	{
		//keyboard inputs
		float xMovement = 0;
		float zMovement = 0;
		Vector3 unityMoveInput = Vector3.zero;

		//set our floats to the inputs of the player
		if (Input.GetKey(forwardButton))
			zMovement++;
		if (Input.GetKey(backwardButton))
			zMovement--;
		if (Input.GetKey(leftButton))
			xMovement--;
		if (Input.GetKey(rightButton))
			xMovement++;

		//put a value in our input vector
		moveInput = (transform.right * xMovement + transform.forward * zMovement).normalized;

		//call our friction function
		MoveOnGround(myRB.velocity, moveInput);
	}

	void MoveOnGround(Vector3 prevVelocity, Vector3 moveDir)
	{
		float speed = prevVelocity.magnitude;
		//how much to "drop" speed this update
		float drop = 0;

		if (speed != 0)
		{
			float control = speed;

			drop += control * friction * Time.fixedDeltaTime;

			//scale the velocity
			float newSpeed = speed - drop < 0 ? 0 : speed - drop;
			newSpeed = speed != 0 ? newSpeed / speed : 0;

			prevVelocity *= newSpeed;
		}

		//call accelerate
		Accelerate(prevVelocity, groundAcceleration, moveDir);
	}

	void Accelerate(Vector3 prevVelocity, float accel, Vector3 wishDir)   //Kasokudo, 加速度
	{
		float wishSpeed, addSpeed, accelSpeed;

		wishSpeed = topSpeed;

		//this is straight witchcraft. determine how much to accelerate
		float currentSpeed = Vector3.Dot(prevVelocity, wishDir); // Vector projection of Current velocity onto wishDir

		//the difference between our top speed and our current speed is what we'll be adding to our speed.
		addSpeed = wishSpeed - currentSpeed;

		//if we're going faster than our top speed, allow holding [W]
		if (addSpeed <= 0)
			addSpeed = 0;

		//bungus
		//apply our acceleration to a float
		accelSpeed = accel * Time.deltaTime * wishSpeed;

		//drop acceleration to top speed?
		if (accelSpeed > addSpeed)
			accelSpeed = addSpeed;

		//save what our new velocity should be
		Vector3 newVelocity = prevVelocity + wishDir * accelSpeed;

		//now apply our witchcraft to my velocity to accelerate
		myRB.velocity = newVelocity;
	}

	void Jump()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			myRB.AddForce(Vector3.up * 10, ForceMode.VelocityChange);
		}
	}

}
