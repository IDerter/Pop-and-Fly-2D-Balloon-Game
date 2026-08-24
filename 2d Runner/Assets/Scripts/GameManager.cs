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
    }

    private void OnDisable()
    {
        player.OnFirstClick -= HandleGameStart;
        player.OnPlayerDied -= HandleGameOver;
        player.OnScoreChanged -= CheckScoreMilestones;
    }

    private void Start()
    {
        DisableAllSpawners();
    }

    private void HandleGameStart()
    {
        tutorialUI.HideStartMenu();
        
        if (mainSpawner) mainSpawner.enabled = true;
        if (lollipopSpawner) lollipopSpawner.enabled = true;
        
        if (backgroundParallax) backgroundParallax.SetActive(true);
    }

    private void CheckScoreMilestones(int score)
    {
        if (score >= 4)
        {
            if (spawnerShit) spawnerShit.enabled = true;
            if (spawnerMagnet) spawnerMagnet.enabled = true;
        }
        
        // ПРАВИЛЬНАЯ РАБОТА С YG2
        if (score == 10 && !YG2.saves.isTutorialCompleted)
        {
            //tutorialUI.ShowTutorialEnd(); 
            
            YG2.saves.isTutorialCompleted = true;
            YG2.SaveProgress();
        }
        
        if (score >= 10 && spawnBeeEnemy) spawnBeeEnemy.enabled = true;
        if (score >= 3 && spawnGhostTeleportEnemy) spawnGhostTeleportEnemy.enabled = true; // было 30 исправить
        if (score >= 60 && spawnerStones) spawnerStones.enabled = true;
        if (score >= 120 && spawnerExplosion) spawnerExplosion.enabled = true;
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

    public void RestartGame() { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
    public void LoadMenu() { SceneManager.LoadScene("MainMenu"); }
}