using TMPro;
using UnityEngine;
using YG; // Обязательно подключаем Яндекс

namespace AmNuamRunner
{
    public class UpgradeShop : MonoBehaviour
    {
        [SerializeField] private int _money;
        [SerializeField] private TextMeshProUGUI _textMoney;
        [SerializeField] private BuyUpgrade[] _sales;

        private void Start()
        {
            foreach (var slot in _sales)
            {
                slot.Initialize();
                slot.GetButton.onClick.AddListener(UpdateMoney);
            }
            
            UpdateMoney();
        }

        private void OnEnable()
        {
            Upgrades.OnUpgradeChanged += RefreshAllSlots;
            BuyUpgrade.OnSkinChanged += RefreshAllSlots;
            // При включении магазина всегда подтягиваем актуальный баланс из облака
            if (YG2.isSDKEnabled) 
            {
                UpdateMoney();
            }
        }

        private void OnDisable()
        {
            Upgrades.OnUpgradeChanged -= RefreshAllSlots;
            BuyUpgrade.OnSkinChanged -= RefreshAllSlots;
        }

        private void RefreshAllSlots()
        {
            foreach (var slot in _sales)
            {
                slot.Initialize();
            }
            UpdateMoney(); 
        }

        public void UpdateMoney()
        {
            // 1. Берем монеты напрямую из облака Яндекса
            _money = YG2.saves.coin; 
            
            _textMoney.text = _money.ToString();
            
            // 2. Обновляем доступность кнопок в зависимости от баланса
            foreach(var slot in _sales)
            {
                slot.CheckCost(_money);
            }
        }

        // Вызывается при нажатии кнопки "Купить"
        public void Buy(UpgradeAsset upgradeAsset)
        {
            int currentLevel = Upgrades.GetUpgradeLevel(upgradeAsset);
            
            // Если максимальный уровень достигнут, выходим
            if (currentLevel >= upgradeAsset.MaxLevel) return;

            // Узнаем цену текущего уровня апгрейда
            int cost = upgradeAsset.costByLevel[currentLevel];

            if (_money >= cost)
            {
                // Списываем деньги и обновляем облачную переменную
                YG2.saves.coin -= cost;
                
                // Выдаем апгрейд (внутри твоего метода BuyUpgrade уже вызывается YG2.SaveProgress())
                Upgrades.BuyUpgrade(upgradeAsset); 
                
                // Обновляем UI
                UpdateMoney();
            }
        }
    }
}