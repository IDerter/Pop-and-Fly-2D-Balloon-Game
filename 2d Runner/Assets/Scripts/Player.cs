using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Advertisements;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
public class Player : MonoBehaviour {
    public bool ismoney = false;
    public Transform player;
    private Vector2 targetPos;
    public float Yincrement;
    public bool IsDamage = true ;
    public int health = 1;
    public float speed;
    public float maxHeight;
    public float minHeight;
    public int score = 0;
    public int scoremoney = 0;
    public int directionInput;
    public GameObject effect;
    public GameManager gameManager;
    public Rigidbody2D rb2d;
    public CapsuleCollider2D collider2d;
    public GameObject magnit;
    public GameObject shipswamp;
    public GameObject shipsnow;
    public GameObject shipfiery;
    public PointEffector2D point;
    public GameObject[] SpawnBlueEnemy;
    public GameObject[] SpawnRedEnemy;
    public GameObject[] SpawnGreenScore;
    public GameObject spawnershit;
    public GameObject spawnerexplosion;
    public GameObject spawnermagnit;
    public GameObject SpawnerYellowBallon;
    public GameObject[] SpawnerYellowTeleportEnemy;
    public GameObject buttonskip;
    public GameObject areol;
    public int firstclick = 0;
    public Paralax script;
    public Paralax script1;
    public GameObject panel1;
    public GameObject panel2;
    public AudioSource sound;
    public AudioSource soundenemy;
    public GameObject abilkamagnit;
    public GameObject magnitobject;
    public GameObject shitobject;
    public GameObject MagnitPoint;
    public int coin;
    public int coin1;
    public GameObject texttap;
    public GameObject textguide;
    public GameObject textironenemy;
    public GameObject textmagnitandshit;
    public GameObject textendlearnlvl;
    public GameObject textqustionlearnlvl;
    public int firstgame = 1;
    public int firstgameеtest = 1;
    public int firstgameеtest1 = 1;
    public int firstgameеtest2 = 1;
    public int newgame = 1;
    public static int test = 0;
    public static int firsttest = 0;
    public GameObject buttonplay;
    public GameObject iconShield;
    public GameObject iconMagnit;
    public GameObject buttonleft;
    public GameObject buttonright;
    public bool checkadvertisiong = true;
    public GameObject buttonreborn;
    public GameObject buttoncoin2x;
    public static int a = 0;
    public static int bestscore = 0;
    public GameObject table;
    public bool textlearn = true;
    public GameObject buttoncontinue;
    public GameObject buttoncontinue10score;
    public GameObject buttoncontinue1score;
    public SetSkin scriptskin;
    public GameObject lefteffect;
    public GameObject righteffect;
    public GameObject leftfinger;
    public GameObject rightfinger;
    public SpriteRenderer player1;
    public SpriteRenderer saw;
    public Spawner scriptspanwer;
    public SpawnerYellowBallon[] scriptspawneryellowballon;
    public SpriteRenderer mainfon;
    public Rigidbody2D rb;
    public GameObject ship1;
    public GameObject ship2;
    public GameObject ship1child;
    public GameObject ship2child;
    public GameObject allship;
    public bool IsShip = true;
    public int counttext = 0;
    public SetSkinFons setskinfons;
    public GameObject paralaxfonswamp;
    public GameObject ParalaxFonFire;
    public GameObject ParalaxFonSnow;
    public Paralax snow;
    public Paralax fire;
    public Paralax swamp;
    public GameObject SpawnerSnowStones;
    public GameObject SpawnerFireStones;
    public GameObject spawnerstones;
    public GameObject allspawnsswamp;
    public GameObject allspawnssnow;
    public GameObject allspawnsfire;
    public static int countlocstone1 = 0;
    public static int countlocstone2 = 0;
    public static int countlocstone3 = 0;
    public static int countenemyshield1 = 0;
    public static int countenemyshield2 = 0;
    public static int countenemyshield3 = 0;
    public static int countenemyshield4 = 0;
    public static int firstloc = 0;
    public static int secondloc = 0;
    public static int thirdloc = 0;
    private const string achiv1 = "CgkIu-eNx_IEEAIQAQ";
    private const string achiv2 = "CgkIu-eNx_IEEAIQAw";
    private const string achiv3 = "CgkIu-eNx_IEEAIQBA";
    private const string achiv4 = "CgkIu-eNx_IEEAIQBQ";
    private const string achiv5 = "CgkIu-eNx_IEEAIQBg";
    private const string achiv7 = "CgkIu-eNx_IEEAIQCA";
    private const string achiv8 = "CgkIu-eNx_IEEAIQCg";
    private const string achiv9 = "CgkIu-eNx_IEEAIQCw";
    private const string leaderboard = "CgkIu-eNx_IEEAIQAA";
   
