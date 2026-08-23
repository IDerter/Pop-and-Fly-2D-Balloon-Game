using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using YG;
using AmNuamRunner;

public class InterstitialAds : MonoBehaviour
{
    public static event Action OnInterstitialAdClosed;

    [Header("In-App Покупки")]
    [Tooltip("Ассет покупки, который отключает принудительную рекламу")]
    [SerializeField] private UpgradeAsset _removeAdsUpgrade;

    [SerializeField] private string _androidAdUnityId;
    [SerializeField] private string _iosAdUnityId;

    private string _adUnitId;
    private bool _isYandexReady = false;

    private void Awake()
    {
#if UNITY_IOS
            _adUnitId = _iosAdUnityId;
#elif UNITY_ANDROID
            _adUnitId = _androidAdUnityId;
#endif
    }

    private void OnEnable()
    {
        // Подписываемся на события закрытия и ошибки
        YG2.onCloseInterAdv += YandexAdClosed;
        YG2.onErrorInterAdv += YandexAdError;
    }

    private void OnDisable()
    {
        // Отписываемся, чтобы избежать утечек памяти
        YG2.onCloseInterAdv -= YandexAdClosed;
        YG2.onErrorInterAdv -= YandexAdError;
    }
    private void YandexAdClosed()
    {
        Debug.Log("[InterstitialAds] Yandex Ad Closed visually by user.");
        OnInterstitialAdClosed?.Invoke();
    }

    private void YandexAdError()
    {
        Debug.LogWarning("[InterstitialAds] Yandex Ad failed to show. Skipping.");
        // Обязательно пропускаем игрока дальше, если реклама не загрузилась (например, блокировщик рекламы)
        OnInterstitialAdClosed?.Invoke(); 
    }

    private void Start()
    {
        // ��������� ���������� ������ SDK
        if (YG2.platform == "YandexGames")
        {
            StartCoroutine(WaitForYandexSDK());
        }
        else
        {
            _isYandexReady = true;
        }
    }

    private IEnumerator WaitForYandexSDK()
    {
        float timeout = 10f;
        float elapsed = 0f;

        while (!IsYandexReady() && elapsed < timeout)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        _isYandexReady = IsYandexReady();
        Debug.Log($"[InterstitialAds] Yandex SDK ready: {_isYandexReady}");
    }

    private bool IsYandexReady()
    {
        try
        {
            if (!string.IsNullOrEmpty(YG2.platform) && !string.IsNullOrEmpty(YG2.lang))
            {
                return true;
            }
        }
        catch
        {
            return false;
        }

        return false;
    }

    public void LoadInterstitialAd()
    {
        if (YG2.platform == "YandexGames")
        {
            if (!_isYandexReady)
            {
                Debug.Log("[InterstitialAds] Yandex SDK not ready yet, waiting...");
                StartCoroutine(LoadWhenYandexReady());
                return;
            }

            Debug.Log("[InterstitialAds] YandexGames - using Yandex SDK");
            return;
        }
    }

    private IEnumerator LoadWhenYandexReady()
    {
        float timeout = 10f;
        float elapsed = 0f;

        while (!IsYandexReady() && elapsed < timeout)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        _isYandexReady = IsYandexReady();

        if (_isYandexReady)
        {
            Debug.Log("[InterstitialAds] Yandex SDK now ready");
        }
    }

    public void ShowInterstitialAd()
    {
        if (_removeAdsUpgrade != null && Upgrades.GetUpgradeLevel(_removeAdsUpgrade) > 0)
        {
            Debug.Log("[InterstitialAds] Игрок купил 'No Ads'. Пропускаем межстраничную рекламу!");
            OnInterstitialAdClosed?.Invoke(); 
            return;
        }

        if (YG2.platform == "YandexGames")
        {
            if (!_isYandexReady)
            {
                Debug.Log("[InterstitialAds] Yandex SDK not ready, cannot show ad");
                OnInterstitialAdClosed?.Invoke();
                return;
            }

            // НОВАЯ ПРОВЕРКА: Если таймер между показами еще не прошел, мгновенно пускаем дальше!
            if (!YG2.isTimerAdvCompleted) 
            {
                Debug.Log($"[InterstitialAds] Реклама на кулдауне. Осталось: {YG2.timerInterAdv} сек. Пропускаем.");
                OnInterstitialAdClosed?.Invoke();
                return;
            }

            Debug.Log("[InterstitialAds] Showing Yandex interstitial");
            YG2.InterstitialAdvShow(); 
            return;
        }

        LoadInterstitialAd();
        StartCoroutine(InterstitialDelayShow());
    }

    public void ShowAd()
    {
        if (YG2.platform == "YandexGames")
        {
            return;
        }
    }

    //#region IUnityAdsLoadListener implementation
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log($"[InterstitialAds] Unity Ads loaded: {placementId}");
    }
    /*

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogWarning($"[InterstitialAds] Unity Ads failed to load: {placementId} - {error} - {message}");
        OnInterstitialAdClosed?.Invoke();
        StartCoroutine(RetryLoadAd(5f));
    }
    #endregion

    #region IUnityAdsShowListener implementation
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"[InterstitialAds] Unity Ads failed to show: {placementId} - {error} - {message}");
        OnInterstitialAdClosed?.Invoke();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log($"[InterstitialAds] Unity Ads started: {placementId}");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log($"[InterstitialAds] Unity Ads clicked: {placementId}");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log($"[InterstitialAds] Unity Ads completed: {placementId} - {showCompletionState}");

        if (placementId == _adUnitId)
        {
            OnInterstitialAdClosed?.Invoke();
        }
    }
    #endregion
    */

    public IEnumerator InterstitialDelayShow()
    {
        yield return new WaitForSeconds(2f);
        ShowAd();
       // MapCompletion.SaveLvlFinished();
    }

    private IEnumerator RetryLoadAd(float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadInterstitialAd();
        Debug.Log($"[InterstitialAds] Retrying to load ad in {delay} seconds.");
    }
}