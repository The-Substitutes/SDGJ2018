using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform Follow;

	[Space]
	public bool lockCameraFromEditor = true;
	public Vector3 cameraOffset;

	// Use this for initialization
	void Start () {
		if(lockCameraFromEditor) {
			cameraOffset =  this.transform.position - Follow.transform.position;
			cameraOffset.z = this.transform.position.z;
		}
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3(	Follow.transform.position.x + cameraOffset.x,
												Follow.transform.position.y + cameraOffset.y,
												 cameraOffset.z);
	}
}
