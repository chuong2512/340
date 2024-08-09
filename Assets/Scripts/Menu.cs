using System;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
	public static bool isHided;

	public GameObject gameOverScreen;

	public GameObject levelCompleteScreen;

	public Text txtCurrentLevel;

	public Text txtNextLevel;

	public Text txtCompleteLevel;

	public Text txtPoints;

	public Text txtBest;

	public Text txtPlayerBestValue;

	public Text txtSliderProgress;

	public Text txtSharePoints;

	public GameObject menuContainer;

	public GameObject settingsContainer;

	public GameObject socialContainer;

	public GameObject exitScreen;

	public RateThis rateThis;

	public Button btnSoundOn;

	public Button btnSoundOff;

	public Button btnShadowsOn;

	public Button btnShadowsOff;

	public Button btnNoAds;

	public GameObject shadowCamera;

	public Button btnRewarded;

	public Button btnContinueNoAds;

	public Button btnVK;

	public GameObject sliderLevel;

	public Slider sliderShare;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
	{
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			if (this.rateThis.container.activeSelf)
			{
				this.rateThis.RateLater();
			}
			else
			{
				this.exitScreen.SetActive(!this.exitScreen.activeSelf);
				Time.timeScale = (float)Sys.BoolToInt(!this.exitScreen.activeSelf);
				if (this.exitScreen.activeSelf)
				{
					Globals.gameState = State.pause;
				}
				else
				{
					Globals.gameState = State.game;
				}
			}
		}
	}

	public void RestartGame()
	{
		this.gameOverScreen.SetActive(false);
		Globals.level.Clear();
		Globals.level.levelIsComplete = false;
		Globals.level.Generate();
		Globals.player.Reset();
		Globals.levelControl.Reset();
		Globals.gameState = State.game;
		this.BestShow();
	}

	public void StartFromControlPoint()
	{
		this.gameOverScreen.SetActive(false);
		Globals.level.levelIsComplete = false;
		Globals.level.RestartRewarded();
		Globals.player.MoveToControlPoint();
		Globals.player.ResetForRewarded();
		Globals.levelControl.Reset();
		Globals.gameState = State.game;
		Globals.menu.btnContinueNoAds.gameObject.SetActive(false);
	}

	public void Show()
	{
		this.menuContainer.SetActive(true);
		this.settingsContainer.SetActive(false);
		this.socialContainer.SetActive(false);
		Menu.isHided = false;
	}

	public void Hide()
	{
		this.menuContainer.SetActive(false);
		this.settingsContainer.SetActive(false);
		this.socialContainer.SetActive(false);
		this.BestHide();
		Menu.isHided = true;
	}

	public void BestShow()
	{
		this.txtBest.gameObject.SetActive(true);
		this.txtPlayerBestValue.gameObject.SetActive(true);
	}

	public void BestHide()
	{
		this.txtBest.gameObject.SetActive(false);
		this.txtPlayerBestValue.gameObject.SetActive(false);
	}

	public void Mute(bool val)
	{
		Globals.settings_Sound = Sys.BoolToInt(!val);
		AudioListener.volume = (float)Globals.settings_Sound;
		PlayerPrefs.SetInt("Settings_Sound", Globals.settings_Sound);
	}

	public void ShadowsVisible(bool val)
	{
		Globals.settings_Shadows = Sys.BoolToInt(val);
		this.shadowCamera.SetActive(val);
		PlayerPrefs.SetInt("Settings_Shadows", Globals.settings_Shadows);
	}

	public void Share(string url)
	{
		if (url.Equals("fb"))
		{
			base.StartCoroutine(Sys.ShareFacebook("1288095161207873", "https://ponystudio92.wixsite.com", "Ball Fall", "Google Play", "I scored " + Sys.NormalizeBigNumber(Globals.playerPoints.ToString()) + " points in the game 'Paper Fall'! Can you beat my score?"));
		}
		if (url.Equals("tw"))
		{
			base.StartCoroutine(Sys.ShareTwitter("I scored " + Sys.NormalizeBigNumber(Globals.playerPoints.ToString()) + " points in the game 'Paper Fall'! Can you beat my score?", "https://play.google.com/store/apps/details?id=com.ponygames.BallFall2D", "@PonyGames"));
		}
		if (url.Equals("vk"))
		{
			base.StartCoroutine(Sys.ShareVk("https://play.google.com/store/apps/details?id=com.ponygames.BallFall2D", "Ball Fall", "Мой результат в игре Paper Fall - " + Sys.NormalizeBigNumber(Globals.playerPoints.ToString()) + "! Кто сможет побить мой рекорд?"));
		}
	}

	public void OpenURL(string url)
	{
		Application.OpenURL(url);
	}

	public void Exit()
	{
		Application.Quit();
	}
}
