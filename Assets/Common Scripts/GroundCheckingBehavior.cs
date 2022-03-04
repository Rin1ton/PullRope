using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckingBehavior : MonoBehaviour
{

	public Vector3 Ground => currentGround;
	public bool IsGrounded => currentGround != Vector3.zero;

	Vector3 currentGround = Vector3.zero;

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
		currentGround = Vector3.up;
	}

	private void OnCollisionExit(Collision collision)
	{
		currentGround = Vector3.zero;
	}



}
