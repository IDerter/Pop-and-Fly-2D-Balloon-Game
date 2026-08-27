using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Components;
using DG.Tweening; 
using YG; // Обязательно подключаем Яндекс

namespace AmNuamRunner
{
    public class BuyUpgrade : MonoBehaviour
    {
        public static event System.Action OnSkinChanged;

        [SerializeField] private UpgradeAsset _asset;
        public UpgradeAsset GetUpgradeAsset {get {return _asset; }}

        [Header("Визуал")]
        [SerializeField] private Image _upgradeIcon;
        [SerializeField] private LocalizeStringEvent _nameLocalizer;
        [SerializeField] private LocalizeStringEvent _descriptionLocalizer;

        [Header("Блокировка (Замок)")]
        [SerializeField] private UpgradeAsset _requiredUpgrade;
        [SerializeField] private GameObject _lockOverlay;
        [SerializeField] private int _unlockLevel = 2; 
        [SerializeField] private LocalizeStringEvent _lockedMessageLocalizer; 

        [Header("Прогресс")]
        [SerializeField] private Image[] _pips;
        [SerializeField] private Color _colorBought = Color.yellow;
        [SerializeField] private Color _colorEmpty = new Color(0.3f, 0.3f, 0.3f, 1f);

        [Header("Кнопка")]
        [SerializeField] private Button _buttonBuy;
        [SerializeField] private TextMeshProUGUI _textCost;
        [SerializeField] private GameObject[] _objectsToHideOnMax;
        [SerializeField] private LocalizeStringEvent _buttonTextLocalizer;

        private const string TABLE_NAME = "UI_Text";
        private const string KEY_IMPROVE = "shop_buy";   
        private const string KEY_BUY = "Buy";   
        private const string KEY_MAX = "shop_max";   
        private const string KEY_BOUGHT = "shop_bought"; 
        private const string KEY_LOCKED = "shop_locked"; 
        
        // --- НОВЫЕ КЛЮЧИ ДЛЯ СКИНОВ ---
        private const string KEY_SELECT = "shop_select";   // "Выбрать"
        private const string KEY_SELECTED = "shop_selected"; // "Выбрано"

        public Button GetButton => _buttonBuy;
        private int _costNumber;
        
        private Tween _lockMessageTween; 

        // Метод, который определяет, является ли этот товар скином, и возвращает его ID
        private string GetSkinId(string assetName)
        {
            if (assetName == "RedDragonSkin") return "RedDragon";
            if (assetName == "BlueDragonSkin") return "BlueDragon";
            if (assetName == "LickSkin") return "Lick";
            return null; // Если это не скин, возвращаем null
        }

        public void Initialize()
        {
            if (_asset == null) return;

            if (_upgradeIcon != null) _upgradeIcon.sprite = _asset.sprite;
            if (_nameLocalizer != null) _nameLocalizer.StringReference = _asset.localizedName;

            int savedLevel = Upgrades.GetUpgradeLevel(_asset);
            string skinId = GetSkinId(_asset.name);
            
            // ХИТРОСТЬ: Красный дракон всегда куплен!
            if (skinId == "RedDragon") savedLevel = 1;

            if (_descriptionLocalizer != null)
            {
                _descriptionLocalizer.StringReference = _asset.localizedDescription;
                int totalBonus = savedLevel * _asset.step;
                _descriptionLocalizer.RefreshString(); 
                _descriptionLocalizer.StringReference.Arguments = new object[] { _asset.step, totalBonus };
                _descriptionLocalizer.RefreshString();
            }

            if (_pips != null)
            {
                for (int i = 0; i < _pips.Length; i++)
                {
                    if (i >= _asset.MaxLevel) _pips[i].gameObject.SetActive(false);
                    else
                    {
                        _pips[i].gameObject.SetActive(true);
                        _pips[i].color = i < savedLevel ? _colorBought : _colorEmpty;
                    }
                }
            }

            bool isUnlocked = _requiredUpgrade == null || Upgrades.GetUpgradeLevel(_requiredUpgrade) > 0;

            if (_lockOverlay != null) _lockOverlay.SetActive(!isUnlocked);
            if (_lockedMessageLocalizer != null) _lockedMessageLocalizer.gameObject.SetActive(false);

            if (!isUnlocked)
            {
                _buttonBuy.interactable = false;
                _costNumber = int.MaxValue; 
                if (_textCost != null) _textCost.text = "-";
            }
            else if (savedLevel >= _asset.MaxLevel && !_asset.IsConsumable)
            {
                // Товар полностью куплен! 
                // Проверяем: это обычное улучшение или скин?
                if (!string.IsNullOrEmpty(skinId))
                {
                    // ЭТО СКИН
                    string currentSkin = string.IsNullOrEmpty(YG2.saves.currentSkin) ? "RedDragon" : YG2.saves.currentSkin;
                    
                    if (currentSkin == skinId)
                    {
                        // Скин сейчас надет
                        _buttonBuy.interactable = false;
                        if (_buttonTextLocalizer != null) _buttonTextLocalizer.StringReference.SetReference(TABLE_NAME, KEY_SELECTED);
                    }
                    else
                    {
                        // Скин куплен, но не надет (Можно выбрать)
                        _buttonBuy.interactable = true;
                        if (_buttonTextLocalizer != null) _buttonTextLocalizer.StringReference.SetReference(TABLE_NAME, KEY_SELECT);
                    }
                }
                else
                {
                    // ЭТО ОБЫЧНОЕ УЛУЧШЕНИЕ (Максимальный уровень)
                    _buttonBuy.interactable = false;
                    string keyToUse = _asset.IsInApp ? KEY_BOUGHT : KEY_MAX;
                    if (_buttonTextLocalizer != null) _buttonTextLocalizer.StringReference.SetReference(TABLE_NAME, keyToUse);
                }

                _costNumber = int.MaxValue;
                if (_textCost != null) _textCost.text = "";

                foreach (var obj in _objectsToHideOnMax)
                {
                    if (obj != null) obj.SetActive(false);
                }
            }
            else
            {
                // ТОВАР ЕЩЕ НЕ КУПЛЕН (ИЛИ МОЖНО ПРОКАЧАТЬ ДАЛЬШЕ)
                string keyToUse;
                if (_asset.IsInApp) keyToUse = KEY_BUY;
                else if (savedLevel == 0) keyToUse = KEY_BUY; 
                else keyToUse = KEY_IMPROVE;

                if (_buttonTextLocalizer != null) _buttonTextLocalizer.StringReference.SetReference(TABLE_NAME, keyToUse);

                foreach (var obj in _objectsToHideOnMax)
                {
                    if (obj != null) obj.SetActive(true);
                }

                if (_asset.IsInApp)
                {
                    _buttonBuy.interactable = true;
                }
                else
                {
                    _costNumber = _asset.costByLevel[savedLevel];
                    if (_textCost != null) _textCost.text = _costNumber.ToString();
                }
            }
        }

