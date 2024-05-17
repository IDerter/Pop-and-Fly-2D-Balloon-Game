using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
[RequireComponent(typeof(TextMeshProUGUI))]
public class ShopManagerFons : MonoBehaviour
{
    public static int countlocswamp = 0;
    public static int countlocsnow = 0;
    private int index1;
    public int indexNow;
    private int score;
    private const string achiv6 = "CgkIu-eNx_IEEAIQBw";
    public TextMeshProUGUI textrainbowskin;
    public TextMeshProUGUI textblueskin;
    public TextMeshProUGUI textyellowskin;
    public TextMeshProUGUI textroyalskin;
    public TextMeshProUGUI textredskin;
    public TextMeshProUGUI textfireloc;
    public TextMeshProUGUI textsnowloc;
    public TextMeshProUGUI textswamploc;
    public TextMeshProUGUI textsoon;
    public GameObject shipswamp;
    public GameObject shipsnow;
    public GameObject shipfiery;
    public GameObject paralaxfonswamp;
    public GameObject ParalaxFonFire;
    public GameObject ParalaxFonSnow;
    public GameObject ButtonPlay;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text priceText;
    [SerializeField] private SetSkinFons[] setSkinFons;
    [SerializeField] private int[] price;
    public int SetLanguage;
    public Button buttonbuy;
    public GameObject buttonbuy1;
    private void Awake()
    {
        SetLanguage = PlayerPrefs.GetInt("KeyId");

    }
    private void Start()
    {
        index1 = setSkinFons[0].index1;
        index1 = setSkinFons[1].index1;
        index1 = setSkinFons[2].index1;
        indexNow = index1;
        countlocswamp = PlayerPrefs.GetInt("CountSwamp");
        countlocsnow = PlayerPrefs.GetInt("CountSnow");
        score = PlayerPrefs.GetInt("coin");
        scoreText.text = score.ToString();
        priceText.text = price[index1].ToString();
        Skin();
        //SetLanguage = PlayerPrefs.GetInt("KeyId");
    }
    public void Update()
    {

    }
       private void GetTheAchiv(string id)
    {
        Social.ReportProgress(id, 100.0f, (bool success) =>
        {
            if (success) print("Получено достижение" + id);
        });
    }
    public void SkinMinus()
    {
        if (index1 > 0)
            index1--;
        else
            index1 = 2;
        setSkinFons[0].UpdateSkin(index1);
        setSkinFons[1].UpdateSkin(index1);
        setSkinFons[2].UpdateSkin(index1);
        Skin();
        if (index1 == 0)
        {
            textswamploc.enabled = true;
        }
        else
          if (index1 != 0)
        {
            textswamploc.enabled = false;
        }
        if (index1 == 1)
        {
            textsnowloc.enabled = true;
        }
        else
         if (index1 != 1)
        {
            textsnowloc.enabled = false;
        }
        if (index1 == 2)
        {
            textfireloc.enabled = true;
            buttonbuy.GetComponent<Button>().enabled = false;
            buttonbuy1.SetActive(false);
            textsoon.enabled = true;
        }
        else
         if (index1 != 2)
        {
            textfireloc.enabled = false;
            buttonbuy.GetComponent<Button>().enabled = true;
            textsoon.enabled = false;
            buttonbuy1.SetActive(true);
        }
        //PlayerPrefs.SetInt("index", index);
    }
    public void SkinPlus()
    {
        ButtonPlay.SetActive(false);
        if (index1 < 2)
            index1++;
        else
            index1 = 0;
        setSkinFons[0].UpdateSkin(index1);
        setSkinFons[1].UpdateSkin(index1);
        setSkinFons[2].UpdateSkin(index1);
        Skin();
        if (index1 == 0)
        {
            textswamploc.enabled = true;
        }
        else
          if (index1 != 0)
        {
            textswamploc.enabled = false;
        }
        if (index1 == 1)
        {
            textsnowloc.enabled = true;
        }
        else
         if (index1 != 1)
        {
            textsnowloc.enabled = false;
        }
        if (index1 == 2)
        {
            textfireloc.enabled = true;
            buttonbuy.GetComponent<Button>().enabled = false;
            textsoon.enabled = true;
            buttonbuy1.SetActive(false);
        }
        else
         if (index1 != 2)
        {
            textfireloc.enabled = false;
            buttonbuy.GetComponent<Button>().enabled = true;
            textsoon.enabled = false;
            buttonbuy1.SetActive(true);
        }
        //PlayerPrefs.SetInt("index", index);
    }
    /*private void DetectionPriceSkin()
    {
        if (score >= price[index])
            priceText.color = Color.green;
        else if (score < price[index])
            priceText.color = Color.red;

    }
    */
    private void Skin()
    {
        setSkinFons[0].UpdateSkin(index1);
        setSkinFons[1].UpdateSkin(index1);
        setSkinFons[2].UpdateSkin(index1);
        scoreText.text = score.ToString();
        priceText.text = price[index1].ToString();
        if (index1 == 0)
        {
            textswamploc.enabled = true;
        }
        else
          if (index1 != 0)
        {
            textswamploc.enabled = false;

        }
        if (index1 == 1)
        {
            textsnowloc.enabled = true;
        }
        else
         if (index1 != 1)
        {
            textsnowloc.enabled = false;
        }
        if (index1 == 2)
        {
            textfireloc.enabled = true;
            buttonbuy.GetComponent<Button>().enabled = false;
            textsoon.enabled = true;
            buttonbuy1.SetActive(false);
        }
        else
         if (index1 != 2)
        {
            textfireloc.enabled = false;
            buttonbuy.GetComponent<Button>().enabled = true;
            textsoon.enabled = false;
            buttonbuy1.SetActive(true);
        }
        //DetectionPriceSkin();
        if (score >= price[index1]) //если хватает очков, то текст становится зеленым и скин можно купить
        {
            priceText.color = Color.green;

        }
        else if (score < price[index1]) //если не хватает очков, то текст становится красным и скин нельзя купить
        {
            priceText.color = Color.red;
        }
        if (indexNow == index1) //если игрок купил скин, то появляется надпись selection
        {
            priceText.color = Color.yellow;
            if (SetLanguage == 0)
            {
                priceText.text = "Selection";
                Debug.Log("Selection");
                
            }
            else if (SetLanguage == 1)
            {
                priceText.text = "Выбрано";
                Debug.Log("Выбрано");
            }
            else if (SetLanguage == 2)
            {
                priceText.text = "Seleccionado";
                Debug.Log("Выбрано");
            }
          
        }
        else if (PlayerPrefs.GetInt("Skin" + index1.ToString()) == 1 || index1 == 0) //если игрок купил скин или это начальньный скин
        {

            priceText.color = Color.yellow;//то текст становится желтого цвета
            if (SetLanguage == 0)
            {
                priceText.text = "Select";
            }
            else if (SetLanguage == 1)
            {
                priceText.text = "Выбрать";
            }
            else if (SetLanguage == 2)
            {
                priceText.text = "Escoger";
            }
            //и появляется надпись select
        }
    }
    public void BuySkin()
    {
       
        Skin();
        if (index1 == 0)
        {
            textswamploc.enabled = true;
            countlocswamp = 1;
            PlayerPrefs.SetInt("CountSwamp", countlocswamp);
        }
        else
             if (index1 != 0)
        {
            textswamploc.enabled = false;

        }
        if (index1 == 1)
        {
            textsnowloc.enabled = true;
            countlocsnow = 1;
            PlayerPrefs.SetInt("CountSnow", countlocsnow);
        }
        if(PlayerPrefs.GetInt("CountSwamp") ==1 && PlayerPrefs.GetInt("CountSnow") == 1)
        {
            GetTheAchiv(achiv6);
        }
        else
         if (index1 != 1)
        {
            textsnowloc.enabled = false;
        }
        if (index1 == 2)
        {
            textfireloc.enabled = true;
        }
        else
         if (index1 != 2)
        {
            textfireloc.enabled = false;
        }
        if (PlayerPrefs.GetInt("Skin" + index1.ToString()) == 1 || index1 == 0) //если скин куплен и его индекс = текущему, то
        {
            ButtonPlay.SetActive(true);
            PlayerPrefs.SetInt("index1", index1); //"Skin" + index.ToString())==1 означает что мы купили скин(1-true,0-false)
            Debug.Log("1");
            priceText.color = Color.yellow;
            if (SetLanguage == 0)
            {
                priceText.text = "Selection";
            }
            else if (SetLanguage == 1)
            {
                priceText.text = "Выбрано";
            }
            //priceText.text = "Selection";
            indexNow = index1;
        }
        else if (PlayerPrefs.GetInt("Skin" + index1.ToString()) == 0)
        {
            if (score >= price[index1])
            {
                score -= price[index1];
                Debug.Log("2");
                priceText.text = price[index1].ToString();
                PlayerPrefs.SetInt("coin", score);

                PlayerPrefs.SetInt("Skin" + index1.ToString(), 1);
            }
            Skin();
        }
    }
}
