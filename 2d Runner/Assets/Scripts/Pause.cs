using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject panelmain;
    public GameObject buttonreturn;
    public GameObject buttonreload;
    public GameObject buttonexit;
    public GameObject musicoff;
    public GameObject musicon;
    public GameObject musicmainlvl;
    public AudioSource sound;
    public static int countpause = 0;
    private const string achiv2 = "CgkIu-eNx_IEEAIQAw";
    public static int music = 0;
    // Use this for initialization
    void Start()
    {
        music = PlayerPrefs.GetInt("checkmusic");
        if (PlayerPrefs.GetInt("checkmusic") == 0)
        {
            musicmainlvl.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("checkmusic") == 1)
        {
            musicmainlvl.SetActive(false);
        }
    }
    private void GetTheAchiv(string id)
    {
        Social.ReportProgress(id, 100.0f, (bool success) =>
        {
            if (success) print("Получено достижение" + id);
        });
    }
    // Update is called once per frame
    void Update()
    {
        /*if (pause.activeSelf == false && Input.GetKeyDown(KeyCode.Escape))
        {
            pause.SetActive(true);
        }
       else if (pause.activeSelf == true && Input.GetKeyDown(KeyCode.Escape))
        {
            pause.SetActive(false);
        }
        */
    }
    public void Return()
    {
        if(buttonreturn.activeSelf==false) //(panelmain.activeSelf == false)
        {
            Time.timeScale = 0f;
            buttonreturn.SetActive(true);
            buttonreload.SetActive(true);
            buttonexit.SetActive(true);
            Instantiate(sound, transform.position, Quaternion.identity);
            if(countpause ==0)
            {
                countpause += 1;
                GetTheAchiv(achiv2);
            }
        }
        else if (buttonreturn.activeSelf == true)
        {
            Time.timeScale = 1f;
            buttonreturn.SetActive(false);
            buttonreload.SetActive(false);
            buttonexit.SetActive(false);
            Instantiate(sound, transform.position, Quaternion.identity);
        }

    }
    public void OffMusic()
    {
        musicoff.SetActive(false);
        musicon.SetActive(true);
        musicmainlvl.SetActive(false);
    }
    public void OnMusic()
    {
        musicoff.SetActive(true);
        musicon.SetActive(false);
        musicmainlvl.SetActive(true);
    }
    public void ReturnOnGame()
    {
        Time.timeScale = 1f;
        Instantiate(sound, transform.position, Quaternion.identity);
        buttonreturn.SetActive(false);
        buttonreload.SetActive(false);
        buttonexit.SetActive(false);
    }
    public void ExitMenu()
    {
        Application.LoadLevel("Menu");
        Instantiate(sound, transform.position, Quaternion.identity);
        Time.timeScale = 1f;

    }
    public void Reload()
    {
        Application.LoadLevel(Application.loadedLevel);
        Instantiate(sound, transform.position, Quaternion.identity);
        buttonreturn.SetActive(false);
        buttonreload.SetActive(false);
        buttonexit.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ReloadLvl()
    {
        Application.LoadLevel("MainLvl");
        Instantiate(sound, transform.position, Quaternion.identity);
        Time.timeScale = 1f;
    }
}
