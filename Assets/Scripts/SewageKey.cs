using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewageKey : MonoBehaviour {

	public GameObject go;
	List<GameObject> trash;


	void Update() {
		if(trash.Count >= 4) {
			go.SetActive(false);
		}
	}

	void OnTriggerEnter(Collision other) {
		if(other.transform.tag == "Trash") {
			trash.Add(other.gameObject);
		}
	}

	void OnTriggerExit(Collision other) {
		if(other.transform.tag == "Trash") {
			trash.Remove(other.gameObject);
		}
	}

}
