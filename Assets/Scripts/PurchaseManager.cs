using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class PurchaseManager : MonoBehaviour, IStoreListener
{
	public delegate void OnSuccessConsumable(PurchaseEventArgs args);

	public delegate void OnSuccessNonConsumable(PurchaseEventArgs args);

	public delegate void OnFailedPurchase(Product product, PurchaseFailureReason failureReason);

	private static IStoreController m_StoreController;

	private static IExtensionProvider m_StoreExtensionProvider;

	private int currentProductIndex;

	[Tooltip("Не многоразовые товары. Больше подходит для отключения рекламы и т.п.")]
	public string[] NC_PRODUCTS;

	[Tooltip("Многоразовые товары. Больше подходит для покупки игровой валюты и т.п.")]
	public string[] C_PRODUCTS;







	public static event PurchaseManager.OnSuccessConsumable OnPurchaseConsumable;

	public static event PurchaseManager.OnSuccessNonConsumable OnPurchaseNonConsumable;

	public static event PurchaseManager.OnFailedPurchase PurchaseFailed;

	private void Awake()
	{
		this.InitializePurchasing();
	}

	public static bool CheckBuyState(string id)
	{
		Product product = PurchaseManager.m_StoreController.products.WithID(id);
		return product.hasReceipt;
	}

	public void InitializePurchasing()
	{
		ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(), new IPurchasingModule[0]);
		string[] c_PRODUCTS = this.C_PRODUCTS;
		for (int i = 0; i < c_PRODUCTS.Length; i++)
		{
			string id = c_PRODUCTS[i];
			configurationBuilder.AddProduct(id, ProductType.Consumable);
		}
		string[] nC_PRODUCTS = this.NC_PRODUCTS;
		for (int j = 0; j < nC_PRODUCTS.Length; j++)
		{
			string id2 = nC_PRODUCTS[j];
			configurationBuilder.AddProduct(id2, ProductType.NonConsumable);
		}
		UnityPurchasing.Initialize(this, configurationBuilder);
	}

	private bool IsInitialized()
	{
		return PurchaseManager.m_StoreController != null && PurchaseManager.m_StoreExtensionProvider != null;
	}

	public void BuyConsumable(int index)
	{
		this.currentProductIndex = index;
		this.BuyProductID(this.C_PRODUCTS[index]);
	}

	public void BuyNonConsumable(int index)
	{
		this.currentProductIndex = index;
		this.BuyProductID(this.NC_PRODUCTS[index]);
	}

	private void BuyProductID(string productId)
	{
		if (this.IsInitialized())
		{
			Product product = PurchaseManager.m_StoreController.products.WithID(productId);
			if (product != null && product.availableToPurchase)
			{
				UnityEngine.Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
				PurchaseManager.m_StoreController.InitiatePurchase(product);
			}
			else
			{
				UnityEngine.Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
				this.OnPurchaseFailed(product, PurchaseFailureReason.ProductUnavailable);
			}
		}
	}

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		UnityEngine.Debug.Log("OnInitialized: PASS");
		PurchaseManager.m_StoreController = controller;
		PurchaseManager.m_StoreExtensionProvider = extensions;
	}

	public void OnInitializeFailed(InitializationFailureReason error)
	{
		UnityEngine.Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}

	public void OnInitializeFailed(InitializationFailureReason error, string message)
	{
		
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
	{
		if (this.C_PRODUCTS.Length > 0 && string.Equals(args.purchasedProduct.definition.id, this.C_PRODUCTS[this.currentProductIndex], StringComparison.Ordinal))
		{
			this.OnSuccessC(args);
		}
		else if (this.NC_PRODUCTS.Length > 0 && string.Equals(args.purchasedProduct.definition.id, this.NC_PRODUCTS[this.currentProductIndex], StringComparison.Ordinal))
		{
			this.OnSuccessNC(args);
		}
		else
		{
			UnityEngine.Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
		}
		return PurchaseProcessingResult.Complete;
	}

	protected virtual void OnSuccessC(PurchaseEventArgs args)
	{
		if (PurchaseManager.OnPurchaseConsumable != null)
		{
			PurchaseManager.OnPurchaseConsumable(args);
		}
		UnityEngine.Debug.Log(this.C_PRODUCTS[this.currentProductIndex] + " Buyed!");
	}

	protected virtual void OnSuccessNC(PurchaseEventArgs args)
	{
		if (PurchaseManager.OnPurchaseNonConsumable != null)
		{
			PurchaseManager.OnPurchaseNonConsumable(args);
		}
		UnityEngine.Debug.Log(this.NC_PRODUCTS[this.currentProductIndex] + " Buyed!");
	}

	protected virtual void OnFailedP(Product product, PurchaseFailureReason failureReason)
	{
		if (PurchaseManager.PurchaseFailed != null)
		{
			PurchaseManager.PurchaseFailed(product, failureReason);
		}
		UnityEngine.Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}

	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		this.OnFailedP(product, failureReason);
	}
}
