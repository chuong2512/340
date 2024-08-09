using System;
using UnityEngine;

public class RowHider : MonoBehaviour
{
	public RowElement[] rowElems;

	private float startTime;

	private GameObject pivot;

	private void Start()
	{
		this.pivot = new GameObject();
		this.pivot.transform.position = new Vector3(Globals.player.transform.position.x, base.transform.position.y, base.transform.position.z);
		this.pivot.transform.parent = base.transform.parent;
		base.transform.parent = this.pivot.transform;
		this.startTime = Time.time;
		RowElement[] array = this.rowElems;
		for (int i = 0; i < array.Length; i++)
		{
			RowElement rowElement = array[i];
			Renderer[] coloredObjects = rowElement.coloredObjects;
			GameObject[] darkBlock = rowElement.darkBlock;
			Renderer[] array2 = coloredObjects;
			for (int j = 0; j < array2.Length; j++)
			{
				Renderer renderer = array2[j];
				renderer.material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
				renderer.gameObject.layer = 2;
			}
			GameObject[] array3 = darkBlock;
			for (int k = 0; k < array3.Length; k++)
			{
				GameObject gameObject = array3[k];
				Renderer component = gameObject.GetComponent<Renderer>();
				gameObject.layer = 2;
				if (component)
				{
					component.material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
				}
			}
		}
	}

	private void Update()
	{
		float num = Time.time - this.startTime;
		float d = Mathf.Lerp(1f, 10f, num * 3f);
		float a = Mathf.Lerp(1f, 0f, num * 3f);
		this.pivot.transform.localScale = Vector3.one * d;
		RowElement[] array = this.rowElems;
		for (int i = 0; i < array.Length; i++)
		{
			RowElement rowElement = array[i];
			Renderer[] coloredObjects = rowElement.coloredObjects;
			GameObject[] darkBlock = rowElement.darkBlock;
			Renderer[] array2 = coloredObjects;
			for (int j = 0; j < array2.Length; j++)
			{
				Renderer renderer = array2[j];
				Color color = renderer.material.color;
				renderer.material.color = new Color(color.r, color.g, color.b, a);
			}
			GameObject[] array3 = darkBlock;
			for (int k = 0; k < array3.Length; k++)
			{
				GameObject gameObject = array3[k];
				Renderer component = gameObject.GetComponent<Renderer>();
				if (component)
				{
					Color color2 = component.material.color;
					component.material.color = new Color(color2.r, color2.g, color2.b, a);
				}
			}
		}
		if ((Time.time - this.startTime) * 3f >= 1f)
		{
			UnityEngine.Object.Destroy(this.pivot);
		}
	}
}
