using MySql.Data.MySqlClient;
using Riptide;
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
	readonly KeyCode grappleButton = KeyCode.Mouse1;
	readonly KeyCode boopButton = KeyCode.Mouse0;

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
	float jumpVelocity = 10;
	float timeSinceLastJump = 60;
	float timeSinceBecameGrounded = 60;
	bool wasGrounded = false;

	//grapple
	readonly float maxGrappleDistance = 40;
	readonly float grappleCooldown = 2;
	float timeUntilGrappleAgain = 0;
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

	//boop
	static readonly float timeBetweenBoops = 1;
	static readonly float timeAfterBoopForCredit = 0.15f;
	static float timeSinceLastBoop = 120;
	static float verticalBoopSpeed = 20;
	static float horizontalBoopSpeed = 60;
	static float timeSinceBooped = 120;
	[SerializeField] ParticleSystem myBoopPS;

	//physics stuff
	readonly int playerPhysicsIndex = 3;
	Rigidbody myRB;
	GroundCheckingBehavior myGroundChecker;

	//RipTide Stuff

	//Audio Stuff

	public AudioClip grappleSound;
	public AudioClip punchSound;
	public static AudioClip punchedSound;

	private void OnValidate()
	{
		if (player == null)
			player = GetComponent<Player>();
	}

	private void Awake()
	{
		//tell everyone we're the player
		References.thePlayer = gameObject;
		References.localPlayerMovement = this;

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
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		//get my original rotation
		myRBOriginalRotation = myRB.transform.rotation;
		myCameraOriginalRotation = myCamera.transform.rotation;
	}





	// Update is called once per frame
	void Update()
	{
		Timers();
		if (!UIManager.isPaused)
		{
			MouseLook();
			Jump();
			MovementInput();
			Grapple();
			Boop();
		}
		CheckIfCanGrapple();
		if (timeSinceBooped >= timeAfterBoopForCredit && myGroundChecker.IsGrounded) Player.playerThatKilledMeID = -1;
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

		if (timeSinceLastBoop < timeBetweenBoops)
			timeSinceLastBoop += Time.deltaTime;

		if (timeSinceBooped <= timeAfterBoopForCredit)
			timeSinceBooped += Time.deltaTime;

		if (timeUntilGrappleAgain >= 0)
			timeUntilGrappleAgain -= Time.deltaTime;
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
		if (timeUntilGrappleAgain > 0)
			return;

		if (Input.GetKeyDown(grappleButton))
		{
			//create a layer mask that includes just the "player" physics layer
			int layerMask = 1 << playerPhysicsIndex;

			//invert that layer mask;
			layerMask = ~layerMask;

			RaycastHit hit;
			// Does the ray intersect any objects excluding the player layer
			//NOTE: this block runs once when the grapple starts
			if (Physics.Raycast(myCamera.transform.position, 
								myCamera.transform.TransformDirection(Vector3.forward), 
								out hit, maxGrappleDistance, layerMask) &&
			timeUntilGrappleAgain <= 0)
			{
				Debug.Log(timeUntilGrappleAgain);
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

				//SFX
				if (grappleSound != null)
					AudioSource.PlayClipAtPoint(grappleSound, transform.position, 1);
			}
		}

		if (Input.GetKeyUp(grappleButton))
		{
			Ungrapple();
		}
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
							out hit, maxGrappleDistance, layerMask) &&
		timeUntilGrappleAgain <= 0)
		{
			References.CrosshairColor = grappleableColor;
		}
		else
		{
			References.CrosshairColor = ungrappleableColor;
		}
	}

	public void Ungrapple()
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
		Message message = Message.Create(MessageSendMode.Unreliable, ClientToServerId.playerTransform);
		message.AddUShort(player.Id);

		//movement
		message.AddVector3(myCamera.transform.forward);
		message.AddVector3(transform.position);
		message.AddQuaternion(transform.rotation);
		message.AddVector3(myRB.velocity);

		NetworkManager.Singleton.Client.Send(message);
	}

	void Boop()
	{
		//if we try to boop while boop is available
		if (timeSinceLastBoop >= timeBetweenBoops && Input.GetKey(boopButton))
		{
			timeSinceLastBoop = 0;

			//play particle system
			if (myBoopPS != null)
				myBoopPS.Play();

			//send a message to the server that we're booping
			Message message = Message.Create(MessageSendMode.Unreliable, ClientToServerId.playerBooped);

			message.AddUShort(player.Id);

			NetworkManager.Singleton.Client.Send(message);

			//SFX
			if (punchSound != null)
				AudioSource.PlayClipAtPoint(punchSound, transform.position, 1);
		}
	}

	// This function is called when we are booped by another player
	[MessageHandler((ushort)ServerToClientId.playerBooped)]
	private static void GetBooped(Message message)
	{
		int booperID = message.GetUShort();

		Vector3 boopDirection = message.GetVector3();
		boopDirection *= horizontalBoopSpeed;
		if (boopDirection.y < verticalBoopSpeed)
		{
			boopDirection = Vector3.ProjectOnPlane(boopDirection, Vector3.up);
			boopDirection = new Vector3(boopDirection.x, boopDirection.y + verticalBoopSpeed, boopDirection.z);
		}
		References.thePlayer.GetComponent<Rigidbody>().velocity += boopDirection;

		if (punchedSound != null)
		{
            AudioSource.PlayClipAtPoint(punchedSound, new Vector3(boopDirection.x, boopDirection.y, boopDirection.z), 1);
        }

		Player.playerThatKilledMeID = booperID;

		timeSinceBooped = 0;
		References.localPlayerMovement.Ungrapple();
		References.localPlayerMovement.timeUntilGrappleAgain = References.localPlayerMovement.grappleCooldown;
	}
}
