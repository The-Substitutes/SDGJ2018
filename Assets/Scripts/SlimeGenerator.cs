using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LineRenderer))]
public class SlimeGenerator : MonoBehaviour {

	public int numPoints = 15;

	public float springForce = 500f;

	public float size = .5f;

	public List<Rigidbody> points;
	public List<SpringJoint> sjoints;
	
	Rigidbody rigid;
	LineRenderer lr;


	// Use this for initialization
	void Start () {

		rigid = this.GetComponent<Rigidbody>();

		lr = this.GetComponent<LineRenderer>();
		lr.positionCount = numPoints;

		for(int i = 0; i < numPoints; i++) {
			GameObject go = new GameObject();
			go.transform.parent = this.transform;
			go.transform.position = this.transform.position + new Vector3(size * Mathf.Cos(i * 2 * Mathf.PI / numPoints), size * Mathf.Sin(i * 2 * Mathf.PI / numPoints));
			go.transform.localScale = new Vector3(.2f, .2f, .01f);
			
			Rigidbody r = go.AddComponent<Rigidbody>();
			r.freezeRotation = true;

			SpringJoint s = go.AddComponent<SpringJoint>();
			s.connectedBody = rigid;
			s.spring = springForce;

			SphereCollider coll = go.AddComponent<SphereCollider>();

			points.Add(r);
			sjoints.Add(s);
		}


	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < numPoints; i++) {
			lr.SetPosition(i, points[i].transform.position);
		}
	}
}
