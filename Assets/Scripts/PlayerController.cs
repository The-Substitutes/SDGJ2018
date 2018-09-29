using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

	Rigidbody rigid;

	public float movementSpeed = 1;

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


	bool isJumping = false;
	float jumpHeight = 0, holdTimer;
	void Jump() {
		if(holdTimer < jumpTime) {
			if(!isJumping) {
				//rigid.velocity = new Vector3(rigid.velocity.x, jumpVelocity, rigid.velocity.z);
				rigid.velocity = rigid.velocity + contactPoints.normalized * jumpVelocity;
				isGrounded = false;
			}
			holdTimer += Time.fixedDeltaTime;
			jumpHeight = Mathf.Clamp(4 - holdTimer, 2, 4);
			rigid.velocity += -Physics.gravity / jumpHeight;
		}
	}


	public float rotation = 90;
	void Movement() {
		this.rigid.velocity = new Vector3(Input.GetAxis("Horizontal")  * movementSpeed, rigid.velocity.y, Input.GetAxis("Vertical")  * movementSpeed);

		if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.01){
			rotation = 90 - 90 * Mathf.Sign(Input.GetAxis("Horizontal"));
		}

		this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(0, rotation, 0), rotationSpeed * Time.deltaTime);


		if(Input.GetKey(KeyCode.Space) && isGrounded) {
			Jump();
		} else {
			isJumping = false;
			holdTimer = 0;
		}
	}

	public Vector3 contactPoints;

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

		Debug.DrawLine(this.transform.position, this.transform.position + contactSum.normalized, Color.red);
	}

}
