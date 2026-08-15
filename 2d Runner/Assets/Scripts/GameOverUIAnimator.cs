using UnityEngine;
using TMPro;
using DG.Tweening; // Подключаем DOTween

public class GameOverUIAnimator : MonoBehaviour
{
    [Header("UI Elements")]
    public Transform cookiePanel; // Перетащи сюда саму печеньку (PanelCookie)
    public CanvasGroup backgroundDim; // Добавь CanvasGroup на темный фон позади печеньки
    public TextMeshProUGUI currentScoreText; // Текст с цифрами 1200
    
    private int targetScore; // Очки, которые игрок набрал в забеге

    public void ShowGameOver(int score)
    {
        targetScore = score;
        gameObject.SetActive(true);

        // 1. Сброс начальных состояний
        cookiePanel.localScale = Vector3.zero; // Печенька сжата в ноль
        backgroundDim.alpha = 0f; // Фон прозрачный
        currentScoreText.text = "0"; // Счет начинается с нуля

        // 2. Анимация затемнения фона
        backgroundDim.DOFade(1f, 0.3f);

        // 3. Выпрыгивание печеньки (Эффект упругости - OutBack)
        cookiePanel.DOScale(1f, 0.5f).SetEase(Ease.OutBack).SetDelay(0.2f).OnComplete(AnimateScore);
    }

    private void AnimateScore()
    {
        // 4. Динамическое набегание цифр счета от 0 до targetScore
        int currentDisplayScore = 0;
        DOTween.To(() => currentDisplayScore, x => currentDisplayScore = x, targetScore, 1f)
            .OnUpdate(() => 
            {
                currentScoreText.text = currentDisplayScore.ToString();
            })
            .SetEase(Ease.OutQuad); // Цифры замедляются к концу
    }
}