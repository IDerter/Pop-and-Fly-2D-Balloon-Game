using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public GameObject panel;
    public GameObject gameOverCanvas;
    public GameObject Canvas;
    public GameObject exitbutton;
    public GameObject playbutton;
    public GameObject settingsbutton;
    public GameObject volume1button;
    public GameObject volume2button;
    public GameObject vkbutton;
    public GameObject levelChanger;
    public GameObject ExitPanel;
    public GameObject PanelShop;
    public GameObject mainfon;
    public GameObject ButtonShopText;
    public GameObject audiomusic;
    public GameObject buttonmusic1;
    public GameObject buttonmusic2;
    public Animator anim;
    public GameObject player;
    public int LevelToLoad;
    public GameObject exitmenu;
    public static bool musicon = true;
    //public GameObject Engbutton;
    //public GameObject Rusbutton;
    public GameObject panelLoc;
    static int numberdeaths = 0;
    public static int music = 0;

    private void Start()
    {
        if(musicon == false)
        {
            audiomusic.SetActive(false);
        }
    }
    public void FadeToLevel()
    {
        anim.SetTrigger("fade");
        GetComponent<AudioSource>().Play();
    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(2);
    }
        public void Settings()
    {
        GetComponent<AudioSource>().Play();
        playbutton.SetActive(false);
        exitbutton.SetActive(false);
        settingsbutton.SetActive(false);
        panel.SetActive(true);
        volume1button.SetActive(true);
        volume2button.SetActive(true);
        player.SetActive(false);
        exitmenu.SetActive(true);
    }
    public void OnClickExit()
    {
        GetComponent<AudioSource>().Play();
        playbutton.SetActive(false);
        exitbutton.SetActive(false);
        settingsbutton.SetActive(false);
        ExitPanel.SetActive(true);
        player.SetActive(false);

        //Application.Quit();
    }
    public void ExitPanel1()
    {
        GetComponent<AudioSource>().Play();
        panel.SetActive(false);
        playbutton.SetActive(true);
        exitbutton.SetActive(true);
        settingsbutton.SetActive(true);
        player.SetActive(true);
        exitmenu.SetActive(false);
    }
    public void Play()
    {
        GetComponent<AudioSource>().Play();

        Application.LoadLevel("ChooseLocation");
    }
    public void PlayGame()
    {
        GetComponent<AudioSource>().Play();

        Application.LoadLevel("MainLvl");
    }
    public void Volume()
    {
        GetComponent<AudioSource>().Play();
        AudioListener.volume = 1;
        volume2button.SetActive(true);
        volume1button.SetActive(false);

    }
    public void MusicOff()
    {
        GetComponent<AudioSource>().Play();
        musicon = false;
        audiomusic.SetActive(false);
        buttonmusic2.SetActive(true);
        buttonmusic1.SetActive(false);
        music = 1;
        PlayerPrefs.SetInt("checkmusic", music);
    }
    public void MusicOn()
    {
        GetComponent<AudioSource>().Play();
        audiomusic.SetActive(true);
        musicon = true;
        buttonmusic2.SetActive(false);
        buttonmusic1.SetActive(true);
        music = 0;
        PlayerPrefs.SetInt("checkmusic", music);

    }
    public void DontVolume()
    {
        GetComponent<AudioSource>().Play();
        AudioListener.volume = 0;
        volume2button.SetActive(false);
        volume1button.SetActive(true);
    }
    public void VK()
    {
        GetComponent<AudioSource>().Play();
        Application.OpenURL("https://vk.com/public181912670");
    }
    public void OnClickNo()
    {
        playbutton.SetActive(true);
        exitbutton.SetActive(true);
        settingsbutton.SetActive(true);
        GetComponent<AudioSource>().Play();
        ExitPanel.SetActive(false);
        player.SetActive(true);
    }
    public void OnClickYes()
    {
        GetComponent<AudioSource>().Play();
        Application.Quit();
    }
    public void ShopSceneOpen()
    {
        GetComponent<AudioSource>().Play();
        Application.LoadLevel("Shop");
    }
    public void Shop()
    {
        GetComponent<AudioSource>().Play();
        PanelShop.SetActive(true);
        mainfon.SetActive(false);
        settingsbutton.SetActive(false);
        playbutton.SetActive(false);
        exitbutton.SetActive(false);
        ButtonShopText.SetActive(true);
    }
    public void ExitShop()
    {
        GetComponent<AudioSource>().Play();
        Application.LoadLevel("Menu");
       // PanelShop.SetActive(false);
       // mainfon.SetActive(true);
       // settingsbutton.SetActive(true);
       // playbutton.SetActive(true);
       // exitbutton.SetActive(true);
       // ButtonShopText.SetActive(false);
    }
   public void Localizition()
    {
        GetComponent<AudioSource>().Play();
        panelLoc.SetActive(true);
        
    }
    public void ExitLocalizition()
    {
        GetComponent<AudioSource>().Play();
        panelLoc.SetActive(false);

    }
    public void Sound()
    {
        GetComponent<AudioSource>().Play();

    }
}