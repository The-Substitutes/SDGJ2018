using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public GameObject bullet;
	public Transform barrelExit;
	public Vector3 direction;

	void Update () {
		if(Input.GetKeyDown(KeyCode.Return)) {
			GameObject go = Instantiate(bullet, barrelExit.position, Quaternion.identity, null);
			go.AddComponent<Rigidbody>().velocity = direction * Mathf.Sign(Input.GetAxis("Horizontal"));
		}
	}
}
