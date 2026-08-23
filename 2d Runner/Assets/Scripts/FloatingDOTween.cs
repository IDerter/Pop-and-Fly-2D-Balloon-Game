using UnityEngine;
using DG.Tweening; // Обязательно подключаем библиотеку

public class FloatingDOTween : MonoBehaviour
{
    [Header("Настройки DOTween")]
    [SerializeField] private float moveDistance = 15f; // Дистанция вверх
    [SerializeField] private float duration = 1.5f;    // Время одного взмаха (в секундах)

    void Start()
    {
        RectTransform rect = GetComponent<RectTransform>();

        if (rect != null)
        {
            // Двигаем по Y, зацикливаем бесконечно (-1), тип цикла Yoyo (туда-обратно)
            // Ease.InOutSine дает идеальную плавность на стартах и разворотах
            rect.DOAnchorPosY(rect.anchoredPosition.y + moveDistance, duration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }
    }
}