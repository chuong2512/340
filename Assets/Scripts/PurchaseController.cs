using System;
using UnityEngine;
using UnityEngine.Purchasing;

public class PurchaseController : MonoBehaviour
{
	private void Awake()
	{
		PurchaseManager.OnPurchaseNonConsumable += new PurchaseManager.OnSuccessNonConsumable(this.PurchaseManager_OnPurchaseNonConsumable);
		PurchaseManager.OnPurchaseConsumable += new PurchaseManager.OnSuccessConsumable(this.PurchaseManager_OnPurchaseConsumable);
		PurchaseManager.PurchaseFailed += new PurchaseManager.OnFailedPurchase(this.PurchaseManager_PurchaseFailed);
	}

	private void Update()
	{
	}

	private void PurchaseManager_PurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
	}

	private void PurchaseManager_OnPurchaseConsumable(PurchaseEventArgs args)
	{
	}

	private void PurchaseManager_OnPurchaseNonConsumable(PurchaseEventArgs args)
	{
		if (args.purchasedProduct.definition.id.Equals("noads"))
		{
			Globals.noAds = true;
			Globals.SaveNoAds();
            AdsControl.Instance.HideBanner();
			Globals.menu.btnNoAds.gameObject.SetActive(false);
			if (Globals.menu.btnRewarded.gameObject.activeSelf)
			{
				Globals.menu.btnRewarded.gameObject.SetActive(false);
				Globals.menu.btnContinueNoAds.gameObject.SetActive(true);
			}
		}
	}

	public void CheckBuyState(string id)
	{
		if (PurchaseManager.CheckBuyState(id))
		{
			Globals.noAds = true;
			Globals.SaveNoAds();
			Globals.menu.btnNoAds.gameObject.SetActive(false);
		}
	}
}
