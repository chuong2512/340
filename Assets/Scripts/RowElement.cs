using System;
using UnityEngine;

public class RowElement : MonoBehaviour
{
	public int id;

	public bool isSlider;

	public GameObject[] darkBlock;

	public Transform slider;

	public Vector2 sliderClamp;

	public float sliderSpeed;

	public bool isEmpty = true;

	public float width;

	public Renderer[] coloredObjects;

	private bool hideFlag;

	private Vector3 hideDirection;

	private float slideSeedRnd;

	private float hideStartTime;

	private Vector3 hideStartScale;

	private void Start()
	{
		this.hideFlag = false;
		if (this.isSlider)
		{
			this.slideSeedRnd = UnityEngine.Random.Range(0f, 2f);
		}
	}

	private void Update()
	{
		if (this.hideFlag)
		{
		}
		if (!Globals.level.levelIsComplete && this.isSlider)
		{
			Vector3 localPosition = this.slider.localPosition;
			localPosition.x = Sys.SmoothLerp(this.sliderClamp.x, this.sliderClamp.y, Mathf.PingPong(Time.time * this.sliderSpeed + this.slideSeedRnd, 1f));
			this.slider.localPosition = localPosition;
		}
	}

	public void SetColor(Color col)
	{
		Renderer[] array = this.coloredObjects;
		for (int i = 0; i < array.Length; i++)
		{
			Renderer renderer = array[i];
			renderer.material.color = col;
		}
	}

	public void ActivateDarkBlock(int id)
	{
		if (this.darkBlock.Length > 0 && id < this.darkBlock.Length)
		{
			this.darkBlock[id].SetActive(true);
		}
	}
}
