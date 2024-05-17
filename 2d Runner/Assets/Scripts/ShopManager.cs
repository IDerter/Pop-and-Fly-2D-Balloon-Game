using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
[RequireComponent(typeof(TextMeshProUGUI))]
public class ShopManager : MonoBehaviour {
    public Image button1;
    public Image button2;
    public Image img;
    public GameObject buttonbuy1;
    public GameObject buttonbuy2;
    private int index;
    public int indexNow;
    private int score;
    private int score1;
    public GameObject[] fons;
    public TextMeshProUGUI textrainbowskin;
    public TextMeshProUGUI textblueskin;
    public TextMeshProUGUI textyellowskin;
    public TextMeshProUGUI textroyalskin;
    public TextMeshProUGUI textredskin;
    public TextMeshProUGUI textgreenskin;
    public TextMeshProUGUI textpurpleskin;
    public TextMeshProUGUI textironskin;
    public TextMeshProUGUI textsnowskin;
    public static int countopenskin = 0;
    public TextMeshProUGUI textdefaultskin;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text score1Text;
    [SerializeField] private Text priceText;
    [SerializeField] private Text priceTextyellow;
    [SerializeField] private SetSkin setSkin;
    [SerializeField] private int [] price;
    [SerializeField] private int [] priceyellow;
    private const string achiv5 = "CgkIu-eNx_IEEAIQBg";
    public int SetLanguage;
    public GameObject icongreen;
    public GameObject iconyellow;
    private void Awake()
    {
        SetLanguage = PlayerPrefs.GetInt("KeyId");

    }
    private void Start()
    {
        index = setSkin.index;
        indexNow = index;
        countopenskin = PlayerPrefs.GetInt("CountSkin");
        score = PlayerPrefs.GetInt("coin");
        scoreText.text = score.ToString();
        score1 = PlayerPrefs.GetInt("coin1");
        score1Text.text = score1.ToString();
        priceText.text = price[index].ToString();
        //priceTextyellow.text = priceyellow[index].ToString();
        Skin();
        //SetLanguage = PlayerPrefs.GetInt("KeyId");
    }
    private void GetTheAchiv(string id)
    {
        Social.ReportProgress(id, 100.0f, (bool success) =>
        {
            if (success) print("Получено достижение" + id);
        });
    }
    public void Update()
    {
       
    }
    public void SkinMinus()
    {
        if (index > 0)
            index--;
        else
            index = 14;
        setSkin.UpdateSkin(index);
        Skin();
        if (index == 0)
        {
            fons[0].SetActive(false);
            fons[1].SetActive(false);
            fons[2].SetActive(false);
            fons[3].SetActive(true);
            fons[4].SetActive(false);
            textdefaultskin.enabled = true;
        }
        else if (index != 0)
        {
            textdefaultskin.enabled = false;
        }
        if (index == 1)
        {
            fons[0].SetActive(true);
            fons[1].SetActive(false);
            fons[2].SetActive(false);
            fons[3].SetActive(false);
            fons[4].SetActive(false);
            textdefaultskin.enabled = true;
        }
        if (index == 2)
        {
            fons[0].SetActive(false);
            fons[1].SetActive(false);
            fons[2].SetActive(false);
            fons[3].SetActive(false);
            fons[4].SetActive(true);
            textdefaultskin.enabled = true;
        }
        if (index == 3)
        {
            fons[0].SetActive(false);
            fons[1].SetActive(false);
            fons[2].SetActive(true);
            fons[3].SetActive(false);
            fons[4].SetActive(false);
            textdefaultskin.enabled = true;
        }
        if (index == 4)
        {
            fons[0].SetActive(false);
            fons[1].SetActive(false);
            fons[2].SetActive(false);
            fons[3].SetActive(true);
            fons[4].SetActive(false);
            textdefaultskin.enabled = true;
        }
        if (index == 5)
        {
            fons[0].SetActive(false);
            fons[1].SetActive(true);
            fons[2].SetActive(false);
            fons[3].SetActive(false);
            fons[4].SetActive(false);
            textdefaultskin.enabled = true;
        }
        if (index == 6)
        {
            textrainbowskin.enabled = true;
            fons[0].SetActive(false);
            fons[1].SetActive(false);
            fons[2].SetActive(true);
            fons[3].SetActive(false);
            fons[4].SetActive(false);
            textdefaultskin.enabled = false;
        }
        else
           if (index != 6)
        {
            textrainbowskin.enabled = false;
        }
        if (index == 7)
        {
            textgreenskin.enabled = true;
            fons[0].SetActive(true);
            fons[1].SetActive(false);
            fons[2].SetActive(false);
            fons[3].SetActive(false);
            fons[4].SetActive(false);

        }
        else
            if (index != 7)
        {
            textgreenskin.enabled = false;
        }
        if (index == 8)
        {
            textpurpleskin.enabled = true;
            fons[0].SetActive(false);
            fons[1].SetActive(true);
            fons[2].SetActive(false);
            fons[3].SetActive(false);
            fons[4].SetActive(false);
        }
        else
            if (index != 8)
        {
            textpurpleskin.enabled = false;
        }
        if (index == 9)
        {
            textblueskin.enabled = true;
            fons[0].SetActive(false);
            fons[1].SetActive(false);
            fons[2].SetActive(true);
            fons[3].SetActive(false);
            fons[4].SetActive(false);
        }
        else if (index != 9)
        {
            textblueskin.enabled = false;
        }
        if (index == 10)
        {
            textyellowskin.enabled = true;
            fons[0].SetActive(false);
            fons[1].SetActive(false);
            fons[2].SetActive(false);
            fons[3].SetActive(true);
            fons[4].SetActive(false);
        }
        else if (index != 10)
        {
            textyellowskin.enabled = false;
        }
        if (index == 11)
        {
            textroyalskin.enabled = true;
            fons[0].SetActive(false);
            fons[1].SetActive(false);
            fons[2].SetActive(false);
            fons[3].SetActive(true);
            fons[4].SetActive(false);
        }
        else
            if (index != 11)
        {
            textroyalskin.enabled = false;
        }
        if (index == 12)
        {
            textredskin.enabled = true;
            fons[0].SetActive(false);
            fons[1].SetActive(false);
            fons[2].SetActive(false);
            fons[3].SetActive(false);
            fons[4].SetActive(true);
        }
        else
        if (index != 12)
        {
            textredskin.enabled = false;
        }
        if (index == 13)
        {
            textironskin.enabled = true;
            fons[0].SetActive(false);
            fons[1].SetActive(false);
            fons[2].SetActive(false);
            fons[3].SetActive(true);
            fons[4].SetActive(false);
            textdefaultskin.enabled = false;
        }
        else
       if (index != 13)
        {
            textironskin.enabled = false;
        }
        if (index == 14)
        {
            textsnowskin.enabled = true;
            iconyellow.SetActive(true);
            icongreen.SetActive(false);
        }
        else
       if (index != 14)
        {
            textsnowskin.enabled = false;
            iconyellow.SetActive(false);
        }
        //PlayerPrefs.SetInt("index", index);
    }
    public void SkinPlus()
    {
        if (index < 14)
            index++;
        else
            index = 0;
        setSkin.UpdateSkin(index);
        Skin();
        if (index == 0)
        {
            fons[0].SetActive(false);
            fons[1].SetActive(false);
            fons[2].SetActive(false);
            fons[3].SetActive(true);
            fons[4].SetActive(false);
            textdefaultskin.enabled = true;
        }
        else if (index != 0)
        {
            textdefaultskin.enabled = false;
        }
        if (index == 1)
        {
            fons[0].SetActive(true);
            fons[1].SetActive(false);
            fons[2].SetActive(false);
            fons[3].SetActive(false);
            fons[4].SetActive(false);
            textdefaultskin.enabled = true;
        }
        if (index == 2)
        {
            fons[0].SetActive(false);
            fons[1].SetActive(false);
            fons[2].SetActive(false);
            fons[3].SetActive(false);
            fons[4].SetActive(true);
            textdefaultskin.enabled = true;
        }
        if (index == 3)
        {
            fons[0].SetActive(false);
            fons[1].SetActive(false);
            fons[2].SetActive(true);
            fons[3].SetActive(false);
            fons[4].SetActive(false);
            textdefaultskin.enabled = true;
        }
        if (index == 4)
        {
            fons[0].SetActive(false);
            fons[1].SetActive(false);
            fons[2].SetActive(false);
            fons[3].SetActive(true);
            fons[4].SetActive(false);
            textdefaultskin.enabled = true;
        }
        if (index == 5)
        {
            fons[0].SetActive(false);
            fons[1].SetActive(true);
            fons[2].SetActive(false);
            fons[3].SetActive(false);
            fons[4].SetActive(false);
            textdefaultskin.enabled = true;
        }
        if (index == 6)
        {
            textrainbowskin.enabled = true;
            fons[0].SetActive(false);
            fons[1].SetActive(false);
            fons[2].SetActive(true);
            fons[3].SetActive(false);
            fons[4].SetActive(false);
            textdefaultskin.enabled = false;
        }
        else
           if (index != 6)
        {
            textrainbowskin.enabled = false;
        }
        if (index == 7)
        {
            textgreenskin.enabled = true;
            fons[0].SetActive(true);
            fons[1].SetActive(false);
            fons[2].SetActive(false);
            fons[3].SetActive(false);
            fons[4].SetActive(false);

        }
        else
            if (index != 7)
        {
            textgreenskin.enabled = false;
        }
        if (index == 8)
        {
            textpurpleskin.enabled = true;
            fons[0].SetActive(false);
            fons[1].SetActive(true);
            fons[2].SetActive(false);
            fons[3].SetActive(false);
            fons[4].SetActive(false);
        }
        else
            if (index != 8)
        {
            textpurpleskin.enabled = false;
        }
        if (index == 9)
        {
            textblueskin.enabled = true;
            fons[0].SetActive(false);
            fons[1].SetActive(false);
            fons[2].SetActive(true);
            fons[3].SetActive(false);
            fons[4].SetActive(false);
        }
        else if (index != 9)
        {
            textblueskin.enabled = false;
        }
        if (index == 10)
        {
            textyellowskin.enabled = true;
            fons[0].SetActive(false);
            fons[1].SetActive(false);
            fons[2].SetActive(false);
            fons[3].SetActive(true);
            fons[4].SetActive(false);
        }
        else if (index != 10)
        {
            textyellowskin.enabled = false;
        }
        if (index == 11)
        {
            textroyalskin.enabled = true;
            fons[0].SetActive(false);
            fons[1].SetActive(false);
            fons[2].SetActive(false);
            fons[3].SetActive(true);
            fons[4].SetActive(false);
        }
        else
            if (index != 11)
        {
            textroyalskin.enabled = false;
        }
        if (index == 12)
        {
            textredskin.enabled = true;
            fons[0].SetActive(false);
            fons[1].SetActive(false);
            fons[2].SetActive(false);
            fons[3].SetActive(false);
            fons[4].SetActive(true);
        }
        else
        if (index != 12)
        {
            textredskin.enabled = false;
        }
        if (index == 13)
        {
            textironskin.enabled = true;
            fons[0].SetActive(false);
            fons[1].SetActive(false);
            fons[2].SetActive(false);
            fons[3].SetActive(true);
            fons[4].SetActive(false);
            textdefaultskin.enabled = false;
        }
        else
       if (index != 13)
        {
            textironskin.enabled = false;

        }
        if (index == 14)
        {
            textsnowskin.enabled = true;
            iconyellow.SetActive(true);
            icongreen.SetActive(false);
        }
        else
      if (index != 14)
        {
            textsnowskin.enabled = false;
            iconyellow.SetActive(false);
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
        setSkin.UpdateSkin(index);
        scoreText.text = score.ToString();
        priceText.text = price[index].ToString();
        //DetectionPriceSkin();
        if ((score >= price[index] && index != 14) || score1 >= price[index]) //если хватает очков, то текст становится зеленым и скин можно купить
        {
            priceText.color = Color.green;
            img.sprite = Resources.Load<Sprite>("button1");
            icongreen.SetActive(true);
        }
        else if (score < price[index]) //если не хватает очков, то текст становится красным и скин нельзя купить
        {
            priceText.color = Color.red;
            icongreen.SetActive(true);
        }
        else if (score1 < price[index]) //если не хватает очков, то текст становится красным и скин нельзя купить
        {
            priceText.color = Color.red;
            icongreen.SetActive(false);
        }
        if (indexNow==index ) //если игрок купил скин, то появляется надпись selection
        {
            icongreen.SetActive(false);
            priceText.color = Color.yellow;
            img.sprite = Resources.Load<Sprite>("button2");
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
            if (index == 0)
            {
                fons[0].SetActive(false);
                fons[1].SetActive(false);
                fons[2].SetActive(false);
                fons[3].SetActive(true);
                fons[4].SetActive(false);
                textdefaultskin.enabled = true;
            }
            else if (index !=0)
            {
                textdefaultskin.enabled = false;
            }
            if (index == 1)
            {
                fons[0].SetActive(true);
                fons[1].SetActive(false);
                fons[2].SetActive(false);
                fons[3].SetActive(false);
                fons[4].SetActive(false);
                textdefaultskin.enabled = true;
            }
            if (index == 2)
            {
                fons[0].SetActive(false);
                fons[1].SetActive(false);
                fons[2].SetActive(false);
                fons[3].SetActive(false);
                fons[4].SetActive(true);
                textdefaultskin.enabled = true;
            }
            if (index == 3)
            {
                fons[0].SetActive(false);
                fons[1].SetActive(false);
                fons[2].SetActive(true);
                fons[3].SetActive(false);
                fons[4].SetActive(false);
                textdefaultskin.enabled = true;
            }
            if (index == 4)
            {
                fons[0].SetActive(false);
                fons[1].SetActive(false);
                fons[2].SetActive(false);
                fons[3].SetActive(true);
                fons[4].SetActive(false);
                textdefaultskin.enabled = true;
            }
            if (index == 5)
            {
                fons[0].SetActive(false);
                fons[1].SetActive(true);
                fons[2].SetActive(false);
                fons[3].SetActive(false);
                fons[4].SetActive(false);
                textdefaultskin.enabled = true;
            }
            if (index == 6)
            {
                textrainbowskin.enabled = true;
                fons[0].SetActive(false);
                fons[1].SetActive(false);
                fons[2].SetActive(true);
                fons[3].SetActive(false);
                fons[4].SetActive(false);
                textdefaultskin.enabled = false;
            }
            else
               if (index != 6)
            {
                textrainbowskin.enabled = false;
            }
            if (index == 7)
            {
                textgreenskin.enabled = true;
                fons[0].SetActive(true);
                fons[1].SetActive(false);
                fons[2].SetActive(false);
                fons[3].SetActive(false);
                fons[4].SetActive(false);

            }
            else
                if (index != 7)
            {
                textgreenskin.enabled = false;
            }
            if (index == 8)
            {
                textpurpleskin.enabled = true;
                fons[0].SetActive(false);
                fons[1].SetActive(true);
                fons[2].SetActive(false);
                fons[3].SetActive(false);
                fons[4].SetActive(false);
            }
            else
                if (index != 8)
            {
                textpurpleskin.enabled = false;
            }
            if (index == 9)
            {
                textblueskin.enabled = true;
                fons[0].SetActive(false);
                fons[1].SetActive(false);
                fons[2].SetActive(true);
                fons[3].SetActive(false);
                fons[4].SetActive(false);
            }
            else if (index != 9)
            {
                textblueskin.enabled = false;
            }
            if (index == 10)
            {
                textyellowskin.enabled = true;
                fons[0].SetActive(false);
                fons[1].SetActive(false);
                fons[2].SetActive(false);
                fons[3].SetActive(true);
                fons[4].SetActive(false);
            }
            else if (index != 10)
            {
                textyellowskin.enabled = false;
            }
            if (index == 11)
            {
                textroyalskin.enabled = true;
                fons[0].SetActive(false);
                fons[1].SetActive(false);
                fons[2].SetActive(false);
                fons[3].SetActive(true);
                fons[4].SetActive(false);
            }
            else
                if (index != 11)
            {
                textroyalskin.enabled = false;
            }
            if (index == 12)
            {
                textredskin.enabled = true;
                fons[0].SetActive(false);
                fons[1].SetActive(false);
                fons[2].SetActive(false);
                fons[3].SetActive(false);
                fons[4].SetActive(true);
            }
            else
            if (index != 12)
            {
                textredskin.enabled = false;
            }
            if (index == 13)
            {
                textdefaultskin.enabled = false;
                textironskin.enabled = true;
                fons[0].SetActive(false);
                fons[1].SetActive(false);
                fons[2].SetActive(false);
                fons[3].SetActive(true);
                fons[4].SetActive(false);
            }
            else
           if (index != 13)
            {
                textironskin.enabled = false;
            }
            if (index == 14)
            {
                textsnowskin.enabled = true;
                iconyellow.SetActive(true);
                icongreen.SetActive(false);
            }
            else
            if (index != 14)
            {
                textsnowskin.enabled = false;
                iconyellow.SetActive(false);
            }
            //PlayerPrefs.SetInt("index", index);
        }
        else if (PlayerPrefs.GetInt("Skin" + index.ToString()) == 1 ||  index ==0) //если игрок купил скин или это начальньный скин
        {
            priceText.color = Color.yellow;//то текст становится желтого цвета
            icongreen.SetActive(false);
            img.sprite = Resources.Load<Sprite>("button2");
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
    public void BuySkin ()
    {
       // if (index == 1&&buyskin == false)
      //  {
      //      countopenskin += 1;
      //      buyskin = true;
     //   }
      //  if (index == 2)
     //   {
     //       countopenskin += 1;
    //    }
    //    PlayerPrefs.SetInt("CountSkin", countopenskin);
     //   if (PlayerPrefs.GetInt("CountSkin") == 13)
     //   {
     //       GetTheAchiv(achiv5);
     //   }
        img.sprite = Resources.Load<Sprite>("button2");
        Skin();
        //button1.GetComponent<Image>().enabled = false;
        //button2.GetComponent<Image>().enabled = true;
   
        if (PlayerPrefs.GetInt("Skin" + index.ToString())==1 || index == 0) //если скин куплен и его индекс = текущему, то
        {
            PlayerPrefs.SetInt("index", index); //"Skin" + index.ToString())==1 означает что мы купили скин(1-true,0-false)
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
            indexNow = index;
        }
      else if (PlayerPrefs.GetInt("Skin" + index.ToString()) == 0)
        {
            if (score >= price[index] &&index!=14)
            {
                score -= price[index];
                Debug.Log("2");
                priceText.text = price[index].ToString();
                PlayerPrefs.SetInt("coin", score);

                PlayerPrefs.SetInt("Skin"+ index.ToString(),1);
                countopenskin += price[index];
                PlayerPrefs.SetInt("CountSkin", countopenskin);
                if(PlayerPrefs.GetInt("CountSkin") == 14)
                    {
                    GetTheAchiv(achiv5);
                    Debug.Log("Моднявый!!!");
                }
            }
            if (score1 >= price[index] &&index==14)
            {
                score1 -= price[index];
                Debug.Log("3");
                priceText.text = price[index].ToString();
                PlayerPrefs.SetInt("coin1", score1);

                PlayerPrefs.SetInt("Skin" + index.ToString(), 1);
                countopenskin += price[index];
                PlayerPrefs.SetInt("CountSkin", countopenskin);
                if (PlayerPrefs.GetInt("CountSkin") == 14)
                {
                    GetTheAchiv(achiv5);
                    Debug.Log("Моднявый!!!");
                }
            }
            Skin();
        }
    }
}
