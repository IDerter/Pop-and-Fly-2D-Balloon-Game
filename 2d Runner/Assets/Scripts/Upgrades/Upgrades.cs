using UnityEngine;
using System;
using YG;

namespace AmNuamRunner
{
    public class Upgrades : SingletonBase<Upgrades>
    {
        public static event Action OnUpgradeChanged;

        [Serializable]
        private class UpgradeSave
        {
            public UpgradeAsset asset;
            public int level = 0;
        }

        [SerializeField] private UpgradeSave[] _upgrades;

        private new void Awake()
        {
            base.Awake();
        }

        private void OnEnable()
        {
            YG2.onGetSDKData += LoadData;
        }

        private void OnDisable()
        {
            YG2.onGetSDKData -= LoadData;
        }

        private void Start()
        {
            if (YG2.isSDKEnabled)
            {
                LoadData();
            }
        }

        public void LoadData()
        {
            if (YG2.saves.upgradeNames != null && YG2.saves.upgradeNames.Length > 0)
            {
                for (int i = 0; i < _upgrades.Length; i++)
                {
                    int index = Array.IndexOf(YG2.saves.upgradeNames, _upgrades[i].asset.name);
                    if (index != -1)
                    {
                        _upgrades[i].level = YG2.saves.upgradeLevels[index];
                    }
                }
            }
        }

        public static void BuyUpgrade(UpgradeAsset asset)
        {
            foreach (var upgrade in Instance._upgrades)
            {
                if (upgrade.asset == asset)
                {
                    upgrade.level += 1;
                    SaveToCloud();
                    
                    // Убрал ссылку на AnalyticsManager, чтобы не было ошибок, если его нет. 
                    // Верни, если он реально существует в твоем проекте.
                    
                    OnUpgradeChanged?.Invoke(); 
                    break;
                }
            }
        }

        private static void SaveToCloud()
        {
            YG2.saves.upgradeNames = new string[Instance._upgrades.Length];
            YG2.saves.upgradeLevels = new int[Instance._upgrades.Length];

            for (int i = 0; i < Instance._upgrades.Length; i++)
            {
                YG2.saves.upgradeNames[i] = Instance._upgrades[i].asset.name;
                YG2.saves.upgradeLevels[i] = Instance._upgrades[i].level;
            }

            YG2.SaveProgress();
        }

        public static int GetUpgradeLevel(UpgradeAsset asset)
        {
            foreach (var upgrade in Instance._upgrades)
            {
                if (upgrade.asset == asset)
                {
                    return upgrade.level;
                }
            }
            return 0;
        }
        
        [ContextMenu("Очистить сохранения апгрейдов (Тест)")]
        public void ResetAllUpgradesTest()
        {
            // 1. Сбрасываем уровни локально в скрипте
            foreach (var upgrade in _upgrades)
            {
                upgrade.level = 0;
            }

            // 2. Очищаем массивы в облачном сохранении Яндекса
            YG2.saves.upgradeNames = new string[0];
            YG2.saves.upgradeLevels = new int[0];

            // Если для тестов нужно заодно сбрасывать монеты и скин — раскомментируй эти строки:
            // YG2.saves.coin = 0;
            // YG2.saves.currentSkin = "OmNom"; // Или базовый скин "RedDragon"

            // 3. Отправляем пустые данные в облако
            YG2.SaveProgress();

            // 4. Обновляем UI магазина (если он сейчас открыт)
            OnUpgradeChanged?.Invoke();

            Debug.Log("<color=yellow>[Тест] Все улучшения успешно сброшены!</color>");
        }
    }
}