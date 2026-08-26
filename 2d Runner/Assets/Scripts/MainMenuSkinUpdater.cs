using UnityEngine;
using UnityEngine.UI;
using YG; // Подключаем Яндекс

namespace AmNuamRunner 
{
    public class MainMenuSkinUpdater : MonoBehaviour
    {
        [Header("Components")]
        public Image playerImage; 
        
        public Sprite omNomSprite; // Сюда перетащите картинку Ам Няма
        public Vector3 omNomScale = Vector3.one; // Базовый масштаб (1, 1, 1)

        public Sprite booSprite; // Сюда перетащите картинку Буки
        public Vector3 booScale = new Vector3(1.2f, 1.2f, 1f); 

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

            string currentSkin = string.IsNullOrEmpty(YG2.saves.currentSkin) ? "OmNom" : YG2.saves.currentSkin;

            if (currentSkin == "Boo")
            {
                if (playerImage != null) playerImage.sprite = booSprite;
                transform.localScale = booScale; // Увеличиваем масштаб для Буки
            }
            else
            {
                // По умолчанию (Ам Ням)
                if (playerImage != null) playerImage.sprite = omNomSprite;
                transform.localScale = omNomScale; // Возвращаем стандартный масштаб
            }
        }
    }
}