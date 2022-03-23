using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovementBehavior : MonoBehaviour
{
	//controls
	readonly KeyCode forwardButton = KeyCode.W;
	readonly KeyCode backwardButton = KeyCode.S;
	readonly KeyCode leftButton = KeyCode.A;
	readonly KeyCode rightButton = KeyCode.D;
	readonly KeyCode grappleButton = KeyCode.Mouse0;

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

	//movement stats
	readonly float friction = 7.5f;
	readonly float topSpeed = 10;
	readonly float airAcceleration = 1.22f;
	readonly float groundAcceleration = 12;
	Vector3 moveInput = new Vector3();

	//jump Stats
	float jumpVelocity = 20;
	float timeSinceLastJump = 60;

	//grapple
	Vector3 grapplePoint;
	Rigidbody myGrappleObjectRB;
	Rigidbody grappleObjectRBPrevConf;
	ConfigurableJoint grapple;

	//physics stuff
	readonly int playerPhysicsIndex = 3;
	Rigidbody myRB;
	GroundCheckingBehavior myGroundChecker;

	private void Awake()
	{
		//
		References.thePlayer = gameObject;

		//get a reference to my rigidbody before the first frame
		myRB = GetComponent<Rigidbody>();

		//get a reference to my ground checker
		myGroundChecker = GetComponent<GroundCheckingBehavior>();
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
	}

	private void FixedUpdate()
	{

	}

	void Timers()
	{
		timeSinceLastJump += Time.deltaTime;
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

		//call our friction function if we're on the ground
		if (myGroundChecker.IsGrounded)
			MoveOnGround(myRB.velocity, moveInput);
		else
			Accelerate(myRB.velocity, airAcceleration, moveInput);
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

			/* when I grapple I have to
			 * 1. get a reference to the object I'm grappling
			 * 2. get check if that thing has rigidbody
			 * 3. if it does, save those parameters we're about to change about it
			 * 4. add the "configurable joint" component to it, automatically adding a rigidbody as well
			 * 5. get a reference to it again, in case it didn't exist a moment ago
			 * 6. configure that RB
			 */

			RaycastHit hit;
			// Does the ray intersect any objects excluding the player layer
			if (Physics.Raycast(myCamera.transform.position, myCamera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
			{
				//start by making sure we aren't already attached to something
				Ungrapple();

				//get a reference to the object I grappled
				GameObject myGrappleObject = hit.collider.gameObject;
				myGrappleObjectRB = myGrappleObject.GetComponent<Rigidbody>();

				//if this object has a rigid body...
				if (myGrappleObjectRB != null)
				{
					//save that object's RB so we can put it back the way it was later
					grappleObjectRBPrevConf = new Rigidbody();
					grappleObjectRBPrevConf = myGrappleObjectRB;
				}

				//create our hingejoint. automatically adds a rigidbody as well.
				grapple = myGrappleObject.AddComponent<ConfigurableJoint>();
				

				//if we don't have an RB saved for our object, then save it.
				if (myGrappleObjectRB == null)
				{
					myGrappleObjectRB = myGrappleObject.GetComponent<Rigidbody>();
				}

				//configure the the RB
				//if the object didn't have a rigidbody to begin with, then the object shouldn't move
				if (grappleObjectRBPrevConf == null)
				{
					myGrappleObjectRB.constraints = RigidbodyConstraints.FreezeAll;
				}

				//configure grapple
				grapple.xMotion = ConfigurableJointMotion.Limited;
				grapple.yMotion = ConfigurableJointMotion.Limited;
				grapple.zMotion = ConfigurableJointMotion.Limited;

				grapplePoint = hit.point;
				Debug.Log("Did Hit");
			}
			else
			{
				Ungrapple();
				Debug.Log("Did not Hit");
				grapplePoint = Vector3.zero;
			}
		}

		if (Input.GetKeyUp(grappleButton))
		{
			Ungrapple();
		}
	}

	void Ungrapple()
	{
		//destroy our tether
		if (grapple != null)
			Destroy(grapple);

		//if the object didn't have an RB before, then get rid of the one we made
		if (grappleObjectRBPrevConf == null)
		{
			if (myGrappleObjectRB != null)
				Destroy(myGrappleObjectRB);
		}
		//otherwise, put it back to the way it was
		else
		{
			if (myGrappleObjectRB != null)
				myGrappleObjectRB = grappleObjectRBPrevConf;
		}

		grapple = null;
		myGrappleObjectRB = null;
		grappleObjectRBPrevConf = null;

	}

	private void OnDrawGizmos()
	{
		if (grapplePoint != Vector3.zero)
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

}
