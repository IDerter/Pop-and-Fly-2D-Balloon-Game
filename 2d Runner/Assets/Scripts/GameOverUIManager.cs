using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using DG.Tweening; 
using YG; 
using UnityEngine.SceneManagement;

public class GameOverUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Transform cookiePanel;          
    public CanvasGroup backgroundDim;       
    public TextMeshProUGUI currentScoreText; 
    public TextMeshProUGUI bestScoreText;    

    [Header("Tutorial Elements")]
    public GameObject shopTutorialFinger;    
    public Transform shopButtonTransform;    
    public Button[] buttonsToLockInTutorial; 
    
    [Tooltip("Объект с картинкой рекламы на кнопке Reborn")]
    public GameObject rebornAdImage; // <--- НОВАЯ ПЕРЕМЕННАЯ

    private Tween _scoreTween; // Сохраняем ссылку на анимацию очков, чтобы надежно ее убивать

    private void Awake()
    {
        gameObject.SetActive(false); 
    }

    private void OnEnable()
    {
        if (backgroundDim != null)
        {
            backgroundDim.blocksRaycasts = true;
        }
    }

    private void OnDisable()
    {
        if (backgroundDim != null)
        {
            backgroundDim.DOKill(); 
            backgroundDim.blocksRaycasts = false; 
            backgroundDim.alpha = 0f; 
        }

        _scoreTween?.Kill(); // Гарантированно убиваем набегание цифр при отключении

        if (shopTutorialFinger != null) shopTutorialFinger.SetActive(false);
        if (shopButtonTransform != null)
        {
            shopButtonTransform.DOKill();
            shopButtonTransform.localScale = Vector3.one; 
        }
    }

    public void CompleteShopTutorial()
    {
        if (YG2.saves.isTutorialCompleted) return;

        YG2.saves.isTutorialCompleted = true;
        YG2.SaveProgress();

        AnalyticsManager.Instance.SaveLearningStep("tutorial_complete");

        if (shopTutorialFinger != null)
        {
            shopTutorialFinger.transform.DOKill();
            shopTutorialFinger.SetActive(false);
        }

        if (shopButtonTransform != null)
        {
            shopButtonTransform.DOKill();
            shopButtonTransform.localScale = Vector3.one;
        }

        // --- Включаем картинку рекламы обратно, так как туториал пройден ---
        if (rebornAdImage != null) rebornAdImage.SetActive(true);

        foreach (var btn in buttonsToLockInTutorial)
        {
            if (btn != null)
            {
                btn.interactable = true;
                btn.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0), 0.3f);
            }
        }
    }

    public void AnimateGameOver(int currentScore, int bestScore)
    {
        gameObject.SetActive(true); 
        
        bool isTutorialCompleted = YG2.saves.isTutorialCompleted;

        if (!isTutorialCompleted)
        {
            AnalyticsManager.Instance.SaveLearningStep("game_over_first");
        }

        // --- Прячем или показываем иконку рекламы в зависимости от статуса обучения ---
        if (rebornAdImage != null)
        {
            rebornAdImage.SetActive(false);
        }

        // --- БЛОКИРУЕМ ВООБЩЕ ВСЕ КНОПКИ НА СТАРТЕ ---
        foreach (var btn in buttonsToLockInTutorial)
        {
            if (btn != null) btn.interactable = false; 
        }
        
        // Блокируем и кнопку магазина тоже (ищем на ней компонент Button)
        if (shopButtonTransform != null && shopButtonTransform.TryGetComponent(out Button shopBtn))
        {
            shopBtn.interactable = false;
        }

        if (shopTutorialFinger != null) shopTutorialFinger.SetActive(false);
        if (shopButtonTransform != null) shopButtonTransform.localScale = Vector3.one;

        cookiePanel.localScale = Vector3.zero;
        backgroundDim.alpha = 0f;
        currentScoreText.text = "0";
        bestScoreText.text = bestScore.ToString(); 

        backgroundDim.DOFade(1f, 0.3f);

        Sound.Whoosh.Play();

        cookiePanel.DOScale(1f, 0.5f).SetEase(Ease.OutBack).SetDelay(0.2f).OnComplete(() =>
        {
            AnimateScoreUp(currentScore);
        });
    }

    public void HidePanel()
    {
        _scoreTween?.Kill(); // Останавливаем счетчик, если он еще бежит
        backgroundDim.DOKill();
        cookiePanel.DOKill();
        
        if (shopButtonTransform != null) shopButtonTransform.DOKill();

        backgroundDim.blocksRaycasts = false;
        backgroundDim.DOFade(0f, 0.3f);

        cookiePanel.DOScale(0f, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    private void AnimateScoreUp(int targetScore)
    {
        int displayScore = 0;
        float lastTickTime = 0f; 

        _scoreTween?.Kill();
        _scoreTween = DOTween.To(() => displayScore, x => displayScore = x, targetScore, 1.5f)
            .OnUpdate(() => 
            {
                currentScoreText.text = displayScore.ToString();
                
                if (Time.unscaledTime - lastTickTime > 0.08f && displayScore < targetScore)
                {
                    lastTickTime = Time.unscaledTime;
                    Sound.PopUp.Play(); 
                }
                
                if (displayScore >= 1000)
                {
                    currentScoreText.fontSize = 60;
                }
                else if (displayScore >= 100)
                {
                    currentScoreText.fontSize = 80;
                }
            })
            .SetEase(Ease.OutQuad)
            .OnComplete(() => 
            {
                Sound.Fanfar.Play(); 
                AnalyticsManager.Instance.SaveLevelLolipopsStats(SceneManager.GetActiveScene().buildIndex, targetScore);

                
                currentScoreText.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0), 0.3f);

                // --- АНИМАЦИЯ ОКОНЧЕНА: РАЗБЛОКИРУЕМ КНОПКИ ---
                bool isTutorialCompleted = YG2.saves.isTutorialCompleted;

                // Кнопка магазина разблокируется всегда
                if (shopButtonTransform != null && shopButtonTransform.TryGetComponent(out Button shopBtn))
                {
                    shopBtn.interactable = true;
                }

                if (rebornAdImage != null)
                {
                    rebornAdImage.SetActive(true);
                }

                // Остальные кнопки - только если туториал пройден
                if (isTutorialCompleted)
                {
                    foreach (var btn in buttonsToLockInTutorial)
                    {
                        if (btn != null) btn.interactable = true;
                    }
                }
                else
                {
                    // Иначе запускаем подсказку с пальцем
                    ShowShopTutorial();
                }
            });
    }

    private void ShowShopTutorial()
    {
        if (!YG2.saves.isTutorialCompleted)
        {
            AnalyticsManager.Instance.SaveLearningStep("opened_shop");
        }

        if (shopTutorialFinger != null)
        {
            shopTutorialFinger.SetActive(true);
            shopTutorialFinger.transform.DOKill();
            Vector3 originalPos = shopTutorialFinger.transform.localPosition;
            shopTutorialFinger.transform.DOLocalMove(originalPos + new Vector3(-20f, -20f, 0f), 0.5f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        if (shopButtonTransform != null)
        {
            shopButtonTransform.DOKill();
            shopButtonTransform.DOScale(1.15f, 0.5f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }
    }
}