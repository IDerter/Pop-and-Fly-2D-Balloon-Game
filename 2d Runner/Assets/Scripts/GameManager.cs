using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Подключаем библиотеку для красивого текста

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject gameOverCanvas;
    public GameObject canvas;

    [Header("Game Objects & Scripts")]
    public Player script;

    public SpriteRenderer player;
    public SpriteRenderer saw;
    public Spawner scriptspanwer;
    public SpawnerYellowBallon[] scriptspawneryellowballon;
    public Rigidbody2D rb2d;
    
    [Header("Parallax & Backgrounds")]
    public GameOverUIManager gameOverUI;

    private bool isGameOver = false;

    private void Start()
    {
        canvas.SetActive(true);
        gameOverCanvas.SetActive(false); // Убеждаемся, что экран проигрыша скрыт при старте
    }

    public void GameContinue()
    {
        isGameOver = false;

        // 1. Вызываем плавное скрытие вместо резкого gameOverCanvas.SetActive(false);
        if (gameOverUI != null) gameOverUI.HidePanel();
        
        if (canvas != null) canvas.SetActive(true);

        // 2. Включаем видимость персонажа и пилы
        if (saw != null) saw.GetComponent<SpriteRenderer>().enabled = true;
        if (player != null) player.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        // 1. Отключаем игровые объекты
        canvas.SetActive(false);
        saw.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<SpriteRenderer>().enabled = false;
        
        Destroy(GameObject.FindGameObjectWithTag("Enemy"), 0f);
        Destroy(GameObject.FindGameObjectWithTag("DiagonalEnemy"), 0f);
        Destroy(GameObject.FindGameObjectWithTag("EnemyTeleport"), 0f);
        Debug.Log("Game Over triggered");

        // 2. Логика сохранения лучшего счета (ОДИН раз при проигрыше)
        int bestScore = PlayerPrefs.GetInt("Score0", 0);
        if (script.score > bestScore)
        {
            bestScore = script.score;
            PlayerPrefs.SetInt("Score0", bestScore);
            PlayerPrefs.Save();
            Debug.Log("Новый рекорд сохранен!");
        }

        // 3. Запускаем красивую анимацию, передавая текущий счет и рекорд
        gameOverUI.AnimateGameOver(script.score, bestScore);
    }

    public void Break()
    {
        gameOverCanvas.SetActive(true);
        Debug.Log("Break");
    }

    public void GameStart()
    {
        isGameOver = false;

        Debug.Log("GameStart");
        canvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        saw.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<SpriteRenderer>().enabled = true;
        script.IsDamage = false;
        Invoke("IsDamage", 5f);
        Invoke("GameStartTrue", 0.5f);
    }

    public void GameStartTrue()
    {
        Debug.Log("GameStartTrue");
        canvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        saw.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<SpriteRenderer>().enabled = true;
        scriptspanwer.enabled = true;
        
        for (int i = 0; i < scriptspawneryellowballon.Length; i++)
        {
            scriptspawneryellowballon[i].enabled = true;
        }
        
        rb2d.constraints = RigidbodyConstraints2D.None;
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void Replay()
    {
        // Используем современный метод загрузки сцены вместо устаревшего Application.LoadLevel
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Перезагружает текущую сцену
    }

    public void LoadMenu()
    {
        // Метод для кнопки "Домой"
        SceneManager.LoadScene("MainMenu"); // Замени "MainMenu" на точное имя твоей сцены меню
    }

    public void IsDamage()
    {
        script.IsDamage = true;
    }

    public void OpenStatistic()
    {
        // Логика открытия статистики
    }
}