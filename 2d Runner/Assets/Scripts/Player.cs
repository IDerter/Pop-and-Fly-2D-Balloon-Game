using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms;
using DG.Tweening; 

public class Player : MonoBehaviour 
{
    [Header("Components & Animation")]
    public Animator characterAnimator;
    public Transform player;
    public SpriteRenderer player1;
    public SpriteRenderer saw;
    public Rigidbody2D rb2d;
    public Rigidbody2D rb;
    public CapsuleCollider2D collider2d;
    public AudioSource sound;
    public AudioSource soundenemy;

    [Header("Stats & Physics")]
    public int health = 1;
    public float speed;
    public float Yincrement;
    public float maxHeight;
    public float minHeight;
    public bool IsDamage = true;
    public bool ismoney = false;

    [Header("Score & State")]
    public int score = 0;
    public int coin;
    public int firstclick = 0;
    public static int a = 0;
    public static int bestscore = 0;
    public int firstgame = 1;
    public int newgame = 1;
    public int firstgameеtest = 1, firstgameеtest1 = 1, firstgameеtest2 = 1;
    public static int test = 0, firsttest = 0;
    
    public static int countLocStone = 0;
    public static int countenemyshield1 = 0, countenemyshield2 = 0, countenemyshield3 = 0, countenemyshield4 = 0;

    [Header("Managers & Scripts")]
    public GameManager gameManager;
    public SetSkin scriptskin;
    public Spawner scriptspanwer;
    public Paralax script;
    public Paralax script1;
    public Paralax backgroundParallax;
    public SpawnerYellowBallon[] scriptspawneryellowballon;

    [Header("UI Elements")]
    public GameObject table;
    public GameObject panel1, panel2;
    public GameObject texttap, textguide, textironenemy, textmagnitandshit, textendlearnlvl, textqustionlearnlvl;
    public GameObject buttonplay, buttonskip, buttonleft, buttonright, buttonreborn, buttoncoin2x;
    public GameObject buttoncontinue, buttoncontinue10score, buttoncontinue1score;
    public GameObject leftfinger, rightfinger;
    public GameObject iconShield, iconMagnit;
    public SpriteRenderer mainfon;

    [Header("Spawners & Objects")]
    public GameObject effect, lefteffect, righteffect;
    public GameObject magnit, magnitobject, shitobject, abilkamagnit, MagnitPoint, areol;
    public PointEffector2D point;
    
    public GameObject SpawnBeeEnemy; 
    public GameObject SpawnEnemy; 
    public GameObject SpawnGreenScore; 
    public GameObject SpawnerYellowTeleportEnemy; 
    
    public GameObject spawnershit, spawnerexplosion, spawnermagnit, SpawnerYellowBallon;
    public GameObject ship1, ship2, ship1child, ship2child, allship;
    
    public GameObject spawnerStones;
    public GameObject paralaxFon;
    public GameObject allSpawns;

    public bool IsShip = true;
    public int counttext = 0;
    public bool textlearn = true;

    // Событие изменения счета (без Update!)
    public event Action<int> OnScoreChanged;

    private const string achiv1 = "CgkIu-eNx_IEEAIQAQ";
    private const string achiv2 = "CgkIu-eNx_IEEAIQAw";
    private const string achiv3 = "CgkIu-eNx_IEEAIQBA";
    private const string achiv4 = "CgkIu-eNx_IEEAIQBQ";
    private const string achiv5 = "CgkIu-eNx_IEEAIQBg";
    private const string achiv7 = "CgkIu-eNx_IEEAIQCA";
    private const string achiv8 = "CgkIu-eNx_IEEAIQCg";
    private const string achiv9 = "CgkIu-eNx_IEEAIQCw";
    private const string leaderboard = "CgkIu-eNx_IEEAIQAA";

    private Vector3 defaultScale;

