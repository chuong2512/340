using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderProgress : MonoBehaviour
{
	public Text txt;

	private Slider sli;

	private float playerMinPos;

	private float levelDist;

	private Lang lang;

	private float oldSliderVal;

	private void Awake()
	{
		this.sli = base.GetComponent<Slider>();
		this.lang = this.txt.GetComponent<Lang>();
		this.oldSliderVal = float.NegativeInfinity;
	}

	private void Update()
	{
		if (Globals.player.transform.position.y < this.playerMinPos)
		{
			this.playerMinPos = Globals.player.transform.position.y;
		}
		float num = Mathf.Lerp(0f, 1f, this.playerMinPos / this.levelDist);
		if (this.oldSliderVal != num)
		{
			this.sli.value = num;
			string mainString = this.lang.mainString;
			this.txt.text = mainString + " " + Mathf.CeilToInt(100f * num).ToString() + "%";
			this.oldSliderVal = num;
		}
	}

	public void ResetProgress()
	{
		this.playerMinPos = float.PositiveInfinity;
		this.levelDist = Globals.level.bucket.transform.position.y;
		this.sli.value = 0f;
		this.oldSliderVal = float.NegativeInfinity;
	}
}
