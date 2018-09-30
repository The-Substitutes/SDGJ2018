using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tpscript : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnTriggerEnter(Collider other)
    {
        other.transform.parent.position -= new Vector3(16, 0);
        Debug.Log("Entered ", gameObject);
    }
}
