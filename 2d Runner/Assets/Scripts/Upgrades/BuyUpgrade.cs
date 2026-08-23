using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Components;
using DG.Tweening; 

namespace AmNuamRunner
{
    public class BuyUpgrade : MonoBehaviour
    {
        [SerializeField] private UpgradeAsset _asset;
        public UpgradeAsset GetUpgradeAsset {get {return _asset; }}

        [Header("Визуал")]
        [SerializeField] private Image _upgradeIcon;
        [SerializeField] private LocalizeStringEvent _nameLocalizer;
        [SerializeField] private LocalizeStringEvent _descriptionLocalizer;

        [Header("Блокировка (Замок)")]
        [Tooltip("Ассет, который должен быть прокачан (выдан по сюжету), чтобы открыть этот лот. Если пусто - открыто сразу.")]
        [SerializeField] private UpgradeAsset _requiredUpgrade;
        [Tooltip("Полупрозрачная черная панель с иконкой замка")]
        [SerializeField] private GameObject _lockOverlay;
        [Tooltip("Уровень, после которого открывается улучшение (для отображения в тексте)")]
        [SerializeField] private int _unlockLevel = 2; // <-- Настраиваем уровень прямо в инспекторе карточки
        [Tooltip("Отдельный локализатор текста ошибки, который висит поверх замка")]
        [SerializeField] private LocalizeStringEvent _lockedMessageLocalizer; // <-- Отдельный текст локализации

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
        private const string KEY_LOCKED = "shop_locked"; // <-- Ключ для локализации ошибки

        public Button GetButton => _buttonBuy;
        private int _costNumber;
        
        private Tween _lockMessageTween; 

        public void Initialize()
        {
            if (_asset == null) return;

            if (_upgradeIcon != null) _upgradeIcon.sprite = _asset.sprite;
            if (_nameLocalizer != null) _nameLocalizer.StringReference = _asset.localizedName;

            int savedLevel = Upgrades.GetUpgradeLevel(_asset);

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

            // --- ЛОГИКА БЛОКИРОВКИ ---
            bool isUnlocked = _requiredUpgrade == null || Upgrades.GetUpgradeLevel(_requiredUpgrade) > 0;

            if (_lockOverlay != null)
            {
                _lockOverlay.SetActive(!isUnlocked);
            }
            
            // Скрываем отдельный текст ошибки при старте
            if (_lockedMessageLocalizer != null)
            {
                _lockedMessageLocalizer.gameObject.SetActive(false);
            }

            if (!isUnlocked)
            {
                _buttonBuy.interactable = false;
                _costNumber = int.MaxValue; 
                if (_textCost != null) _textCost.text = "-";
            }
            else if (savedLevel >= _asset.MaxLevel)
            {
                _buttonBuy.interactable = false;
                _costNumber = int.MaxValue;
                if (_textCost != null) _textCost.text = "";

                string keyToUse = _asset.IsInApp ? KEY_BOUGHT : KEY_MAX;
                if (_buttonTextLocalizer != null)
                    _buttonTextLocalizer.StringReference.SetReference(TABLE_NAME, keyToUse);

                foreach (var obj in _objectsToHideOnMax)
                {
                    if (obj != null) obj.SetActive(false);
                }
            }
            else
            {
                if (_buttonTextLocalizer != null)
                    _buttonTextLocalizer.StringReference.SetReference(TABLE_NAME, _asset.IsInApp ? KEY_BUY : KEY_IMPROVE);

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

        // --- МЕТОД ДЛЯ КЛИКА ПО ЗАМКУ ---
        public void OnLockedOverlayClicked()
        {
            if (_lockedMessageLocalizer != null)
            {
                // Включаем объект текста ошибки
                _lockedMessageLocalizer.gameObject.SetActive(true);

                // Настраиваем ключ локализации ошибки и подставляем уровень из инспектора карточки
                _lockedMessageLocalizer.StringReference.SetReference(TABLE_NAME, KEY_LOCKED);
                _lockedMessageLocalizer.StringReference.Arguments = new object[] { _unlockLevel };
                _lockedMessageLocalizer.RefreshString();
                
                // Перезапускаем таймер, если игрок кликает несколько раз
                _lockMessageTween?.Kill(); 

                // Ждем 2 секунды и убираем текст ошибки
                _lockMessageTween = DOVirtual.DelayedCall(2f, () => 
                {
                    if (this != null && _lockedMessageLocalizer != null)
                    {
                        _lockedMessageLocalizer.gameObject.SetActive(false);
                    }
                }).SetUpdate(true);
            }
        }

        public void CheckCost(int money)
        {
            if (_asset == null) return;

            bool isUnlocked = _requiredUpgrade == null || Upgrades.GetUpgradeLevel(_requiredUpgrade) > 0;
            if (!isUnlocked || Upgrades.GetUpgradeLevel(_asset) >= _asset.MaxLevel)
            {
                _buttonBuy.interactable = false;
                return;
            }

            _buttonBuy.interactable = money >= _costNumber;
        }

        public void Buy()
        {
            Upgrades.BuyUpgrade(_asset);
            Initialize();

            int savedLevel = Upgrades.GetUpgradeLevel(_asset);
            AnalyticsManager.Instance.SaveShopBuy(_asset.name, savedLevel);

            Sound.BuySound.Play();
        }
        
        private void OnDestroy()
        {
            _lockMessageTween?.Kill();
        }
    }
}