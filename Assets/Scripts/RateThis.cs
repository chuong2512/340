using System;
using UnityEngine;

public class RateThis : MonoBehaviour
{
	private static int counter;

	public GameObject container;

	private string rateURL = "market://details?id=com.AnionSoftware.PaperFall";

	private void Start()
	{
		if (!PlayerPrefs.HasKey("RateThisCounter"))
		{
			PlayerPrefs.SetInt("RateThisCounter", 1);
			RateThis.counter = 1;
		}
		else
		{
			RateThis.counter = PlayerPrefs.GetInt("RateThisCounter");
			if (RateThis.counter > 0)
			{
				RateThis.counter++;
				PlayerPrefs.SetInt("RateThisCounter", RateThis.counter);
				if (Mathf.RoundToInt(Mathf.Repeat((float)RateThis.counter, 4f)) == 3)
				{
					this.container.SetActive(true);
					Time.timeScale = 0f;
					Globals.gameState = State.pause;
				}
			}
		}
	}

	public void RateNow()
	{
		PlayerPrefs.SetInt("RateThisCounter", -1);
		this.container.SetActive(false);
		Time.timeScale = 1f;
		Globals.gameState = State.game;
		Application.OpenURL(this.rateURL);
	}

	public void RateLater()
	{
		this.container.SetActive(false);
		Time.timeScale = 1f;
		Globals.gameState = State.game;
	}

	public void RateNever()
	{
		PlayerPrefs.SetInt("RateThisCounter", -1);
		this.container.SetActive(false);
		Time.timeScale = 1f;
		Globals.gameState = State.game;
	}
}
