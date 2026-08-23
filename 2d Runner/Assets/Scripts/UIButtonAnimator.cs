using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;


public class UIButtonAnimator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        [Header("Что анимируем?")]
        [SerializeField] private RectTransform _targetVisual;

        [Header("Настройки анимации")]
        [SerializeField] private float _pressScale = 0.9f;
        [SerializeField] private float _pressDuration = 0.1f;
        [SerializeField] private float _releaseDuration = 0.2f;

        [Header("Звук")]
        [SerializeField] private Sound _clickSound = Sound.Click;

        [SerializeField] private float _fadeDuration = 0.25f;

        private Button _button;
        private Vector3 _originalScale;
        private Sequence _animSequence;
        
        // НОВОЕ: Защитный флаг
        private bool _isHiding = false; 

        private void Awake()
        {
            if (_targetVisual == null) _targetVisual = GetComponent<RectTransform>();
            _button = GetComponent<Button>();

            if (_targetVisual != null) _originalScale = _targetVisual.localScale;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_button != null && !_button.interactable) return;
            if (_targetVisual == null) return;

            _clickSound.Play();

            _animSequence?.Kill();
            _animSequence = DOTween.Sequence();
            _animSequence.Append(_targetVisual.DOScale(_originalScale * _pressScale, _pressDuration).SetEase(Ease.OutQuad));
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            // ИСПРАВЛЕНО: Защита от перехвата анимации
            if (_button != null && !_button.interactable) return; 
            if (_targetVisual == null) return;

            _animSequence?.Kill();
            _animSequence = DOTween.Sequence();
            _animSequence.Append(_targetVisual.DOScale(_originalScale, _releaseDuration).SetEase(Ease.OutBack));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // ИСПРАВЛЕНО: Главная причина бага была здесь!
            if (_button != null && !_button.interactable) return; 
            if (_targetVisual == null) return;

            _animSequence?.Kill();
            _animSequence = DOTween.Sequence();
            _animSequence.Append(_targetVisual.DOScale(_originalScale, _releaseDuration).SetEase(Ease.OutQuad));
        }

        public void Hide()
        {
            if (_targetVisual == null || !gameObject.activeInHierarchy) return;

            if (_button != null) _button.interactable = false;
            
            _isHiding = true; // Ставим флаг, что процесс пошел

            _animSequence?.Kill();
            _animSequence = DOTween.Sequence();
            
            _animSequence.Append(_targetVisual.DOScale(Vector3.zero, _fadeDuration).SetEase(Ease.InBack))
                         .OnComplete(() => 
                         {
                             _isHiding = false;
                             gameObject.SetActive(false); 
                         });
        }

        public void Show()
        {
            if (_targetVisual == null) return;

            _isHiding = false; // Сбрасываем флаг

            gameObject.SetActive(true);
            if (_button != null) _button.interactable = true;

            _targetVisual.localScale = Vector3.zero;

            _animSequence?.Kill();
            _animSequence = DOTween.Sequence();
            
            _animSequence.Append(_targetVisual.DOScale(_originalScale, _fadeDuration).SetEase(Ease.OutBack));
        }

        private void OnDestroy()
        {
            _animSequence?.Kill();
        }

        private void OnDisable()
        {
            _animSequence?.Kill();
            if (_targetVisual != null) _targetVisual.localScale = _originalScale;

            // ИСПРАВЛЕНО: Если анимация прервалась (например, выключили родителя), доводим дело до конца!
            if (_isHiding)
            {
                _isHiding = false;
                gameObject.SetActive(false);
            }
        }
    }