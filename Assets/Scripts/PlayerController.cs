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
		if(holdTimer < jumpTime && isGrounded) {
			if(!isJumping) {
				rigid.velocity = new Vector3(rigid.velocity.x, jumpVelocity, rigid.velocity.z);
				isGrounded = false;
			}
			holdTimer += Time.fixedDeltaTime;
			jumpHeight = Mathf.Clamp(4 - holdTimer, 2, 4);
			rigid.velocity += -Physics.gravity / jumpHeight;
		}
	}

	void Movement() {
		this.rigid.velocity = new Vector3(Input.GetAxis("Horizontal"), rigid.velocity.y, Input.GetAxis("Vertical")) * movementSpeed;

		if(Input.GetKey(KeyCode.Space)) {
			Jump();
		} else {
			isJumping = false;
			holdTimer = 0;
		}
	}

	public Vector3 contactPoints;

	void OnCollisionStay(Collision other) {
		Vector3 contactSum = Vector3.zero;
		for (int i = 0; i < other.contacts.Length; i++) {
			contactSum += other.contacts[i].point;
		}

		contactSum -= this.transform.position;

		Debug.DrawLine(this.transform.position, this.transform.position + contactSum.normalized, Color.red);

		Debug.Log(Vector3.Angle(contactSum, Vector3.down));

		if(Vector3.Angle(contactSum.normalized, Vector3.down) < 5f) {
			isGrounded = true;
		}
	}

}
