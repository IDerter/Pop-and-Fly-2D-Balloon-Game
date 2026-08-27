using TMPro;
using UnityEngine;
using YG;
using DG.Tweening; // Обязательно подключаем DOTween!

namespace AmNuamRunner
{
    public class UpgradeShop : MonoBehaviour
    {
        [SerializeField] private int _money;
        [SerializeField] private TextMeshProUGUI _textMoney;
        [SerializeField] private BuyUpgrade[] _sales;

        private int _displayedMoney; // То, что сейчас видит игрок на экране
        private Tween _moneyTween;   // Ссылка на анимацию цифр
        private Vector3 _defaultTextScale; // Стартовый размер текста

        private void Awake()
        {
            if (_textMoney != null)
            {
                _defaultTextScale = _textMoney.transform.localScale;
            }
        }

        private void Start()
        {
            // Устанавливаем стартовое значение сразу, без анимации
            _money = YG2.saves.coin;
            _displayedMoney = _money;
            if (_textMoney != null) _textMoney.text = _displayedMoney.ToString();

            foreach (var slot in _sales)
            {
                slot.Initialize();
            }
            
            UpdateMoney(false); // Инициализация кнопок без анимации
        }

        private void OnEnable()
        {
            Upgrades.OnUpgradeChanged += RefreshAllSlots;
            BuyUpgrade.OnSkinChanged += RefreshAllSlots;
            
            if (YG2.isSDKEnabled) 
            {
                UpdateMoney(false); // При открытии магазина просто показываем актуальный баланс
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
            UpdateMoney(true); // А вот при покупке - вызываем С анимацией!
        }

        // Добавили параметр animate = true по умолчанию
        public void UpdateMoney(bool animate = true)
        {
            _money = YG2.saves.coin; 
            
            if (_textMoney != null) 
            {
                if (animate && _displayedMoney != _money)
                {
                    // 1. Анимация бегущих цифр
                    _moneyTween?.Kill(); // Останавливаем старую анимацию, если игрок спамит кнопку
                    _moneyTween = DOTween.To(() => _displayedMoney, x => 
                    {
                        _displayedMoney = x;
                        _textMoney.text = _displayedMoney.ToString();
                    }, _money, 0.5f).SetUpdate(true); // Занимает 0.5 секунд, работает даже на паузе

                    // 2. Анимация пульсации текста (Джус)
                    // Сначала сбрасываем масштаб, чтобы не исказилось при быстрых кликах
                    _textMoney.transform.DOKill(); 
                    _textMoney.transform.localScale = _defaultTextScale;
                    
                    // Цвет пульсации (зеленый прибавили, красный отняли)
                    Color popColor = _money > _displayedMoney ? Color.green : new Color(1f, 0.3f, 0.3f);
                    
                    // Прыжок размера текста
                    _textMoney.transform.DOPunchScale(new Vector3(0.25f, 0.25f, 0), 0.35f, 2, 0.5f).SetUpdate(true);
                    
                    // Мигание цветом
                    _textMoney.DOColor(popColor, 0.15f).SetUpdate(true).OnComplete(() => {
                        _textMoney.DOColor(Color.white, 0.2f).SetUpdate(true); // Возвращаем в белый
                    });
                }
                else
                {
                    // Если анимация не нужна (например, при открытии окна)
                    _displayedMoney = _money;
                    _textMoney.text = _displayedMoney.ToString();
                }
            }
            
            // Проверяем, хватает ли денег на апгрейды (ориентируемся на реальные деньги _money, а не на анимацию)
            foreach(var slot in _sales)
            {
                slot.CheckCost(_money);
            }
        }

        private void OnDestroy()
        {
            // Очищаем память от анимаций при удалении объекта
            _moneyTween?.Kill();
            if (_textMoney != null) _textMoney.transform.DOKill();
        }
    }
}