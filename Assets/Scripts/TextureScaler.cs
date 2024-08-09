using System;
using UnityEngine;

public class TextureScaler : MonoBehaviour
{
	private void Start()
	{
	}

	public void Resize()
	{
		Renderer component = base.GetComponent<Renderer>();
		component.material.mainTextureScale = new Vector2(base.transform.lossyScale.x * 0.3f, base.transform.lossyScale.y * 0.3f);
	}

	public static void ResizeTextures()
	{
		TextureScaler[] array = UnityEngine.Object.FindObjectsOfType<TextureScaler>();
		TextureScaler[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			TextureScaler textureScaler = array2[i];
			textureScaler.Resize();
		}
	}
}
