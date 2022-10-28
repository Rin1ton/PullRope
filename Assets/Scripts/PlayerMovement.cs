﻿using Riptide;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	[SerializeField] private Player player;

	readonly float shortTimerStop = 120;

	//controls
	readonly KeyCode forwardButton = KeyCode.W;
	readonly KeyCode backwardButton = KeyCode.S;
	readonly KeyCode leftButton = KeyCode.A;
	readonly KeyCode rightButton = KeyCode.D;
	readonly KeyCode grappleButton = KeyCode.Mouse0;
	readonly KeyCode boopButton = KeyCode.Mouse1;

	//mouselook stats
	/*
	 * NOTE: player sensitivity stored in references
	 */
	readonly float maxVerticalRotaion = 90;
	[SerializeField] GameObject myCamera;
	Quaternion myRBOriginalRotation;
	Quaternion myCameraOriginalRotation;
	float rotationX = 0;
	float rotationY = 0;
	readonly bool invertY = false;

	//movement stats
	readonly float friction = 7.5f;
	readonly float topSpeed = 10;
	readonly float airAcceleration = 1.45f;
	readonly float groundAcceleration = 12;
	Vector3 moveInput = new Vector3();

	//jump Stats
	float jumpVelocity = 20;
	float timeSinceLastJump = 60;
	float timeSinceBecameGrounded = 60;
	bool wasGrounded = false;

	//grapple
	readonly float maxGrappleDistance = 40;
	Color32 grappleableColor;
	Color32 ungrappleableColor;
	Vector3 grapplePoint;
	float grappleLength;
	bool grappled = false;
	float grappleWinchRate = 12.5f;
	float minGrappleDistance = 2;
	Vector3 additionalVelocity;
	Vector3 myPositionLastFixedFrame;

	//grapple object
	readonly float grappleObjectMoveTimescale = 10;
	[SerializeField] GameObject myGrappleHookPrefab;
	float grappleObjectLerpTime = 0;
	Vector3 grappleStartPos;
	GameObject myGrappleHookObject;
	LineRenderer grappleObjectLR;

	//physics stuff
	readonly int playerPhysicsIndex = 3;
	Rigidbody myRB;
	GroundCheckingBehavior myGroundChecker;

	//RipTide Stuff

	private void OnValidate()
	{
		if (player == null)
			player = GetComponent<Player>();
	}

	private void Awake()
	{
		//tell everyone we're the player
		References.thePlayer = gameObject;

		//get a reference to my rigidbody before the first frame
		myRB = GetComponent<Rigidbody>();

		//get a reference to my ground checker
		myGroundChecker = GetComponent<GroundCheckingBehavior>();

		//set our grappleable and ungrappleable colors (green and white respectively)
		grappleableColor = new Color32(0, 255, 0, 255);
		ungrappleableColor = new Color32(255, 255, 255, 255);
	}

	// Start is called before the first frame update
	void Start()
	{
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
		Timers();
		MouseLook();
		Jump();
		MovementInput();
		Grapple();
		CheckIfCanGrapple();
	}

	private void FixedUpdate()
	{
		ApplyGrapplePhysics();
		SendPosition();
	}






	void Timers()
	{
		if (timeSinceLastJump < shortTimerStop)
			timeSinceLastJump += Time.deltaTime;

		if (!wasGrounded && myGroundChecker.IsGrounded)
		{
			timeSinceBecameGrounded = 0;
			wasGrounded = true;
		}
		if (wasGrounded && !myGroundChecker.IsGrounded)
			wasGrounded = false;

		if (timeSinceBecameGrounded < shortTimerStop && myGroundChecker.IsGrounded)
			timeSinceBecameGrounded += Time.deltaTime;
	}

	void MouseLook()
	{
		//read the mouse inputs
		rotationX += Input.GetAxisRaw("Mouse X") * References.playerSensitivityX;
		if (!invertY)
			rotationY += Input.GetAxisRaw("Mouse Y") * References.playerSensitivityY;
		else
			rotationY -= Input.GetAxisRaw("Mouse Y") * References.playerSensitivityY;

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

		//correct our moveInput with our original facing direction
		Vector3 correctedMoveInput;
		correctedMoveInput = myRBOriginalRotation * moveInput;

		//call our friction function if we're on the ground
		if (myGroundChecker.IsGrounded && timeSinceBecameGrounded > Time.deltaTime)
			MoveOnGround(myRB.velocity, correctedMoveInput);
		else
			Accelerate(myRB.velocity, airAcceleration, correctedMoveInput);
	}

	void MoveOnGround(Vector3 prevVelocity, Vector3 moveDir)
	{
		float speed = prevVelocity.magnitude;
		//how much to "drop" speed this update
		float drop = 0;

		if (speed != 0)
		{
			float control = speed;

			drop += control * friction * Time.deltaTime;

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

		//before we move, compensate for starting looking another direction
		//newVelocity = myRBOriginalRotation * newVelocity;

		//now apply our witchcraft to my velocity to accelerate
		myRB.velocity = newVelocity;
	}

	void Grapple()
	{
		if (Input.GetKeyDown(grappleButton))
		{
			//create a layer mask that includes just the "player" physics layer
			int layerMask = 1 << playerPhysicsIndex;

			//invert that layer mask;
			layerMask = ~layerMask;

			/* when I grapple I have to:
			 * 1. get a reference to the object I'm grappling
			 * 2. get check if that thing has rigidbody
			 * 3. if it does, save those parameters we're about to change about it
			 * 4. add the "configurable joint" component to it, automatically adding a rigidbody as well
			 * 5. get a reference to it again, in case it didn't exist a moment ago
			 * 6. configure that RB
			 */

			RaycastHit hit;
			// Does the ray intersect any objects excluding the player layer
			//NOTE: this block runs once when the grapple starts
			if (Physics.Raycast(myCamera.transform.position, myCamera.transform.TransformDirection(Vector3.forward), out hit, maxGrappleDistance, layerMask))
			{

				grappled = true;
				grapplePoint = hit.point;
				grappleLength = (myCamera.transform.position - hit.point).magnitude;
				myPositionLastFixedFrame = myRB.position;

				grapplePoint = hit.point;

				//send out grapple hook gameobject
				myGrappleHookObject = Instantiate(myGrappleHookPrefab, myCamera.transform);
				myGrappleHookObject.transform.parent = null;
				grappleObjectLerpTime = 0;
				grappleStartPos = myCamera.transform.position;
				grappleObjectLR = myGrappleHookObject.GetComponent<LineRenderer>();
			}
		}

		if (Input.GetKeyUp(grappleButton))
		{
			Ungrapple();
		}
	}

	void Boop()
	{

	}

	void ApplyGrapplePhysics()
	{
		if (grappled)
		{
			Vector3 directionToGrapple = Vector3.Normalize(grapplePoint - transform.position);
			float currentDistanceToGrapple = Vector3.Distance(grapplePoint, transform.position);

			float speedTowardsGrapplePoint = Mathf.Round(Vector3.Dot(myRB.velocity, directionToGrapple) * 100) / 100;

			if (currentDistanceToGrapple > grappleLength)
			{
				myRB.velocity -= speedTowardsGrapplePoint * directionToGrapple;
				myRB.position = grapplePoint - directionToGrapple * grappleLength;
			}

			//speed is distance/time
			//soooooooooooooo
			//

			grappleLength -= grappleWinchRate * Time.fixedDeltaTime;

			if (grappleLength <= minGrappleDistance)
			{
				Ungrapple();
			}

			//apply our theoretical velocity to our actual velocity
			additionalVelocity = (myRB.position - myPositionLastFixedFrame) / Time.fixedDeltaTime;
			myPositionLastFixedFrame = myRB.position;

			//set the line positions
			grappleObjectLR.SetPosition(0, myGrappleHookObject.transform.position);
			grappleObjectLR.SetPosition(1, myCamera.transform.position + new Vector3(.5f, 0, -.5f));

			//if the grapple hasn't made it to the grapple point yet...
			if (grappleObjectLerpTime != 1)
			{
				//add to the grapple object lerp time
				grappleObjectLerpTime += grappleObjectMoveTimescale * Time.fixedDeltaTime;
				grappleObjectLerpTime = grappleObjectLerpTime > 1 ? 1 : grappleObjectLerpTime;

				//then apply that to the position
				myGrappleHookObject.transform.position = Vector3.Lerp(grappleStartPos, grapplePoint, grappleObjectLerpTime);
			}
		}
	}

	void CheckIfCanGrapple()
	{

		//create a layer mask that includes just the "player" physics layer
		int layerMask = 1 << playerPhysicsIndex;

		//invert that layer mask;
		layerMask = ~layerMask;

		//create a hit object
		RaycastHit hit;

		//IF this code is run, then we're able to grapple the object we're looking at
		if (Physics.Raycast(myCamera.transform.position,
							myCamera.transform.TransformDirection(Vector3.forward),
							out hit, maxGrappleDistance, layerMask))
		{
			References.CrosshairColor = grappleableColor;
		}
		else
		{
			References.CrosshairColor = ungrappleableColor;
		}
	}

	void Ungrapple()
	{
		if (grappled)
		{
			//grapple physics
			grappled = false;
			grappleLength = 0;
			grapplePoint = Vector3.zero;
			myRB.velocity = additionalVelocity;

			//grapple gameobject
			Destroy(myGrappleHookObject);
		}
	}

	private void OnDrawGizmos()
	{
		if (grapplePoint != Vector3.zero && false)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere(grapplePoint, .5f);
		}
	}

	void Jump()
	{
		if (Input.GetKey(KeyCode.Space) && myGroundChecker.IsGrounded && timeSinceLastJump > 0.1)
		{
			timeSinceLastJump = 0;
			myRB.AddForce(Vector3.up * jumpVelocity, ForceMode.VelocityChange);
		}
	}

	private void SendPosition()
	{
		Message message = Message.Create(MessageSendMode.Reliable, ClientToServerId.playerTransform);
		message.AddUShort(player.Id);

		//movement
		message.AddVector3(myCamera.transform.forward);
		message.AddVector3(transform.position);
		message.AddQuaternion(transform.rotation);

		Debug.Log(myCamera.transform.forward + ", " + transform.position + ", " + transform.rotation);

		NetworkManager.Singleton.Client.Send(message);
	}

}