        public void OnLockedOverlayClicked()
        {
            if (_lockedMessageLocalizer != null)
            {
                _lockedMessageLocalizer.gameObject.SetActive(true);
                _lockedMessageLocalizer.StringReference.SetReference(TABLE_NAME, KEY_LOCKED);
                _lockedMessageLocalizer.StringReference.Arguments = new object[] { _unlockLevel };
                _lockedMessageLocalizer.RefreshString();
                
                _lockMessageTween?.Kill(); 
                _lockMessageTween = DOVirtual.DelayedCall(2f, () => 
                {
                    if (this != null && _lockedMessageLocalizer != null)
                        _lockedMessageLocalizer.gameObject.SetActive(false);
                }).SetUpdate(true);
            }
        }

        public void CheckCost(int money)
        {
            if (_asset == null) return;

            bool isUnlocked = _requiredUpgrade == null || Upgrades.GetUpgradeLevel(_requiredUpgrade) > 0;
            int savedLevel = Upgrades.GetUpgradeLevel(_asset);
            string skinId = GetSkinId(_asset.name);
            if (skinId == "RedDragon") savedLevel = 1;

            if (!isUnlocked)
            {
                _buttonBuy.interactable = false;
                return;
            }

            if (!string.IsNullOrEmpty(skinId) && savedLevel >= _asset.MaxLevel)
            {
                string currentSkin = string.IsNullOrEmpty(YG2.saves.currentSkin) ? "RedDragon" : YG2.saves.currentSkin;
                _buttonBuy.interactable = (currentSkin != skinId);
                return;
            }

            // Блокируем кнопку только если это НЕ многоразовый товар
            if (savedLevel >= _asset.MaxLevel && !_asset.IsConsumable) 
            {
                _buttonBuy.interactable = false;
                return;
            }

            // Для Инапов кнопка должна быть активна всегда, а для игровых покупок проверяем баланс
            if (_asset.IsInApp) 
            {
                _buttonBuy.interactable = true; 
            }
            else
            {
                _buttonBuy.interactable = money >= _costNumber;
            }
        }

        public void Buy()
        {
            string skinId = GetSkinId(_asset.name);
            int currentLevel = Upgrades.GetUpgradeLevel(_asset);
            if (skinId == "RedDragon") currentLevel = 1;

            // СЦЕНАРИЙ 1: Игрок нажимает "Выбрать" на уже купленном скине
            if (currentLevel >= _asset.MaxLevel && !string.IsNullOrEmpty(skinId))
            {
                YG2.saves.currentSkin = skinId;
                YG2.SaveProgress();
                
                Sound.EquipSound.Play(); 
                
                OnSkinChanged?.Invoke(); 
                return;
            }

            // СЦЕНАРИЙ 2: Игрок покупает товар
            if (currentLevel >= _asset.MaxLevel && !_asset.IsConsumable) return;
            
            // Инапы (за реальные деньги) обрабатываются через IAPHandler, их пропускаем
            if (_asset.IsInApp) return; 

            // --- ДОБАВЛЕНО: Проверяем баланс и списываем монеты ---
            if (YG2.saves.coin >= _costNumber)
            {
                YG2.saves.coin -= _costNumber;
                
                // Upgrades.BuyUpgrade внутри себя вызывает YG2.SaveProgress(), 
                // так что новый баланс монет автоматически улетит в облако!
                Upgrades.BuyUpgrade(_asset);

                // Если купили новый скин, сразу экипируем его
                if (!string.IsNullOrEmpty(skinId))
                {
                    YG2.saves.currentSkin = skinId;
                    YG2.SaveProgress();
                    OnSkinChanged?.Invoke(); 
                    
                    // Если хочешь, чтобы при ПОКУПКЕ скина тоже играл звук экипировки, 
                    // можешь раскомментировать строку ниже (но тогда она проиграет вместе со звуком покупки)
                    // Sound.EquipSound.Play(); 
                }
                else
                {
                    Initialize();
                }

                int savedLevel = Upgrades.GetUpgradeLevel(_asset);
                AnalyticsManager.Instance.SaveShopBuy(_asset.name, savedLevel);
                
                // Звук покупки
                Sound.BuySound.Play();
            }
        }
        
        private void OnDestroy()
        {
            _lockMessageTween?.Kill();
        }
    }
}
