using System;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
	private float updateInterval = 0.5f;

	private float lastInterval;

	private int frames;

	private float fps;

	private Text txt;

	private void Start()
	{
		this.lastInterval = Time.realtimeSinceStartup;
		this.frames = 0;
		this.txt = base.GetComponent<Text>();
	}

	private void Update()
	{
		this.frames++;
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		if (realtimeSinceStartup > this.lastInterval + this.updateInterval)
		{
			this.fps = (float)this.frames / (realtimeSinceStartup - this.lastInterval);
			this.frames = 0;
			this.lastInterval = realtimeSinceStartup;
		}
		this.txt.text = "Fps: " + this.fps.ToString("f2");
	}
}
