using UnityEngine;
using DG.Tweening;

public class KeyPressAnimation : MonoBehaviour
{
    [SerializeField] private float jumpDistance = -15f; // На сколько пикселей кнопка "проваливается"
    [SerializeField] private float duration = 0.4f;

    private void OnEnable()
    {
        // Имитация нажатия: сдвиг вниз (по Y) и легкое уменьшение, затем возврат обратно
        transform.DOLocalMoveY(transform.localPosition.y + jumpDistance, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);

        transform.DOScale(0.95f, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    private void OnDisable()
    {
        transform.DOKill();
    }
}