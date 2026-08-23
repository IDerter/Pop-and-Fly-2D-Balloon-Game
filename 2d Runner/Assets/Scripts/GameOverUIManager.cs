using UnityEngine;
using TMPro;
using DG.Tweening; 

public class GameOverUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Transform cookiePanel;           // Сюда перетащи PanelCookie
    public CanvasGroup backgroundDim;       // Сюда перетащи темный фон с CanvasGroup
    public TextMeshProUGUI currentScoreText; // Текст "1200"
    public TextMeshProUGUI bestScoreText;    // Текст рекорда "12"

    // Срабатывает автоматически при gameObject.SetActive(true)
    private void OnEnable()
    {
        if (backgroundDim != null)
        {
            backgroundDim.blocksRaycasts = true;
        }
    }

    // Срабатывает автоматически при gameObject.SetActive(false)
    private void OnDisable()
    {
        if (backgroundDim != null)
        {
            // Обязательно "убиваем" анимацию прозрачности, если она еще не успела закончиться
            backgroundDim.DOKill(); 
            
            // Снимаем блокировку кликов
            backgroundDim.blocksRaycasts = false; 
            
            // ВОТ ОН ФИКС: Сбрасываем прозрачность обратно в ноль!
            backgroundDim.alpha = 0f; 
        }
    }

    public void AnimateGameOver(int currentScore, int bestScore)
    {
        gameObject.SetActive(true); // Это автоматически вызовет OnEnable()

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

    public void HidePanel()
    {
        // Убиваем текущие анимации, чтобы не было конфликтов
        backgroundDim.DOKill();
        cookiePanel.DOKill();

        backgroundDim.blocksRaycasts = false;

        // Плавно убираем затемнение
        backgroundDim.DOFade(0f, 0.3f);

        // Печенька "улетает" обратно, и только когда анимация закончится — выключаем объект
        cookiePanel.DOScale(0f, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
        {
            gameObject.SetActive(false);
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
                
                // Логика изменения размера шрифта
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