using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BlueTint : MonoBehaviour {


	public Color color;
	public Material tintMat;

	private void OnRenderImage(RenderTexture src, RenderTexture dst) {

		if (tintMat == null) {
			tintMat = new Material(Shader.Find("Custom/Tint"));
		}

		tintMat.SetColor("_Tint", color);
	
		Graphics.Blit(src, dst, tintMat);
	}
}