    void Awake()
    {
        a = PlayerPrefs.GetInt("checktext");
        bestscore = PlayerPrefs.GetInt("Score0", 0);
        rb = GetComponent<Rigidbody2D>();
        rb2d = GetComponent<Rigidbody2D>();

        if (player1 != null) defaultScale = player1.transform.localScale;
    }

    void Start()
    {
        if (characterAnimator != null) characterAnimator.enabled = false; 

        InitializeSkinAndHealth();
        InitializeBackgrounds();

        if ((test >= 1 && firsttest == 1) || firstgame == 0)
        {
            newgame = PlayerPrefs.GetInt("firstgame", 1);
            PlayerPrefs.SetInt("firstgame", firstgame);
        }

        coin = PlayerPrefs.GetInt("coin", 0);
        IsDamage = true;
        Time.timeScale = 1f;
        test++;

        if (newgame == 0 || a != 0) DisableTutorialUI();
    }

    private void Update()
    {
        if (health <= 0)
        {
            HandleDeath();
            return;
        }

        CheckScoreMilestones();
        HandleMagnitSkinLogic();
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
    }

    // ==========================================
    // ПУБЛИЧНЫЙ МЕТОД ДЛЯ НАЧИСЛЕНИЯ ОЧКОВ СОБЫТИЕМ
    // ==========================================
    public void AddScore(int amount)
    {
        score += amount;
        OnScoreChanged?.Invoke(score); // Оповещаем UI без использования Update
    }

    // ==========================================
    // ЛОГИКА ДВИЖЕНИЯ И ВВОДА
    // ==========================================

    public void ButtonUp() { MovePlayer(1, -15f); }
    public void ButtonDown() { MovePlayer(-1, 15f); }

    private void MovePlayer(int direction, float tiltAngle)
    {
        if (firstclick == 0) HandleFirstClickStart();

        if (firstclick == 1)
        {
            Instantiate(sound, transform.position, Quaternion.identity);
            DisableFingersUI();
            ActivateCurrentSkinEnemies();
        }

        rb2d.velocity = new Vector2(speed * direction, rb2d.velocity.y);

        if (player1 != null)
        {
            DOTween.Kill("RotateTween"); 
            player1.transform.DORotate(new Vector3(0, 0, tiltAngle), 0.35f)
                .SetEase(Ease.OutBack)
                .SetId("RotateTween"); 
        }
    }

    private void HandleFirstClickStart()
    {
        firstclick = 1;
        if (characterAnimator != null) characterAnimator.enabled = true; 

        ActivateBackgroundScripts();
        ActivateCurrentSkinEnemies();

        texttap.SetActive(false);
        table.SetActive(false);
        panel1.SetActive(false);
        panel2.SetActive(false);

        if (newgame == 1 && a == 0 && firstgameеtest1 == 1) ShowTutorialDialogs();

        script1.enabled = true;
        script.enabled = true;
    }

