using UnityEngine;
using TMPro;
using DG.Tweening; // Твой знакомый DOTween

public class GameOverUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Transform cookiePanel;           // Сюда перетащи PanelCookie
    public CanvasGroup backgroundDim;       // Сюда перетащи темный фон (добавь на него CanvasGroup, если нет)
    public TextMeshProUGUI currentScoreText; // Текст "1200"
    public TextMeshProUGUI bestScoreText;    // Текст рекорда "12"

    public void AnimateGameOver(int currentScore, int bestScore)
    {
        gameObject.SetActive(true);

        // 1. Сброс состояний (прячем печеньку, обнуляем счет)
        cookiePanel.localScale = Vector3.zero;
        backgroundDim.alpha = 0f;
        currentScoreText.text = "0";
        bestScoreText.text = bestScore.ToString(); // Рекорд показываем сразу

        // 2. Плавное появление темного фона
        backgroundDim.DOFade(1f, 0.3f);

        // 3. Выпрыгивание печеньки с эффектом пружины (OutBack)
        cookiePanel.DOScale(1f, 0.5f).SetEase(Ease.OutBack).SetDelay(0.2f).OnComplete(() =>
        {
            // Как только печенька выпрыгнула, запускаем набегание цифр
            AnimateScoreUp(currentScore);
        });
    }

    private void AnimateScoreUp(int targetScore)
    {
        int displayScore = 0;
        
        // DOTween крутит переменную от 0 до targetScore за 1.5 секунды
        DOTween.To(() => displayScore, x => displayScore = x, targetScore, 1.5f)
            .OnUpdate(() => 
            {
                currentScoreText.text = displayScore.ToString();
                
                // Перенес твою логику изменения размера шрифта сюда!
                if (displayScore >= 1000)
                {
                    currentScoreText.fontSize = 60;
                }
                else if (displayScore >= 100)
                {
                    currentScoreText.fontSize = 80;
                }
            })
            .SetEase(Ease.OutQuad); // Замедление анимации к концу
    }
}