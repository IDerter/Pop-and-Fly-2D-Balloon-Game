using UnityEngine;
using UnityEngine.SceneManagement;
using YG; // Используем новую версию YG2

public class GameManager : MonoBehaviour
{
    [Header("Scripts")]
    public Player player;
    public GameOverUIManager gameOverUI; 
    public MenuAndTutorialUi tutorialUI; 
    
    [Header("Basic Spawners (Start Immediately)")]
    public Spawner mainSpawner;
    public Spawner lollipopSpawner;
    
    [Header("Progression Spawners (Unlock by Score)")]
    public Spawner spawnerShit;
    public Spawner spawnerMagnet;
    public Spawner spawnBeeEnemy;
    public Spawner spawnGhostTeleportEnemy;
    public Spawner spawnerStones;
    public Spawner spawnerExplosion;
    
    [Header("World")]
    public GameObject backgroundParallax; 

    private bool isGameOver = false;
    
    // БУФЕРНАЯ ПЕРЕМЕННАЯ: Отслеживает, сколько монет мы уже сохранили в текущем забеге
    private int _coinsSavedThisRun = 0; 

    private void OnEnable()
    {
        player.OnFirstClick += HandleGameStart;
        player.OnPlayerDied += HandleGameOver;
        player.OnScoreChanged += CheckScoreMilestones;

        RewardedAds.RewardOn += HandleRewardReceived;
    }

    private void OnDisable()
    {
        player.OnFirstClick -= HandleGameStart;
        player.OnPlayerDied -= HandleGameOver;
        player.OnScoreChanged -= CheckScoreMilestones;

        RewardedAds.RewardOn -= HandleRewardReceived;
    }

    private void Start()
    {
        DisableAllSpawners();
    }

    private void HandleGameStart()
    {
        tutorialUI.HideStartMenu();

        if (!YG2.saves.isTutorialCompleted)
        {
            AnalyticsManager.Instance.SaveLearningStep("tutorial_start");
        }
        
        if (mainSpawner) mainSpawner.enabled = true;
        if (lollipopSpawner) lollipopSpawner.enabled = true;
        
        if (backgroundParallax) backgroundParallax.SetActive(true);
    }

    private void HandleRewardReceived(string rewardType)
    {
        if (rewardType == "Reborn") // Или TypeReward.Reborn.ToString()
        {
            RevivePlayerAfterAd();
        }
    }

    private void CheckScoreMilestones(int score)
    {
        if (score >= 4)
        {
            if (spawnerShit) spawnerShit.enabled = true;
            if (spawnerMagnet) spawnerMagnet.enabled = true;
        }
        
        if (score >= 10 && spawnBeeEnemy) spawnBeeEnemy.enabled = true;
        if (score >= 30 && spawnGhostTeleportEnemy) spawnGhostTeleportEnemy.enabled = true; // было 30 исправить
        if (score >= 60 && spawnerStones) spawnerStones.enabled = true;
       // if (score >= 1 && spawnerExplosion) spawnerExplosion.enabled = true;
    }

    private void HandleGameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        DisableAllSpawners();
        ClearEnemies();

        // 1. ЛОГИКА СОХРАНЕНИЯ МОНЕТ С УЧЕТОМ ВОЗРОЖДЕНИЙ
        int newCoins = player.score - _coinsSavedThisRun; 
        
        if (newCoins > 0)
        {
            YG2.saves.coin += newCoins; // Добавляем в облако только новые монеты
            _coinsSavedThisRun = player.score; // Запоминаем, что эти монеты уже учтены
        }

        // 2. Логика сохранения рекорда
        if (player.score > YG2.saves.bestScore)
        {
            YG2.saves.bestScore = player.score;
        }

        // 3. Отправляем всё (и рекорд, и монеты) в облако Яндекса одним запросом
        YG2.SaveProgress(); 

        Invoke(nameof(ShowGameOverScreen), 1f);
    }

    private void ShowGameOverScreen()
    {
        gameOverUI.AnimateGameOver(player.score, YG2.saves.bestScore);
    }

    // Этот метод нужно вызывать из вашего скрипта рекламы YG2
    public void RevivePlayerAfterAd()
    {
        isGameOver = false;
        CancelInvoke(nameof(ShowGameOverScreen));

        ClearEnemies();
        
        if (mainSpawner) mainSpawner.enabled = true;
        if (lollipopSpawner) lollipopSpawner.enabled = true;
        CheckScoreMilestones(player.score); 
        
        gameOverUI.HidePanel(); 
        player.Revive();
    }

    private void DisableAllSpawners()
    {
        if (mainSpawner) mainSpawner.enabled = false;
        if (lollipopSpawner) lollipopSpawner.enabled = false;
        
        if (spawnerShit) spawnerShit.enabled = false;
        if (spawnerMagnet) spawnerMagnet.enabled = false;
        if (spawnBeeEnemy) spawnBeeEnemy.enabled = false;
        if (spawnGhostTeleportEnemy) spawnGhostTeleportEnemy.enabled = false;
        if (spawnerStones) spawnerStones.enabled = false;
        if (spawnerExplosion) spawnerExplosion.enabled = false;
    }

    private void ClearEnemies()
    {
        string[] tagsToClear = { "Enemy", "DiagonalEnemy", "IronEnemy", "EnemyTeleport", "Invulnerable", "Magnit", "Score", "Ship" };
        
        foreach (string tag in tagsToClear)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
            foreach (var obj in objects)
            {
                ObjectPoolManager.Instance.ReturnToPool(obj);
            }
        }
    }

    public void RestartGame() 
    { 
        // 1. Отписываемся на всякий случай, чтобы предотвратить двойное срабатывание при спаме кнопки
        InterstitialAds.OnInterstitialAdClosed -= OnAdClosedForRestart;
        
        // 2. Подписываемся на событие закрытия рекламы
        InterstitialAds.OnInterstitialAdClosed += OnAdClosedForRestart;
        
        // 3. Вызываем показ рекламы через менеджер
        AdsManager.Instance._interstitialAds.ShowInterstitialAd();

        AnalyticsManager.Instance.RestartLeveStats(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnAdClosedForRestart()
    {
        InterstitialAds.OnInterstitialAdClosed -= OnAdClosedForRestart; // Отписываемся
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);     // Перезагружаем уровень
    }

    public void LoadMenu() 
    { 
        // Делаем то же самое для кнопки выхода в меню
        InterstitialAds.OnInterstitialAdClosed -= OnAdClosedForMenu;
        InterstitialAds.OnInterstitialAdClosed += OnAdClosedForMenu;
        
        AdsManager.Instance._interstitialAds.ShowInterstitialAd();
    }

    private void OnAdClosedForMenu()
    {
        InterstitialAds.OnInterstitialAdClosed -= OnAdClosedForMenu;
        SceneManager.LoadScene("MainMenu");
    }

    private void OnDestroy()
    {
        // Обязательная защита от утечек памяти, если объект GameManager уничтожится раньше времени
        InterstitialAds.OnInterstitialAdClosed -= OnAdClosedForRestart;
        InterstitialAds.OnInterstitialAdClosed -= OnAdClosedForMenu;
    }
}