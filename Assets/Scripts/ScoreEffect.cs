using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreEffect : MonoBehaviour
{
	private float startTime;

	private Text txt;

	private void Start()
	{
		this.startTime = Time.time;
		this.txt = base.GetComponent<Text>();
		base.transform.localScale = new Vector3(0f, 0f, 0f);
	}

	private void Update()
	{
		this.txt.color = new Color(this.txt.color.r, this.txt.color.g, this.txt.color.b, Mathf.Lerp(1f, 0f, (Time.time - this.startTime) * 2f));
		float num = Mathf.Lerp(0f, 1f, (Time.time - this.startTime) * 8f);
		base.transform.localScale = new Vector3(num, num, 1f);
		base.transform.Translate(new Vector3(0f, 5.5f, 0f));
		if (Time.time - this.startTime > 1f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
