using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimSped : MonoBehaviour {

    Animator anim;

	// Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();
        anim.speed = Random.Range(.9f, 1.1f);
	}
	
}
