using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {


	public SpriteRenderer sr;
	public float h = 0;

	public SpriteRenderer shadow;

	[HideInInspector]
	public Rigidbody rigid;

	public float movementSpeed = 1;
	public float sprintMultiplier = 2;

	public float jumpVelocity = 1;

	public float jumpTime = .5f;

	public bool isGrounded = false;
	public float rotationSpeed = 10;


	public GameObject graphic;

	// Use this for initialization
	void Start () {
		rigid = this.GetComponent<Rigidbody>();
		sr = this.GetComponentInChildren<SpriteRenderer>();
		shadow.transform.parent = this.transform;
	}


	void FixedUpdate() {
		Movement();
		sr.color = Color.HSVToRGB(h, 1, 1);


		RaycastHit[] hits = Physics.RaycastAll(new Ray(this.transform.position, Vector3.down));
		for (int i = 0; i < hits.Length; i++) {
			Debug.Log(i + " " + hits[i]);
		}

		if(hits.Length >= 2) {
			shadow.transform.position = hits[1].point + Vector3.up * .1f;
		}
	}


	public bool isJumping = false;
	void Jump() {
		if(!isJumping && isGrounded) {
			rigid.velocity = new Vector3(rigid.velocity.x, jumpVelocity, rigid.velocity.z);
			//rigid.velocity = new Vector3(rigid.velocity.x, contactPoints.normalized.y * jumpVelocity, rigid.velocity.z);
			isGrounded = false;
			rigid.useGravity = false;
			isJumping = true;
		}
		//jumpHold = Mathf.Clamp(2 + holdTimer, 3-jumpTime , 3 + jumpTime);
		rigid.AddForce(Physics.gravity / 2, ForceMode.Acceleration);
	}


	float rotation = 180;
	void Movement() {
		
		if(Input.GetKey(KeyCode.LeftShift)) {
			h = Time.fixedTime % 1;
			rigid.velocity = new Vector3(Input.GetAxis("Horizontal") * movementSpeed * sprintMultiplier, rigid.velocity.y, Input.GetAxis("Vertical") * sprintMultiplier * movementSpeed);
		} else {
			this.rigid.velocity = new Vector3(Input.GetAxis("Horizontal")  * movementSpeed, rigid.velocity.y, Input.GetAxis("Vertical")  * movementSpeed);
		}


		if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.01){
			rotation = 90 + 90 * Mathf.Sign(Input.GetAxis("Horizontal"));
		}

		graphic.transform.rotation = Quaternion.Lerp(graphic.transform.rotation, Quaternion.Euler(0, rotation, 0), rotationSpeed * Time.deltaTime);
		//this.transform.rotation = Quaternion.Euler(0, rotation, 0);


		if(Input.GetKey(KeyCode.Space)) {
			Jump();
		}

		if(isJumping && rigid.velocity.y <= 0) {
			rigid.useGravity = true;
			isJumping = false;
		} else {
			rigid.AddForce(Physics.gravity / 2, ForceMode.Acceleration);
		}
			
	}

	Vector3 contactPoints;

	void OnCollisionExit(Collision other) {
		isGrounded = false;
	}

	void OnCollisionStay(Collision other) {
		Vector3 contactSum = Vector3.zero;
		for (int i = 0; i < other.contacts.Length; i++) {
			contactSum += other.contacts[i].point;
			Debug.DrawLine(this.transform.position, other.contacts[i].point, Color.yellow);
		}

		contactPoints = (contactSum /= other.contacts.Length) - this.transform.position;

		Debug.DrawLine(this.transform.position, this.transform.position + contactPoints, Color.red);
		Debug.DrawLine(this.transform.position, this.transform.position + Vector3.down, Color.green);

		if(Vector3.Angle(contactPoints, Vector3.down) < 18.53759) {
			isGrounded = true;
		}
	}

}
