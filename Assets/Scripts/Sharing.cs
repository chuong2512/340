using System;
using UnityEngine;

public class Sharing : MonoBehaviour
{
	private string subject = "Paper Fall";

	private string body = "I scored " + Sys.NormalizeBigNumber(Globals.playerPoints.ToString()) + " points in the game 'Paper Fall'! Can you beat my score? https://goo.gl/LbmsHW";

	private void Start()
	{
		if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Belarusian || Application.systemLanguage == SystemLanguage.Ukrainian)
		{
			this.body = "Мой результат в игре Paper Fall - " + Sys.NormalizeBigNumber(Globals.playerPoints.ToString()) + "! Кто сможет побить мой рекорд? https://goo.gl/LbmsHW";
		}
	}

	private void Update()
	{
	}

	public void Share()
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.content.Intent");
		AndroidJavaObject androidJavaObject = new AndroidJavaObject("android.content.Intent", new object[0]);
		androidJavaObject.Call<AndroidJavaObject>("setAction", new object[]
		{
			androidJavaClass.GetStatic<string>("ACTION_SEND")
		});
		androidJavaObject.Call<AndroidJavaObject>("setType", new object[]
		{
			"text/plain"
		});
		androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[]
		{
			androidJavaClass.GetStatic<string>("EXTRA_SUBJECT"),
			this.subject
		});
		androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[]
		{
			androidJavaClass.GetStatic<string>("EXTRA_TEXT"),
			this.body
		});
		AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject @static = androidJavaClass2.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaObject androidJavaObject2 = androidJavaClass.CallStatic<AndroidJavaObject>("createChooser", new object[]
		{
			androidJavaObject,
			"Share Via"
		});
		@static.Call("startActivity", new object[]
		{
			androidJavaObject2
		});
	}
}