    void Awake()
    {
        //newgame = PlayerPrefs.GetInt("firstgame");
        a = PlayerPrefs.GetInt("checktext");
        bestscore = PlayerPrefs.GetInt("Score0");

    }
    void Start () {
       if (scriptskin.index == 14)
        {
            health = 2;
        }
        countlocstone1 = PlayerPrefs.GetInt("CountStone1");
        countlocstone2 = PlayerPrefs.GetInt("CountStone2");
        countlocstone3 = PlayerPrefs.GetInt("CountStone3");
        countenemyshield1 = PlayerPrefs.GetInt("CountEnemy1");
        countenemyshield2 = PlayerPrefs.GetInt("CountEnemy2");
        countenemyshield3 = PlayerPrefs.GetInt("CountEnemy3");
        countenemyshield4 = PlayerPrefs.GetInt("CountEnemy4");
        if (setskinfons.index1 == 0)
        {
            paralaxfonswamp.SetActive(true);
            allspawnssnow.SetActive(false);

        }
        if (setskinfons.index1 == 1)
        {
            ParalaxFonSnow.SetActive(true);
            allspawnssnow.SetActive(true);
            allspawnsswamp.SetActive(false);
        }
        if (setskinfons.index1 == 2)
        {
            ParalaxFonFire.SetActive(true);
        }
        rb.GetComponent<Rigidbody2D>();
        //table.SetActive(true);
        if ((test >=1 &&firsttest == 1 )||firstgame ==0)
        {
            newgame = PlayerPrefs.GetInt("firstgame");
            PlayerPrefs.SetInt("firstgame", firstgame);
            
        }
        coin = PlayerPrefs.GetInt("coin");
        coin1 = PlayerPrefs.GetInt("coin1");
        rb2d = GetComponent<Rigidbody2D>();
        //newgame = PlayerPrefs.GetInt("firstgame");
        IsDamage = true;
        if(newgame ==0 || a!=0 )
        {
            texttap.SetActive(false);
            table.SetActive(false);
            leftfinger.SetActive(false);
            rightfinger.SetActive(false);

        }
        Time.timeScale = 1f;
        test++;
        checkadvertisiong = true;
        if (Advertisement.isSupported) Advertisement.Initialize("3789551", true);
        //newgame = PlayerPrefs.GetInt("firstgame");
    }
    private void GetTheAchiv(string id)
    {
        Social.ReportProgress(id, 100.0f, (bool success) =>
          {
              if (success) print("Получено достижение" + id);
          });
    }
    void TimeSleep()
    {
        mainfon.GetComponent<SpriteRenderer>().sortingOrder = 6;
    }
    void ShipTrigger()
    {
        ship1.GetComponent<BoxCollider2D>().enabled = true;
        ship2.GetComponent<BoxCollider2D>().enabled = true;
        ship1child.SetActive(true);
        ship2child.SetActive(true);
    }
    public void achivOpen()
    {
        Social.ShowAchievementsUI();
    }
    public void leaderboardOpen()
    {
        Social.ShowLeaderboardUI();
    }
    // Update is called once per frame
    private void Update()
    {
        PlayerPrefs.SetInt("coin", coin);
        PlayerPrefs.SetInt("coin1", coin1);
        PlayerPrefs.SetInt("checktext", a);
        if (health <= 0)
        {
           
            Invoke("TimeSleep", 0.5f);
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            magnitobject.SetActive(false);
            shitobject.SetActive(false);
            IsDamage = false;
            saw.GetComponent<SpriteRenderer>().enabled = false;
            player1.GetComponent<SpriteRenderer>().enabled = false;
            scriptspanwer.enabled = false;
            for (int i =0;i<7;i++)
            {
                scriptspawneryellowballon[i].enabled = false;
            }
            Destroy(GameObject.FindGameObjectWithTag("Enemy"), 0.2f);
            Destroy(GameObject.FindGameObjectWithTag("Score"), 0.2f);
            Destroy(GameObject.FindGameObjectWithTag("DiagonalEnemy"), 0.2f);
            Destroy(GameObject.FindGameObjectWithTag("EnemyTeleport"), 0.2f);
            PlayerPrefs.SetInt("Score0",bestscore);
            if (score>= PlayerPrefs.GetInt("Score0"))
            {
                PlayerPrefs.SetInt("Score0", bestscore);
                Social.ReportScore(score, leaderboard, (bool success) =>
                  {
                      if (success) print("удачно добавлен в таблицу лидеров!");
                  });
            }
            if (score >= 10)
            {
                firstgame = 0;
                firsttest = 1;
                PlayerPrefs.SetInt("firstgame", firstgame);
                Debug.Log("firstgame");

            }
            if(scoremoney >=30)
            {
                GetTheAchiv(achiv9);
            }
            if (score <= 9)
            {
                if (newgame == 1 && a == 0)
                {
                    GetTheAchiv(achiv3);
                }
            }
            Invoke("ReloadLevel", 0.5f);
        }
        if (score >= 500)
        {
            GetTheAchiv(achiv4);
        }
        if (score >= 30)
        {
            if (setskinfons.index1 == 0|| setskinfons.index1 == 2)
            {
                SpawnBlueEnemy[0].SetActive(true);
            }
            else if (setskinfons.index1 == 1)
            {
                SpawnBlueEnemy[1].SetActive(true);
            }
        }

        if (score >= 50)
        {
            if (setskinfons.index1 == 0 || setskinfons.index1 == 2)
            {
                SpawnerYellowTeleportEnemy[0].SetActive(true);
            }
            else if (setskinfons.index1 == 1)
            {
                SpawnerYellowTeleportEnemy[1].SetActive(true);
            }
            
        }
        if (score >= 120)
        {
            spawnerexplosion.SetActive(true);
        }
        if (scriptskin.index == 11)
        {
            Debug.Log("magnit");
            point.enabled = true;
            collider2d.enabled = true;
            magnitobject.SetActive(true);
            abilkamagnit.SetActive(true);
        }
        if (scriptskin.index == 8)
        {
            speed = 8f;
        }
        if (score >= 10)
        {
            if (newgame == 1 && a==0)
            {
                firstgame = 0;
                firsttest = 1;
                PlayerPrefs.SetInt("firstgame", firstgame);
                newgame = PlayerPrefs.GetInt("firstgame",firstgame);
                a = 1;
                buttonleft.SetActive(false);
                buttonright.SetActive(false);
                textqustionlearnlvl.SetActive(true);
                table.SetActive(true);
                buttonplay.SetActive(true);
                GetTheAchiv(achiv1);
                Invoke("Timescale", 0.2f);
            }
        }
        if (score >=100)
        {
            ship1.SetActive(true);
            ship2.SetActive(true);
            Invoke("ShipTrigger",2f);
            if (setskinfons.index1 == 0)
            {
                shipswamp.SetActive(true);
                Debug.Log("1");
            }
            if (setskinfons.index1 == 1)
            {
                shipsnow.SetActive(true);
                Debug.Log("2");
            }
            if (setskinfons.index1 == 2)
            {
                shipfiery.SetActive(true);
                Debug.Log("3");
            }
        }
        if (PlayerPrefs.GetInt("CountStone1") == 1 && PlayerPrefs.GetInt("CountStone2") == 1 && PlayerPrefs.GetInt("CountStone3") == 1)
        {
            GetTheAchiv(achiv7);
        }
        if (score >= 90)
        {
            if (setskinfons.index1 == 0)
            {
                spawnerstones.SetActive(true);
            }
            if (setskinfons.index1 == 1)
            {
                SpawnerSnowStones.SetActive(true);
            }
            if (setskinfons.index1 == 2)
            {
                SpawnerFireStones.SetActive(true);
            }
            
        }
        if (score >= 4)
        {

            spawnershit.SetActive(true);
            spawnermagnit.SetActive(true);
            if (textendlearnlvl.activeSelf == true && newgame == 1)
            {
                Invoke("TextLearnLvlOff", 5f);
            }
        }

    }
    public void Reborn()
    {
        if (health<=0 && Advertisement.IsReady("rewardedVideo")&&checkadvertisiong == true)
        {

            ShowOptions options = new ShowOptions();
            options.resultCallback = Hand;
            Advertisement.Show("rewardedVideo", options);
            checkadvertisiong = false;
            buttonreborn.SetActive(false);
            Invoke("ButtonCoin2x", 3f);
            Debug.Log("rewarded");
        }
        if (!Advertisement.IsReady("rewardedVideo") && checkadvertisiong == true && health <= 0 )
        {
            Invoke("ReloadLevel", 0f);
        }

    }
    public void ButtonCoin2x()
    {
        buttoncoin2x.SetActive(true);
        
    }
    public void Coin()
    {
        if (health <= 0 && Advertisement.IsReady("rewardedVideo") && checkadvertisiong == false)
        {
            
            ShowOptions options = new ShowOptions();
            options.resultCallback = HandCoin;
            Advertisement.Show("rewardedVideo", options);
            
            checkadvertisiong = false;
            //buttoncoin2x.SetActive(false);

        }
        if (!Advertisement.IsReady("rewardedVideo") && health <= 0)
        {
            Invoke("ReloadLevel",0f);
        }

    }
    void Hand(ShowResult result)
    {
        if(result == ShowResult.Finished)
        {
            IsDamage = true;
            if (scriptskin.index == 14)
            {
                health = 2;
            }
            else
            {
                health = 1;
            }
            gameManager.GameStart();
            Debug.Log("Finished");
        }
        else if (result == ShowResult.Skipped)
        {
            Invoke("BreakAdvertising", 0f);
            Debug.Log("Skipped");
        }
        else if (result == ShowResult.Failed)
        {
            Invoke("BreakAdvertising", 0f);
            Debug.Log("Failed");
        }
    }
    void HandCoin(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            IsDamage = true;
            buttoncoin2x.SetActive(false);
            score = score * 2;
            coin += score;
            //PlayerPrefs.SetInt("coin",coin);
            Debug.Log("Произошло сохранение удвоенного результата!");
            if (score >= PlayerPrefs.GetInt("Score0"))
            {
                PlayerPrefs.SetInt("Score0", bestscore);
                Social.ReportScore(score, leaderboard, (bool success) =>
                {
                    if (success) print("удачно добавлен в таблицу лидеров!");
                });
            }
            Debug.Log("Finished");
        }
        else if (result == ShowResult.Skipped)
        {
            Invoke("BreakAdvertising", 0f);
            Debug.Log("Skipped");
        }
        else if (result == ShowResult.Failed)
        {
            Invoke("BreakAdvertising", 0f);
            Debug.Log("Failed");
        }
    }
    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0); //velocity - Линейная скорость твердого тела.
    }
    public void Timescale()
    {
        Time.timeScale = 0f;
    }
   public void SkipTutorial()
    {
        firstgame = 0;
        firsttest = 1;
        PlayerPrefs.SetInt("firstgame", firstgame);
        Debug.Log("firstgame");
        textguide.SetActive(false);
        table.SetActive(false);
        buttoncontinue1score.SetActive(false);
        Time.timeScale = 1f;
        buttonskip.SetActive(false);
    }
    public void ButtonUp()
    {
        if(firstclick==0)
        {
            if (setskinfons.index1 == 0)
            {
                swamp.enabled = true;
            }
            if (setskinfons.index1 == 1)
            {
                snow.enabled = true;
            }
            if (setskinfons.index1 == 2)
            {
                fire.enabled = true;
            }
            firstclick = 1;
            if (setskinfons.index1 == 0 || setskinfons.index1 == 2)
            {
                SpawnRedEnemy[0].SetActive(true);
            }
            else if (setskinfons.index1 == 1)
            {
                SpawnRedEnemy[1].SetActive(true);
            }
            if (setskinfons.index1 == 0 || setskinfons.index1 == 2)
            {
                SpawnGreenScore[0].SetActive(true);
            }
            else if (setskinfons.index1 == 1)
            {
                SpawnGreenScore[1].SetActive(true);
            }
            texttap.SetActive(false);
            table.SetActive(false);
            if (newgame == 1 && a == 0 && firstgameеtest1 == 1)
            {
                script.enabled = true;
                script1.enabled = true;
                textguide.SetActive(true);
                table.SetActive(true);
                buttoncontinue1score.SetActive(true);
                Time.timeScale = 0f;
                buttonskip.SetActive(true);
            }
            script1.enabled = true;
            script.enabled = true;
            panel1.SetActive(false);
            panel2.SetActive(false);
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
            Instantiate(sound, transform.position, Quaternion.identity);
            leftfinger.SetActive(false);
            rightfinger.SetActive(false);
        }
        if (firstclick == 1)
            if (setskinfons.index1 == 0 || setskinfons.index1 == 2)
            {
                SpawnRedEnemy[0].SetActive(true);
            }
            else if (setskinfons.index1 == 1)
            {
                SpawnRedEnemy[1].SetActive(true);
            }
            if (setskinfons.index1 == 0 || setskinfons.index1 == 2)
            {
                SpawnGreenScore[0].SetActive(true);
            }
            else if (setskinfons.index1 == 1)
            {
                SpawnGreenScore[1].SetActive(true);
            }
        //righteffect.SetActive(true);
        //Invoke("TimeEffectRight", 2f);
        Instantiate(effect, player.transform.position, Quaternion.identity);
        Instantiate(sound, transform.position, Quaternion.identity);
        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        leftfinger.SetActive(false);
        rightfinger.SetActive(false);
        //transform.position = targetPos;

    }
    void TimeEffectRight()
    {
       // righteffect.SetActive(false);
    }
    void TimeEffectLeft()
    {
        //lefteffect.SetActive(false);
    }
    public void ButtonDown()
    {
        if (firstclick == 0)
        {
            if (setskinfons.index1 == 0)
            {
                swamp.enabled = true;
            }
            if (setskinfons.index1 == 1)
            {
                snow.enabled = true;
            }
            if (setskinfons.index1 == 2)
            {
                fire.enabled = true;
            }
            firstclick = 1;
            //firstgame = 0;
            if (setskinfons.index1 == 0 || setskinfons.index1 == 2)
            {
                SpawnRedEnemy[0].SetActive(true);
            }
            else if (setskinfons.index1 == 1)
            {
                SpawnRedEnemy[1].SetActive(true);
            }
            if (setskinfons.index1 == 0 || setskinfons.index1 == 2)
            {
                SpawnGreenScore[0].SetActive(true);
            }
            else if (setskinfons.index1 == 1)
            {
                SpawnGreenScore[1].SetActive(true);
            }
            table.SetActive(false);
            
            //SpawnerYellowTeleportEnemy.SetActive(true);
            script.enabled = true;

            if (newgame == 1 && a == 0 && firstgameеtest1 == 1)
            {
                script.enabled = true;
                script1.enabled = true;
                textguide.SetActive(true);
                table.SetActive(true);
                buttoncontinue1score.SetActive(true);
                Time.timeScale = 0f;
                buttonskip.SetActive(true);
            }
            script1.enabled = true;
            script.enabled = true;
            panel1.SetActive(false);
            panel2.SetActive(false);
            Instantiate(sound, transform.position, Quaternion.identity);
            rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
            leftfinger.SetActive(false);
            rightfinger.SetActive(false);
            // PlayerPrefs.SetInt("firstgame", firstgame);
            /* if (newgame == 1&& a==0)
              textguide.SetActive(true);
              table.SetActive(true);
              Time.timeScale = 0f;
              */
        }
        if (firstclick == 1)
        {
            Instantiate(effect, player.transform.position, Quaternion.identity);
            //lefteffect.SetActive(true);
            Invoke("TimeEffectLeft", 2f);
            rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
            Instantiate(sound, transform.position, Quaternion.identity);
            leftfinger.SetActive(false);
            rightfinger.SetActive(false);
            if (setskinfons.index1 == 0 || setskinfons.index1 == 2)
            {
                SpawnRedEnemy[0].SetActive(true);
            }
            else if (setskinfons.index1 == 1)
            {
                SpawnRedEnemy[1].SetActive(true);
            }
            if (setskinfons.index1 == 0 || setskinfons.index1 == 2)
            {
                SpawnGreenScore[0].SetActive(true);
            }
            else if (setskinfons.index1 == 1)
            {
                SpawnGreenScore[1].SetActive(true);
            }
            //transform.Translate(Vector2.left * speed);
            //targetPos = new Vector2(transform.position.x, transform.position.y - Yincrement);
            //transform.position = targetPos;
        }
    }
    void ReloadLevel()
    {
        gameManager.GameOver();

    }
    void BreakAdvertising()
    {
        gameManager.Break();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
 
        if (col.gameObject.tag == "Invulnerable")
        {
            IsDamage = false;
            health = 100;
            Destroy(col.gameObject);
            areol.SetActive(true);
            iconShield.SetActive(true);
            Invoke("isdamage", 5f);
        }
        if (col.gameObject.tag == "Magnit")
        {
            Debug.Log("magnit");
            point.enabled = true;
            collider2d.enabled = true;
            Destroy(col.gameObject);
            magnitobject.SetActive(true);
            iconMagnit.SetActive(true);
            Invoke("magnitoff", 5f);
        }
     
        if(col.gameObject.tag == "Enemy" && IsDamage == false)
            {
            countenemyshield1 = 1;
            Debug.Log("ЩитИВрагсТегомЭнеми");
            PlayerPrefs.SetInt("CountStone1", countenemyshield1);
        }
        if (col.gameObject.tag == "DiagonalEnemy" && IsDamage == false)
        {
            countenemyshield2 = 1;
            Debug.Log("ЩитИВрагсТегомDiagonalEnemy");
            PlayerPrefs.SetInt("CountStone2", countenemyshield2);
        }
        if (col.gameObject.tag == "EnemyTeleport" && IsDamage == false)
        {
            countenemyshield3 = 1;
            Debug.Log("ЩитИВрагсТегомEnemyTeleport");
            PlayerPrefs.SetInt("CountStone3", countenemyshield3);
        }
        if (col.gameObject.tag == "IronEnemy" && IsDamage == false)
        {
            countenemyshield4 = 1;
            Debug.Log("ЩитИВрагсТегомIronEnemy");
            PlayerPrefs.SetInt("CountStone4", countenemyshield4);
        }
        if(PlayerPrefs.GetInt("CountStone1")==1 && PlayerPrefs.GetInt("CountStone2") == 1 && PlayerPrefs.GetInt("CountStone3") == 1 && PlayerPrefs.GetInt("CountStone4") == 1)
        {
            GetTheAchiv(achiv8);
        }
        if (col.gameObject.tag == "Ship" && IsDamage == true)
        {
            Instantiate(effect, player.transform.position, Quaternion.identity);
            health = 0;
            Instantiate(soundenemy, transform.position, Quaternion.identity);
        }
        else
            if(col.gameObject.tag == "Ship" && IsDamage == false)
        {
            Instantiate(effect, player.transform.position, Quaternion.identity);
           // Destroy(col.gameObject);
            Instantiate(soundenemy, transform.position, Quaternion.identity);
        }
        if (setskinfons.index1 == 0 &&  col.gameObject.name == "stoneswamp(Clone)")
        {
            Debug.Log("ПерваяАчивка!");
            PlayerPrefs.SetInt("CountStone1", countlocstone1);
            Debug.Log("1");
        }
        if (setskinfons.index1 == 1 && col.gameObject.name == "stonesnow(Clone)")
        {
            Debug.Log("ВтораяАчивка!");
            PlayerPrefs.SetInt("CountStone2", countlocstone2);
            Debug.Log("2");
        }
        if (setskinfons.index1 == 2  && col.gameObject.name == "stone(Clone)")
        {
            Debug.Log("ТретьяАчивка!");
            PlayerPrefs.SetInt("CountStone3", countlocstone3);
            Debug.Log("3");
        }
        if (PlayerPrefs.GetInt("CountStone1") == 1 && PlayerPrefs.GetInt("CountStone2") == 1 && PlayerPrefs.GetInt("CountStone3") == 1)
        {
            Debug.Log("ACHIVKA7!");
            GetTheAchiv(achiv7);
        }
    }
    void TextOff()
    {
        textmagnitandshit.SetActive(false);
        textendlearnlvl.SetActive(true);
        if (textlearn == true)
        {
            Invoke("TextLearLvlOff", 5f);
            textlearn = false;
        }
        Time.timeScale = 0f;
    }
       void TextLearnLvlOff()
    {
        textendlearnlvl.SetActive(false);
        table.SetActive(false);
        Time.timeScale = 1f;
    }
           
    
   
    void isdamage()
    {
        health = 1;
        IsDamage = true;
        areol.SetActive(false);
    }
    void magnitoff()
    {
        point.enabled = false;
        collider2d.enabled = false;
        magnitobject.SetActive(false);
    }
    public void EndLearn()
    {
        //Time.timeScale = 1f;
        table.SetActive(false);
        Application.LoadLevel(Application.loadedLevel);
    }
    public void ButtonContinue()
    {
        textmagnitandshit.SetActive(false);
        table.SetActive(false);
        buttoncontinue.SetActive(false);
        buttonleft.SetActive(true);
        buttonright.SetActive(true);
        //leftfinger.SetActive(true);
        //rightfinger.SetActive(true);
        Time.timeScale = 1f;
    }
    public void ButtonContinue10Score()
    {
        textendlearnlvl.SetActive(false);
        buttoncontinue10score.SetActive(false);
        table.SetActive(false);
        buttonleft.SetActive(true);
        buttonright.SetActive(true);
        Time.timeScale = 1f;
    }
    public void ButtonContinue1Score()
    {
        
        counttext += 1;
        if (counttext == 0)
        {
            textguide.SetActive(true);
            
        }
        if (counttext == 1)
        {
            textguide.SetActive(false);
            textironenemy.SetActive(true);
        }
       
        if(counttext==2)
        {
            textironenemy.SetActive(false);
            textmagnitandshit.SetActive(true);
        }
        if (counttext == 3)
        {
            textironenemy.SetActive(false);
            textmagnitandshit.SetActive(false);
            textendlearnlvl.SetActive(true);

        }
        if (counttext == 4)
        {
            textendlearnlvl.SetActive(false);
            table.SetActive(false);
            buttoncontinue1score.SetActive(false);
            buttonleft.SetActive(true);
            buttonright.SetActive(true);
            script.enabled = true;
            Time.timeScale = 1f;
            buttonskip.SetActive(false);
        }
        //buttonleft.SetActive(true);
        // buttonright.SetActive(true);
        // Time.timeScale = 1f;
    }
}
