using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening; 

public class LevelChangerDotween : MonoBehaviour
{
    [Header("Настройки перехода")]
    [SerializeField] private Image fadeImage; 
    [SerializeField] private float fadeDuration = 1f; 

    private int _targetLevelIndex; 

    private void Start()
    {
        // Плавное появление (Fade In) при старте новой сцены остается как было
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            fadeImage.raycastTarget = true; 
            
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1f);
            
            fadeImage.DOFade(0f, fadeDuration).SetUpdate(true).OnComplete(() => 
            {
                fadeImage.raycastTarget = false; 
            });
        }
    }

    // ВЫЗЫВАТЬ ЭТОТ МЕТОД ПО КЛИКУ НА КНОПКУ ПЕРЕХОДА
    public void FadeToLevelWithAd(int levelIndex)
    {
        _targetLevelIndex = levelIndex; 
        
        // 1. СРАЗУ показываем рекламу (Требование Яндекса < 0.33 сек)
        InterstitialAds.OnInterstitialAdClosed += OnAdClosedForTransition;
        AdsManager.Instance._interstitialAds.ShowInterstitialAd();
    }

    private void OnAdClosedForTransition()
    {
        InterstitialAds.OnInterstitialAdClosed -= OnAdClosedForTransition;

        // 2. Реклама закончилась (или была пропущена из-за кулдауна). 
        // Теперь делаем плавное затемнение и меняем сцену!
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            fadeImage.raycastTarget = true; 
            
            fadeImage.DOFade(1f, fadeDuration).SetUpdate(true).OnComplete(() =>
            {
                SceneManager.LoadScene(_targetLevelIndex);
            });
        }
        else
        {
            // На всякий случай, если забыли привязать картинку
            SceneManager.LoadScene(_targetLevelIndex);
        }
    }

    private void OnDestroy()
    {
        InterstitialAds.OnInterstitialAdClosed -= OnAdClosedForTransition;
    }
}