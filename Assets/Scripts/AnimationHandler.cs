using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour {

	Animator animator;
	PlayerController controller;

	float speed;

	// Use this for initialization
	void Start () {
		animator = this.GetComponentInChildren<Animator>();
		controller = this.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetFloat("Horizontal_Speed", Mathf.Abs(controller.rigid.velocity.x));
	}
}
