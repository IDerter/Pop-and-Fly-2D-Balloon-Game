using UnityEngine;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine.Advertisements;
using System.Collections;
public class SceneAchivandBoard : MonoBehaviour
{
    public GameObject buttonachiv;
    public GameObject buttontablelid;
    public static int countopen = 0;
    // Start is called before the first frame update
    public void Awake()
    {

        countopen = PlayerPrefs.GetInt("CountOpen");
       /* PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) =>
        {
            if (success == true)
            {
                print("удачно вошел!");
            }
            else
                print("удачно вошел!");
        });
        */
             if (countopen != 1)
             {


                 PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
                 PlayGamesPlatform.InitializeInstance(config);
                 PlayGamesPlatform.Activate();
                 Social.localUser.Authenticate((bool success) =>
                 {
                     if (success == true)
                     {
                         print("удачно вошел!");
                     }

                     else
                     {

                         countopen = 1;
                         PlayerPrefs.SetInt("CountOpen", countopen);
                         print("Неудачно вошел!");
                         if (countopen >= 1)
                         {
                             Destroy(gameObject, 2f);
                             buttontablelid.SetActive(false);
                             buttonachiv.SetActive(false);
                         }
                     }

                 });
             }
             else
             {
                 Destroy(gameObject, 0.1f);
                 buttontablelid.SetActive(false);
                 buttonachiv.SetActive(false);
             }
            
        }
    public void Gps()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
    }
    public void achivOpen()
    {
        Social.ShowAchievementsUI();
    }
    public void OpenGPS()
    {
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) =>
        {
            if (success == true)
            {
                print("удачно вошел!");
            }
            else
            {
                print("Неудачно вошел!");
            }
        });
    }
    public void leaderboardOpen()
    {
        Social.ShowLeaderboardUI();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
