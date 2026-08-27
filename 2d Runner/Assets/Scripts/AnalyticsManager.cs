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

    public void SaveLevelLolipopsStats(int scene_index, int lolipops_count)
    {
#if !UNITY_EDITOR
        CustomEvent myEvent = new CustomEvent("lolipops_level")
        {
            {"scene_index", scene_index },
            {"lolipops_count", lolipops_count }
        };

        AnalyticsService.Instance.RecordEvent(myEvent);
        AnalyticsService.Instance.Flush();
#endif

        Debug.Log($"lolipops_level = {scene_index} Lolipops= {lolipops_count}");
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
