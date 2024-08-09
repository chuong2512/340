
using System;
using UnityEngine;

public class AdsController : MonoBehaviour
{

	private float deltaTime;

	private static string outputMessage = string.Empty;

	private int oldGameState;

	private float timerInter;

	private bool oldNoAds;

	private bool isRequested;

	private int counter;

	private int nextCounter;

	public bool skipRewarded;

	public bool forceFlag;

    public string IOSInterestitalKey, IOSBannerKey, AndroidInterestitalKey, AndroidBannerKey, IOSRewardedVideoKey, AndroidRewardedVideoKey,AndroidAppID,IOSAppID;


    public static string OutputMessage
	{
		set
		{
			AdsController.outputMessage = value;
		}
	}
    /*
	public void Awake()
	{
	}

	public void Start()
	{
		string appId = "";
#if UNITY_ANDROID
        appId = AndroidAppID;
#endif

#if UNITY_IOS
        appId = IOSAppID;
#endif

        MobileAds.SetiOSAppPauseOnBackground(true);
		MobileAds.Initialize(appId);
		this.rewardBasedVideo = RewardBasedVideoAd.Instance;
		this.rewardBasedVideo.OnAdLoaded += new EventHandler<EventArgs>(this.HandleRewardBasedVideoLoaded);
		this.rewardBasedVideo.OnAdFailedToLoad += new EventHandler<AdFailedToLoadEventArgs>(this.HandleRewardBasedVideoFailedToLoad);
		this.rewardBasedVideo.OnAdOpening += new EventHandler<EventArgs>(this.HandleRewardBasedVideoOpened);
		this.rewardBasedVideo.OnAdStarted += new EventHandler<EventArgs>(this.HandleRewardBasedVideoStarted);
		this.rewardBasedVideo.OnAdRewarded += new EventHandler<Reward>(this.HandleRewardBasedVideoRewarded);
		this.rewardBasedVideo.OnAdClosed += new EventHandler<EventArgs>(this.HandleRewardBasedVideoClosed);
		this.rewardBasedVideo.OnAdLeavingApplication += new EventHandler<EventArgs>(this.HandleRewardBasedVideoLeftApplication);
		this.oldGameState = Globals.gameState;
		if (!Globals.noAds)
		{
			this.RequestBanner();
			this.RequestInterstitial();
			this.RequestRewardBasedVideo();
			this.isRequested = true;
		}
		this.timerInter = Time.time + 180f;
		this.nextCounter = 3;
	}

	public void Update()
	{
		this.deltaTime += (Time.deltaTime - this.deltaTime) * 0.1f;
		if (!Globals.noAds)
		{
			if (this.oldGameState != Globals.gameState)
			{
				if (Globals.gameState == State.gameOver)
				{
					if (this.rewardBasedVideo.IsLoaded() && !this.skipRewarded)
					{
						Globals.menu.btnRewarded.gameObject.SetActive(true);
					}
					else
					{
						Globals.menu.btnRewarded.gameObject.SetActive(false);
					}
				}
				else if (Globals.gameState == State.game)
				{
					if (!this.rewardBasedVideo.IsLoaded())
					{
						this.RequestRewardBasedVideo();
					}
					if (this.counter == this.nextCounter || Time.time > this.timerInter || this.forceFlag)
					{
						this.ShowInterstitial();
						this.RequestInterstitial();
						this.timerInter = Time.time + UnityEngine.Random.Range(30f, 40f);
						this.nextCounter = this.counter + UnityEngine.Random.Range(5, 10);
						this.forceFlag = false;
					}
					this.counter++;
				}
				this.oldGameState = Globals.gameState;
			}
		}
		else if (!this.oldNoAds)
		{
			this.oldNoAds = true;
			if (this.isRequested)
			{
				this.HideBanner();
				this.bannerView.Destroy();
			}
		}
	}

	private AdRequest CreateAdRequest()
	{
		return new AdRequest.Builder().AddTestDevice("SIMULATOR").AddTestDevice("0123456789ABCDEF0123456789ABCDEF").AddKeyword("game").SetGender(Gender.Male).SetBirthday(new DateTime(1985, 1, 1)).TagForChildDirectedTreatment(false).AddExtra("color_bg", "9B30FF").Build();
	}

	private void RequestBanner()
	{
        string adUnitId = "";


#if UNITY_ANDROID
        adUnitId =  AndroidBannerKey;
#endif

#if UNITY_IOS
        adUnitId = IOSBannerKey;
#endif

        if (this.bannerView != null)
		{
			this.bannerView.Destroy();
		}
		this.bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
		this.bannerView.OnAdLoaded += new EventHandler<EventArgs>(this.HandleAdLoaded);
		this.bannerView.OnAdFailedToLoad += new EventHandler<AdFailedToLoadEventArgs>(this.HandleAdFailedToLoad);
		this.bannerView.OnAdOpening += new EventHandler<EventArgs>(this.HandleAdOpened);
		this.bannerView.OnAdClosed += new EventHandler<EventArgs>(this.HandleAdClosed);
		this.bannerView.OnAdLeavingApplication += new EventHandler<EventArgs>(this.HandleAdLeftApplication);
		this.bannerView.LoadAd(this.CreateAdRequest());
	}

	private void RequestInterstitial()
	{
		string adUnitId = "";


#if UNITY_ANDROID
        adUnitId =  AndroidInterestitalKey;
#endif

#if UNITY_IOS
        adUnitId = IOSInterestitalKey;
#endif

        if (this.interstitial != null)
		{
			this.interstitial.Destroy();
		}
		this.interstitial = new InterstitialAd(adUnitId);
		this.interstitial.OnAdLoaded += new EventHandler<EventArgs>(this.HandleInterstitialLoaded);
		this.interstitial.OnAdFailedToLoad += new EventHandler<AdFailedToLoadEventArgs>(this.HandleInterstitialFailedToLoad);
		this.interstitial.OnAdOpening += new EventHandler<EventArgs>(this.HandleInterstitialOpened);
		this.interstitial.OnAdClosed += new EventHandler<EventArgs>(this.HandleInterstitialClosed);
		this.interstitial.OnAdLeavingApplication += new EventHandler<EventArgs>(this.HandleInterstitialLeftApplication);
		this.interstitial.LoadAd(this.CreateAdRequest());
	}

	private void UpdateIterstitial()
	{
		if (this.counter == this.nextCounter || Time.time > this.timerInter)
		{
			this.ShowInterstitial();
			this.RequestInterstitial();
			this.timerInter = Time.time + 60f;
			this.nextCounter = this.counter + 3;
		}
		this.counter++;
	}

	private void RequestRewardBasedVideo()
	{
        string adUnitId = "";


#if UNITY_ANDROID
        adUnitId = AndroidRewardedVideoKey;
#endif

#if UNITY_IOS
        adUnitId = IOSRewardedVideoKey;
#endif
        this.rewardBasedVideo.LoadAd(this.CreateAdRequest(), adUnitId);
	}

	private void ShowBanner()
	{
		this.bannerView.Show();
	}

	private void HideBanner()
	{
		this.bannerView.Hide();
	}

	private void ShowInterstitial()
	{
		if (this.interstitial.IsLoaded())
		{
			this.interstitial.Show();
		}
		else
		{
			MonoBehaviour.print("Interstitial is not ready yet");
		}
	}

	public void ShowRewardBasedVideo()
	{
		if (this.rewardBasedVideo.IsLoaded())
		{
			this.rewardBasedVideo.Show();
		}
		else
		{
			MonoBehaviour.print("Reward based video ad is not ready yet");
		}
	}

	public void HandleAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLoaded event received");
	}

	public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
	}

	public void HandleAdOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdOpened event received");
	}

	public void HandleAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdClosed event received");
	}

	public void HandleAdLeftApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLeftApplication event received");
	}

	public void HandleInterstitialLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleInterstitialLoaded event received");
	}

	public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleInterstitialFailedToLoad event received with message: " + args.Message);
	}

	public void HandleInterstitialOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleInterstitialOpened event received");
	}

	public void HandleInterstitialClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleInterstitialClosed event received");
	}

	public void HandleInterstitialLeftApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleInterstitialLeftApplication event received");
	}

	public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
	}

	public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
	}

	public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
	}

	public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
	}

	public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
		Globals.menu.btnRewarded.gameObject.SetActive(false);
		this.skipRewarded = true;
	}

	public void HandleRewardBasedVideoRewarded(object sender, Reward args)
	{
		string type = args.Type;
		MonoBehaviour.print("HandleRewardBasedVideoRewarded event received for " + args.Amount.ToString() + " " + type);
		this.timerInter = Time.time + UnityEngine.Random.Range(30f, 40f);
		this.nextCounter = this.counter + UnityEngine.Random.Range(5, 10);
		Globals.menu.StartFromControlPoint();
		Globals.menu.btnRewarded.gameObject.SetActive(false);
		this.skipRewarded = true;
	}

	public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
	}
	*/
}