    // ==========================================
    // ЛОГИКА СТОЛКНОВЕНИЙ
    // ==========================================

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Invulnerable"))
        {
            PlayEatJuice();
            IsDamage = false;
            health = 100;
            Destroy(col.gameObject);
            areol.SetActive(true);
            iconShield.SetActive(true);
            Invoke(nameof(ResetDamage), 5f);
        }
        else if (col.gameObject.CompareTag("Magnit"))
        {
            PlayEatJuice();
            point.enabled = true;
            collider2d.enabled = true;
            Destroy(col.gameObject);
            magnitobject.SetActive(true);
            iconMagnit.SetActive(true);
            Invoke(nameof(MagnitOff), 5f);
        }
        else if (col.gameObject.CompareTag("Ship"))
        {
            Instantiate(soundenemy, transform.position, Quaternion.identity);
            if (IsDamage) health = 0;
        }
    }

    private void HandleDeath()
    {
        Invoke(nameof(TimeSleep), 0.5f);
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;

        magnitobject.SetActive(false);
        shitobject.SetActive(false);
        IsDamage = false;

        if (saw != null) saw.GetComponent<SpriteRenderer>().enabled = false;
        if (characterAnimator != null) characterAnimator.SetTrigger("Die");

        DisableSpawners();
        DestroyGameObjectsWithTags(new[] { "Enemy", "Score", "DiagonalEnemy", "EnemyTeleport" });

        PlayerPrefs.SetInt("coin", coin);
        PlayerPrefs.SetInt("checktext", a);
        UpdateBestScore();
        HandleFirstGameLogic();
        PlayerPrefs.Save();

        Invoke(nameof(ReloadLevel), 0.5f);
    }

    // ==========================================
    // UI И МЕНЮ МЕТОДЫ
    // ==========================================

    public void achivOpen() { Social.ShowAchievementsUI(); }
    public void leaderboardOpen() { Social.ShowLeaderboardUI(); }
    public void Reborn() { Invoke(nameof(ReloadLevel), 0f); }
    public void ButtonCoin2x() { buttoncoin2x.SetActive(true); }
    public void Coin() { Invoke(nameof(ReloadLevel), 0f); }
    public void Timescale() { Time.timeScale = 0f; }
    public void BreakAdvertising() { gameManager.Break(); }
    public void EndLearn() { table.SetActive(false); SceneManager.LoadScene(SceneManager.GetActiveScene().name); }

    public void SkipTutorial()
    {
        firstgame = 0;
        firsttest = 1;
        PlayerPrefs.SetInt("firstgame", firstgame);
        textguide.SetActive(false);
        table.SetActive(false);
        buttoncontinue1score.SetActive(false);
        Time.timeScale = 1f;
        buttonskip.SetActive(false);
    }

    public void ButtonContinue()
    {
        textmagnitandshit.SetActive(false);
        table.SetActive(false);
        buttoncontinue.SetActive(false);
        buttonleft.SetActive(true);
        buttonright.SetActive(true);
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
        if (counttext == 0) textguide.SetActive(true);
        if (counttext == 1) { textguide.SetActive(false); textironenemy.SetActive(true); }
        if (counttext == 2) { textironenemy.SetActive(false); textmagnitandshit.SetActive(true); }
        if (counttext == 3) { textironenemy.SetActive(false); textmagnitandshit.SetActive(false); textendlearnlvl.SetActive(true); }
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
    }

    void TextOff()
    {
        textmagnitandshit.SetActive(false);
        textendlearnlvl.SetActive(true);
        if (textlearn)
        {
            Invoke(nameof(TextLearnLvlOff), 5f);
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

    // ==========================================
    // ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ (Инкапсуляция)
    // ==========================================

    private void CheckScoreMilestones()
    {
        if (score >= 4)
        {
            if (spawnershit != null) spawnershit.SetActive(true);
            if (spawnermagnit != null) spawnermagnit.SetActive(true);
            if (textendlearnlvl.activeSelf && newgame == 1) Invoke(nameof(TextLearnLvlOff), 5f);
        }

        if (score >= 10 && newgame == 1 && a == 0)
        {
            firstgame = 0;
            firsttest = 1;
            PlayerPrefs.SetInt("firstgame", firstgame);
            newgame = PlayerPrefs.GetInt("firstgame", firstgame);
            a = 1;
            buttonleft.SetActive(false);
            buttonright.SetActive(false);
            textqustionlearnlvl.SetActive(true);
            table.SetActive(true);
            buttonplay.SetActive(true);
            Invoke(nameof(Timescale), 0.2f);
        }

        if (score >= 10 && SpawnBeeEnemy != null) SpawnBeeEnemy.SetActive(true);
        if (score >= 30 && SpawnerYellowTeleportEnemy != null) SpawnerYellowTeleportEnemy.SetActive(true);
        if (score >= 60) ActivateStoneSpawners();
        if (score >= 100) 
        {
            if (ship1 != null) ship1.SetActive(true);
            if (ship2 != null) ship2.SetActive(true);
            Invoke(nameof(ShipTrigger), 2f);
        }
        if (score >= 120 && spawnerexplosion != null) spawnerexplosion.SetActive(true);
    }

    private void UpdateBestScore()
    {
        int savedBest = PlayerPrefs.GetInt("Score0", 0);
        if (score > savedBest)
        {
            PlayerPrefs.SetInt("Score0", score);
            bestscore = score;
        }
    }

    private void HandleFirstGameLogic()
    {
        if (score >= 10)
        {
            firstgame = 0;
            firsttest = 1;
            PlayerPrefs.SetInt("firstgame", firstgame);
        }
    }

    private void ActivateCurrentSkinEnemies()
    {
        if (SpawnEnemy != null) SpawnEnemy.SetActive(true);
        if (SpawnGreenScore != null) SpawnGreenScore.SetActive(true);
    }

    private void ActivateBackgroundScripts()
    {
        if (backgroundParallax != null) backgroundParallax.enabled = true;
    }

    private void ActivateStoneSpawners()
    {
        if (spawnerStones != null) spawnerStones.SetActive(true);
    }

    private void InitializeSkinAndHealth()
    {
        if (scriptskin.index == 14) health = 2;
        if (scriptskin.index == 8) speed = 8f;
    }

    private void InitializeBackgrounds()
    {
        if (paralaxFon != null) paralaxFon.SetActive(true);
        if (allSpawns != null) allSpawns.SetActive(true);
    }

    private void HandleMagnitSkinLogic()
    {
        if (scriptskin.index == 11) { point.enabled = true; collider2d.enabled = true; magnitobject.SetActive(true); abilkamagnit.SetActive(true); }
    }

    private void ShowTutorialDialogs()
    {
        script.enabled = true;
        script1.enabled = true;
        textguide.SetActive(true);
        table.SetActive(true);
        buttoncontinue1score.SetActive(true);
        Time.timeScale = 0f;
        buttonskip.SetActive(true);
    }

    private void DestroyGameObjectsWithTags(string[] tags)
    {
        foreach (string t in tags) { Destroy(GameObject.FindGameObjectWithTag(t), 0.2f); }
    }

    private void DisableSpawners()
    {
        if (scriptspanwer != null) scriptspanwer.enabled = false;
        foreach (var spawner in scriptspawneryellowballon) { if (spawner != null) spawner.enabled = false; }
    }

    private void DisableTutorialUI() { texttap.SetActive(false); table.SetActive(false); leftfinger.SetActive(false); rightfinger.SetActive(false); }
    private void DisableFingersUI() { leftfinger.SetActive(false); rightfinger.SetActive(false); }

    void TimeSleep() { mainfon.GetComponent<SpriteRenderer>().sortingOrder = 6; }
    void ShipTrigger() { if (ship1 != null) ship1.GetComponent<BoxCollider2D>().enabled = true; if (ship2 != null) ship2.GetComponent<BoxCollider2D>().enabled = true; if (ship1child != null) ship1child.SetActive(true); if (ship2child != null) ship2child.SetActive(true); }
    void ResetDamage() { health = 1; IsDamage = true; areol.SetActive(false); }
    void MagnitOff() { point.enabled = false; collider2d.enabled = false; magnitobject.SetActive(false); }
    void ReloadLevel() { gameManager.GameOver(); }

    public void PlayEatJuice()
    {
        if (characterAnimator != null) characterAnimator.SetTrigger("Eat");

        if (player1 != null)
        {
            DOTween.Kill("ScaleTween"); 
            player1.transform.localScale = defaultScale; 
            player1.transform.DOPunchScale(new Vector3(0.2f, -0.2f, 0f), 0.3f, 1, 0.5f).SetId("ScaleTween");
        }
    }
}