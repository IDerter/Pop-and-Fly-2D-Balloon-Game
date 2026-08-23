using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsRewarded : MonoBehaviour
{
    [SerializeField] private Button _buttonClickRewarded;
    [SerializeField] private bool _isDelete = true;
    [SerializeField] private TypeReward _type;


    public void StartRewarded()
    {
        AdsManager.Instance._rewardedAds.ShowRewardedAd(_type, null);

        if (AnalyticsManager.Instance != null)
            AnalyticsManager.Instance.SaveRewardedAds(_type.ToString());

        if (_isDelete)
            Destroy(_buttonClickRewarded.gameObject, 1f);
    }
}
