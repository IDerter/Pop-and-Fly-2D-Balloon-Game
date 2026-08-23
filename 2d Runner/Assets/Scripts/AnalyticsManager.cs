using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

public class AnalyticsManager : SingletonBase<AnalyticsManager>
{
    [SerializeField] private bool _isInitialized = false;

    private async void Start()
    {
#if !UNITY_EDITOR
        await UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();
        _isInitialized = true;
#endif
        Debug.Log(_isInitialized);
    }

    public void NextLevelStats(int currentLevel)
    {
#if !UNITY_EDITOR
        Debug.Log(_isInitialized + " nextLevel");
        if (!_isInitialized)
		{
            return;
		}

        CustomEvent myEvent = new CustomEvent("next_level")
        {
            {"level_index", currentLevel }
        };
        AnalyticsService.Instance.RecordEvent(myEvent);
        AnalyticsService.Instance.Flush();
#endif
        Debug.Log("next_level");
    }

    public void RestartLeveStats(int scene_index)
    {
#if !UNITY_EDITOR
        CustomEvent myEvent = new CustomEvent("restart_level")
        {
            {"scene_index", scene_index }
        };

        AnalyticsService.Instance.RecordEvent(myEvent);
        AnalyticsService.Instance.Flush();
#endif

        Debug.Log("restart_level");
    }

    public void SaveLevelStarsStats(int scene_index, int stars_count)
    {
#if !UNITY_EDITOR
        CustomEvent myEvent = new CustomEvent("stars_level")
        {
            {"scene_index", scene_index },
            {"stars_count", stars_count }
        };

        AnalyticsService.Instance.RecordEvent(myEvent);
        AnalyticsService.Instance.Flush();
#endif

        Debug.Log($"stars_level ������� = {scene_index} � ���-�� ����� = {stars_count}");
    }

    public void SaveRewardedAds(string type)
    {
#if !UNITY_EDITOR
        CustomEvent myEvent = new CustomEvent("rewarded_ads")
        {
            {"rewarded_type", type }
        };

        AnalyticsService.Instance.RecordEvent(myEvent);
        AnalyticsService.Instance.Flush();
#endif

        Debug.Log($"SaveRewardedAds {type}");
    }

    public void SaveLearningStep(string stepName)
    {
#if !UNITY_EDITOR
        CustomEvent myEvent = new CustomEvent("stepLearning")
        {
            {"step_name", stepName }
        };

        AnalyticsService.Instance.RecordEvent(myEvent);
        AnalyticsService.Instance.Flush();
#endif

        Debug.Log($"stepName = {stepName}");
    }

    public void SaveShopBuy(string nameShop, int level)
    {
#if !UNITY_EDITOR
        CustomEvent myEvent = new CustomEvent("shopBuy")
        {
            {"name_shop", nameShop },
            {"level_shop", level }
        };

        AnalyticsService.Instance.RecordEvent(myEvent);
        AnalyticsService.Instance.Flush();
#endif

    }

    public void SaveDailyReward(int dayIndex, int rewardAmount)
    {
#if !UNITY_EDITOR
        CustomEvent myEvent = new CustomEvent("dailyReward")
        {
            {"day_index", dayIndex },
            {"reward_amount", rewardAmount }
        };

        AnalyticsService.Instance.RecordEvent(myEvent);
        AnalyticsService.Instance.Flush();
#endif

    }

     public void SaveSellTower(int scene_index)
    {
#if !UNITY_EDITOR
        CustomEvent myEvent = new CustomEvent("sellTower")
        {
            {"scene_index", scene_index }
        };

        AnalyticsService.Instance.RecordEvent(myEvent);
        AnalyticsService.Instance.Flush();
#endif

    }
}
