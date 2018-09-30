using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform Follow;

	[Space]
	public bool lockCameraFromEditor = true;
    public Vector3 followInDirection;

	public Vector3 cameraOffset;

	// Use this for initialization
	void Start () {
		if(lockCameraFromEditor) {
			cameraOffset =  this.transform.position - Follow.transform.position;
            if(followInDirection.z != 0){
                cameraOffset.z = this.transform.position.z;
            }
			
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		this.transform.position = new Vector3(followInDirection.x * Follow.transform.position.x + cameraOffset.x,
                                              followInDirection.y * Follow.transform.position.y + cameraOffset.y,
                                              followInDirection.z * Follow.transform.position.z + cameraOffset.z);
	}
}
