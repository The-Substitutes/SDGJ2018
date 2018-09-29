﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

	Rigidbody rigid;

	public float movementSpeed = 1;
	public float sprintSpeed = 2;

	public float jumpVelocity = 1;

	public float jumpTime = .5f;

	public bool isGrounded = false;
	public float rotationSpeed = 10;


	// Use this for initialization
	void Start () {
		rigid = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {
		Movement();
	}


	public bool isJumping = false;
	public float jumpHold = 2, holdTimer;
	void Jump() {
		if(holdTimer < jumpTime) {
			if(!isJumping && isGrounded) {
				rigid.velocity = new Vector3(rigid.velocity.x, jumpVelocity, rigid.velocity.z);
				//rigid.velocity = new Vector3(rigid.velocity.x, contactPoints.normalized.y * jumpVelocity, rigid.velocity.z);
				isGrounded = false;
				rigid.useGravity = false;
				isJumping = true;
			}
			holdTimer += Time.fixedDeltaTime;
			jumpHold = Mathf.Clamp(2 + holdTimer, 3-jumpTime , 3 + jumpTime);
			rigid.AddForce(Physics.gravity / jumpHold, ForceMode.Acceleration);
		}
	}


	float rotation = 180;
	void Movement() {
		
		if(Input.GetKey(KeyCode.LeftShift)) {
			rigid.velocity = new Vector3(Input.GetAxis("Horizontal")  * sprintSpeed, rigid.velocity.y, Input.GetAxis("Vertical")  * sprintSpeed);
		} else {
			this.rigid.velocity = new Vector3(Input.GetAxis("Horizontal")  * movementSpeed, rigid.velocity.y, Input.GetAxis("Vertical")  * movementSpeed);
		}


		if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.01){
			rotation = 90 + 90 * Mathf.Sign(Input.GetAxis("Horizontal"));
		}

		this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(0, rotation, 0), rotationSpeed * Time.deltaTime);

		if(Input.GetKey(KeyCode.Space)) {
			Jump();
			holdTimer = 0;
		}

		if(isJumping && rigid.velocity.y <= 0) {
			rigid.useGravity = true;
			isJumping = false;
		} else {
			Debug.Log(Physics.gravity / jumpHold);
			rigid.AddForce(Physics.gravity / jumpHold, ForceMode.Acceleration);
		}
			
	}

	Vector3 contactPoints;

	void OnCollisionExit(Collision other) {
		isGrounded = false;
	}

	void OnCollisionStay(Collision other) {
		isGrounded = true;
		Vector3 contactSum = Vector3.zero;
		for (int i = 0; i < other.contacts.Length; i++) {
			contactSum += other.contacts[i].point;
		}

		contactPoints = contactSum -= this.transform.position;
	}

}
