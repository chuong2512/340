using System;
using UnityEngine;

public class Globals : MonoBehaviour
{
	public static Player player;

	public static Level level;

	public static LevelControl levelControl;

	public static Data data;

	public static SliderProgress sliderProgress;

	public static Menu menu;

	public static Canvas canvas;

	public static int playerPoints;

	public static int playerBest;

	public static Colors colors;

	public static int gameState;

	public static int settings_Sound;

	public static int settings_Shadows;

	public static int currentLevel;

	public static bool noAds;

	private void Awake()
	{
		Globals.player = GameObject.Find("Player").GetComponent<Player>();
		Globals.level = GameObject.Find("Level").GetComponent<Level>();
		Globals.levelControl = GameObject.Find("Level").GetComponent<LevelControl>();
		Globals.data = GameObject.Find("MainController").GetComponent<Data>();
		Globals.sliderProgress = GameObject.Find("Canvas/Slider").GetComponent<SliderProgress>();
		Globals.menu = GameObject.Find("MainController").GetComponent<Menu>();
		Globals.canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		Globals.colors = new Colors();
		this.Init();
	}

	private void Start()
	{
		Globals.level.Generate();
	}

	private void Init()
	{
		if (!PlayerPrefs.HasKey("SystemCounter"))
		{
			PlayerPrefs.SetInt("SystemCounter", 1);
			PlayerPrefs.SetInt("CurrentLevel", 1);
			PlayerPrefs.SetInt("PlayerPoints", 0);
			PlayerPrefs.SetInt("PlayerBest", 0);
			PlayerPrefs.SetInt("Settings_Sound", 1);
			PlayerPrefs.SetInt("Settings_Shadows", 1);
			PlayerPrefs.SetInt("NoAds", 0);
			Globals.currentLevel = 1;
			Globals.playerPoints = 0;
			Globals.playerBest = 0;
			Globals.settings_Sound = 1;
			Globals.settings_Shadows = 1;
			Globals.noAds = false;
			PlayerPrefs.SetString("Hash", Sys.MD5(string.Concat(new string[]
			{
				Globals.currentLevel.ToString(),
				Globals.playerPoints.ToString(),
				Globals.playerBest.ToString(),
				Sys.BoolToInt(Globals.noAds).ToString(),
				"Nenavizhu_Blyat'_Chiterov_e28f45g6^$%ef2F3g^&j8JS#$Twf#$%^$H&*jj6ijuYHerferwdf436g56*67876Yr34%^$%^&765JUIIJ*78j$%^^G4%F4&H5yH^"
			})));
		}
		else
		{
			Globals.currentLevel = PlayerPrefs.GetInt("CurrentLevel");
			Globals.playerPoints = PlayerPrefs.GetInt("PlayerPoints");
			Globals.playerBest = PlayerPrefs.GetInt("PlayerBest");
			Globals.settings_Sound = PlayerPrefs.GetInt("Settings_Sound");
			Globals.settings_Shadows = PlayerPrefs.GetInt("Settings_Shadows");
			Globals.noAds = Sys.IntToBool(PlayerPrefs.GetInt("NoAds"));
			Globals.menu.btnSoundOn.gameObject.SetActive(Sys.IntToBool(Globals.settings_Sound));
			Globals.menu.btnSoundOff.gameObject.SetActive(!Sys.IntToBool(Globals.settings_Sound));
			Globals.menu.btnShadowsOn.gameObject.SetActive(Sys.IntToBool(Globals.settings_Shadows));
			Globals.menu.btnShadowsOff.gameObject.SetActive(!Sys.IntToBool(Globals.settings_Shadows));
			string value = Sys.MD5(string.Concat(new string[]
			{
				Globals.currentLevel.ToString(),
				Globals.playerPoints.ToString(),
				Globals.playerBest.ToString(),
				Sys.BoolToInt(Globals.noAds).ToString(),
				"Nenavizhu_Blyat'_Chiterov_e28f45g6^$%ef2F3g^&j8JS#$Twf#$%^$H&*jj6ijuYHerferwdf436g56*67876Yr34%^$%^&765JUIIJ*78j$%^^G4%F4&H5yH^"
			}));
			if (!PlayerPrefs.GetString("Hash").Equals(value))
			{
				this.ResetSettings();
			}
			if (!Sys.IntToBool(Globals.settings_Sound))
			{
				AudioListener.volume = 0f;
			}
			if (!Sys.IntToBool(Globals.settings_Shadows))
			{
				Globals.menu.shadowCamera.gameObject.SetActive(false);
			}
			if (Globals.noAds)
			{
				Globals.menu.btnNoAds.gameObject.SetActive(false);
			}
		}
		Globals.menu.txtCurrentLevel.text = Globals.currentLevel.ToString();
		Globals.menu.txtNextLevel.text = (Globals.currentLevel + 1).ToString();
		Globals.menu.txtPlayerBestValue.text = Globals.playerBest.ToString();
		Globals.level.ChangeColorScheme();
		Globals.gameState = State.game;
		if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Belarusian || Application.systemLanguage != SystemLanguage.Ukrainian)
		{
		}
	}

	public static void SavePoints()
	{
		PlayerPrefs.SetInt("PlayerPoints", Globals.playerPoints);
		PlayerPrefs.SetInt("PlayerBest", Globals.playerBest);
		PlayerPrefs.SetString("Hash", Sys.MD5(string.Concat(new string[]
		{
			Globals.currentLevel.ToString(),
			Globals.playerPoints.ToString(),
			Globals.playerBest.ToString(),
			Sys.BoolToInt(Globals.noAds).ToString(),
			"Nenavizhu_Blyat'_Chiterov_e28f45g6^$%ef2F3g^&j8JS#$Twf#$%^$H&*jj6ijuYHerferwdf436g56*67876Yr34%^$%^&765JUIIJ*78j$%^^G4%F4&H5yH^"
		})));
	}

	public static void SaveLevel()
	{
		PlayerPrefs.SetInt("CurrentLevel", Globals.currentLevel);
		PlayerPrefs.SetString("Hash", Sys.MD5(string.Concat(new string[]
		{
			Globals.currentLevel.ToString(),
			Globals.playerPoints.ToString(),
			Globals.playerBest.ToString(),
			Sys.BoolToInt(Globals.noAds).ToString(),
			"Nenavizhu_Blyat'_Chiterov_e28f45g6^$%ef2F3g^&j8JS#$Twf#$%^$H&*jj6ijuYHerferwdf436g56*67876Yr34%^$%^&765JUIIJ*78j$%^^G4%F4&H5yH^"
		})));
	}

	public static void SaveNoAds()
	{
		PlayerPrefs.SetInt("NoAds", 1);
		PlayerPrefs.SetString("Hash", Sys.MD5(string.Concat(new string[]
		{
			Globals.currentLevel.ToString(),
			Globals.playerPoints.ToString(),
			Globals.playerBest.ToString(),
			Sys.BoolToInt(Globals.noAds).ToString(),
			"Nenavizhu_Blyat'_Chiterov_e28f45g6^$%ef2F3g^&j8JS#$Twf#$%^$H&*jj6ijuYHerferwdf436g56*67876Yr34%^$%^&765JUIIJ*78j$%^^G4%F4&H5yH^"
		})));
	}

	private void ResetSettings()
	{
		PlayerPrefs.SetInt("CurrentLevel", 1);
		PlayerPrefs.SetInt("PlayerPoints", 0);
		PlayerPrefs.SetInt("PlayerBest", 0);
		PlayerPrefs.SetInt("NoAds", 0);
		Globals.currentLevel = 1;
		Globals.playerPoints = 0;
		Globals.playerBest = 0;
		Globals.noAds = false;
		PlayerPrefs.SetString("Hash", Sys.MD5(string.Concat(new string[]
		{
			Globals.currentLevel.ToString(),
			Globals.playerPoints.ToString(),
			Globals.playerBest.ToString(),
			Sys.BoolToInt(Globals.noAds).ToString(),
			"Nenavizhu_Blyat'_Chiterov_e28f45g6^$%ef2F3g^&j8JS#$Twf#$%^$H&*jj6ijuYHerferwdf436g56*67876Yr34%^$%^&765JUIIJ*78j$%^^G4%F4&H5yH^"
		})));
		MonoBehaviour.print("FAIL! Settings Restored");
	}
}
