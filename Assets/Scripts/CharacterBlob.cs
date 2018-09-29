using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CharacterBlob : MonoBehaviour {

	public int numOfPoints;
	public Material mat;

	public List<Vector3> vertices;
	public List<int> triangles;

	MeshFilter filter;
	MeshRenderer render;
	Mesh mesh;

	// Use this for initialization
	void Start () {
		filter = this.GetComponent<MeshFilter>();
		render = this.GetComponent<MeshRenderer>();
		mesh = filter.mesh;

		mesh.Clear();

		for(int i = 0; i < numOfPoints; i++) {
			vertices.Add(new Vector3( i * Mathf.Cos(2 * Mathf.PI) / 50,   // X component for circle
									  i * Mathf.Cos(2 * Mathf.PI) / 50)); // Y component for circle
		}

		for(int i = 1; i < numOfPoints - numOfPoints % 3; i+=3) {
			triangles.Add(0);
			triangles.Add(i);
			triangles.Add(i+1);
		}

		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();

		mesh.RecalculateNormals();
		mesh.RecalculateTangents();
		mesh.RecalculateBounds();

		render.material = mat;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
