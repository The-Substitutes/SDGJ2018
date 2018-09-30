using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBounce : MonoBehaviour {

	[Range(4, 5)]
	public float harmoticRate = 1;
	[Range(0, 0.5f)]
	public float bounceHeight = 1;
	
	Vector3 initPos;

	void Start() {
		initPos = this.transform.localPosition;
	}

	// Update is called once per frame
	void Update () {
		this.transform.localPosition = initPos + Vector3.up * bounceHeight * Mathf.Sin(harmoticRate * Time.time);

	}
}
