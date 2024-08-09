using System;
using UnityEngine;

public class TextureScroller : MonoBehaviour
{
	private Renderer ren;

	private void Start()
	{
		this.ren = base.GetComponent<Renderer>();
	}

	private void LateUpdate()
	{
		this.ren.material.mainTextureOffset = new Vector2(0f, Camera.main.transform.position.y * 0.1f * this.ren.material.mainTextureScale.y);
	}
}
