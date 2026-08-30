using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

namespace AmNuamRunner
{

	public class PurchaseManager : SingletonBase<PurchaseManager>
	{
		public static event Action<string> PurchaseOn;
		private void Start()
		{
			Debug.Log(YG2.purchases[0].priceCurrencyCode.ToString());
		}

		private void OnEnable()
		{
			YG2.onPurchaseSuccess += SuccessPurchased;
			YG2.onPurchaseFailed += FailedPurchased;
		}

		private void OnDisable()
		{
			YG2.onPurchaseSuccess -= SuccessPurchased;
			YG2.onPurchaseFailed -= FailedPurchased;
		}

		private void SuccessPurchased(string id)
		{
			//if (id == TypeReward.X2CoinsAndHearts.ToString())
			//{
			//	PurchaseOn?.Invoke(TypeReward.X2CoinsAndHearts.ToString());

				//YG2.ConsumePurchase(id);
			//}
		}

		private void FailedPurchased(string id)
		{
			// ������� �� ���� ���������
		}
	}
}