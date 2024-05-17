/*using UnityEngine;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine.Advertisements;
using System.Collections;
public class PlayServices : MonoBehaviour
{
    public string gameId = "1234567";
    public string placementId = "bannerPlacement";
    public bool testMode = true;
    //public int playerScore;
    //string leaderboardID = "";
    //  string achievementID = "";
    // public bool isConnectedToGooglePlayServices;
    // private void Awake()
    //  {
    //     PlayGamesPlatform.DebugLogEnabled = true;
    //     PlayGamesPlatform.Activate();
    //  }
    // private void Start()
    //  {
    //     SignToGooglePlayServices();
    //  }
    public void Start()
    {
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) =>
        {
            if (success == true)
            {
                print("удачно вошел!");
            }
            else print("Неудачно вошел!");
        });
        Advertisement.Initialize(gameId, testMode);
        StartCoroutine(ShowBannerWhenInitialized());
    }
  


    public void achivOpen()
    {
        Social.ShowAchievementsUI();
    }
    public void leaderboardOpen()
    {
        Social.ShowLeaderboardUI();
    }
    //  public void SignToGooglePlayServices()
    //  {
    //    PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) =>
    //    {
    //     switch (result)
    //     {
    //    case SignInStatus.Success:
    //      isConnectedToGooglePlayServices = true;
    //     break;
    //   default:
    //   isConnectedToGooglePlayServices = false;
    //   break;
    //  }
    // });

    IEnumerator ShowBannerWhenInitialized()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show(placementId);
    }

    //}
    /* void Start()
     {

         DontDestroyOnLoad(this);
         try
         {
             PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
             PlayGamesPlatform.InitializeInstance(config);
             PlayGamesPlatform.DebugLogEnabled = true;
             PlayGamesPlatform.Activate();
             Social.localUser.Authenticate((bool success) => { });
         }
         catch (Exception exception)
         {
             Debug.Log(exception);
         }
     }

     public void AddScoreToLeaderboard()
     {
         if (Social.localUser.authenticated)
         {
             Social.ReportScore(playerScore, "CgkIu-eNx_IEEAIQAA", success => { });
         }
     }

     public void ShowLeaderboard()
     {
         if (Social.localUser.authenticated)
         {
             Social.ShowLeaderboardUI();
         }
     }

     public void ShowAchievements()
     {
         if (Social.localUser.authenticated)
         {
             Social.ShowAchievementsUI();
         }
     }

     public void UnlockAchievement()
     {
         if (Social.localUser.authenticated)
         {
             Social.ReportProgress("CgkIu - eNx_IEEAIQAQ", 100f, success => { });
         }
     }
     */
//}