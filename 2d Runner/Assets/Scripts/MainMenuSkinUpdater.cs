using UnityEngine;
using UnityEngine.UI;
using YG; // Подключаем Яндекс

namespace AmNuamRunner 
{
    public class MainMenuSkinUpdater : MonoBehaviour
    {
        [Header("Components")]
        public Image playerImage; 
        
        public Sprite redDragonSprite; // Сюда перетащите картинку Ам Няма
        public Vector3 redDragonScale = Vector3.one; // Базовый масштаб (1, 1, 1)

        public Sprite blueDragonSprite; // Сюда перетащите картинку Буки
        public Vector3 blueDragonScale = new Vector3(1.2f, 1.2f, 1f); 

        private void Start()
        {
            ApplyMenuSkin();
        }

        // Подписываемся на событие при включении объекта
        private void OnEnable()
        {
            // Подставьте AmNuamRunner.Upgrades.OnUpgradeChanged, если скрипт не в namespace
            AmNuamRunner.BuyUpgrade.OnSkinChanged += ApplyMenuSkin;
        }

        // Обязательно отписываемся при выключении/удалении, чтобы не было утечек памяти
        private void OnDisable()
        {
            AmNuamRunner.BuyUpgrade.OnSkinChanged -= ApplyMenuSkin;
        }

        private void ApplyMenuSkin()
        {
            // Проверяем, загрузились ли сохранения
            if (!YG2.isSDKEnabled) return; 

            string currentSkin = string.IsNullOrEmpty(YG2.saves.currentSkin) ? "RedDragon" : YG2.saves.currentSkin;

            if (currentSkin == "BlueDragon")
            {
                if (playerImage != null) playerImage.sprite = blueDragonSprite;
                transform.localScale = blueDragonScale; // Увеличиваем масштаб для Буки
            }
            else
            {
                // По умолчанию (Красный дракон)
                if (playerImage != null) playerImage.sprite = redDragonSprite;
                transform.localScale = redDragonScale; // Возвращаем стандартный масштаб
            }
        }
    }
}