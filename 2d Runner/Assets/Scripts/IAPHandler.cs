using UnityEngine;
using YG;

namespace AmNuamRunner
{
    public class IAPHandler : SingletonBase<IAPHandler>
    {
        [Header("Донатные товары (Разовые)")]
        [SerializeField] private UpgradeAsset _noAds;
        // Многоразовому инапу на леденцы здесь даже не нужна ссылка, 
        // так как он просто дает валюту, а не прокачивает уровень.

        private new void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            if (_noAds != null && Upgrades.GetUpgradeLevel(_noAds) > 0)
            {
                YG2.StickyAdActivity(false);
                Debug.Log("[IAP] NoAds был куплен ранее. Стики-баннеры отключены на старте.");
            }
        }

        private void OnEnable()
        {
            YG2.onPurchaseSuccess += OnPurchaseSuccess;
        }

        private void OnDisable()
        {
            YG2.onPurchaseSuccess -= OnPurchaseSuccess;
        }

        private void OnPurchaseSuccess(string id)
        {
            // --- МНОГОРАЗОВЫЕ ТОВАРЫ ---
            if (id == "Candies500") // Убедись, что ID совпадает с консолью Яндекса
            {
                YG2.saves.coin += 500; // Начисляем 500 леденцов (монет)
                YG2.SaveProgress();

                // Обновляем UI магазина сразу после покупки, если он открыт
                var shop = FindObjectOfType<UpgradeShop>();
                if (shop != null) shop.UpdateMoney();
            }
            else if (id == "NoAds")
            {
                Upgrades.BuyUpgrade(_noAds); 
                YG2.StickyAdActivity(false);

                //AnalyticsManager.Instance.SaveShopBuy(_asset.name, savedLevel);
            }

            Sound.BuySound.Play();
            
            Debug.Log($"[IAP] Успешно куплен товар: {id}");
        }
    }
}