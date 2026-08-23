using UnityEngine;
using YG;

namespace AmNuamRunner
{
    public class IAPHandler : SingletonBase<IAPHandler>
    {
        [Header("Донатные товары")]
        [SerializeField] private UpgradeAsset _upgradeX2CoinsAndHearts;
        [SerializeField] private UpgradeAsset _stars15;
        [SerializeField] private UpgradeAsset _noAds;

        private new void Awake()
        {
            base.Awake(); // Сохраняем логику синглтона
        }

        private void Start()
        {
            // --- НОВАЯ ЛОГИКА ---
            // Проверяем при старте игры: если игрок УЖЕ покупал NoAds ранее,
            // мы сразу отключаем стики-баннеры, чтобы они даже не появлялись.
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
            if (id == "Stars15")
            {
                // Начисляем звезды (внутри уже есть вызов OnScoreUpdated для UI)
                //MapCompletion.AddBoughtStars(15); 
                
                // Прокачиваем ассет "15 звезд" (чтобы сохранить факт покупки)
                Upgrades.BuyUpgrade(_stars15); 
            }
            else if (id == "X2CoinsAndHearts")
            {
                // Просто вызываем метод покупки в менеджере
                // Внутри него сработает аналитика и обновится UI магазина!
                Upgrades.BuyUpgrade(_upgradeX2CoinsAndHearts); 
            }
            else if (id == "NoAds")
            {
                // Просто вызываем метод покупки в менеджере
                // Внутри него сработает аналитика и обновится UI магазина!
                Upgrades.BuyUpgrade(_noAds); 
                YG2.StickyAdActivity(false);
            }
            
            Debug.Log($"[IAP] Успешно куплен товар: {id}");
        }
    }
}