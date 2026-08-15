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
    public GameObject allobject;
    public AudioSource sound;
    public GameObject spawnergreen;
    public GameObject spawneryellow;
    public SpriteRenderer player;
    public SpriteRenderer saw;
    public Spawner scriptspanwer;
    public SpriteRenderer mainfon;
    public SpawnerYellowBallon[] scriptspawneryellowballon;
    public Rigidbody2D rb2d;
    
    [Header("Parallax & Backgrounds")]
    public GameObject paralaxfonswamp;
    public GameObject ParalaxFonFire;
    public GameObject ParalaxFonSnow;
    public GameObject spawnsnow;
    public GameObject spawnsnowwhite;
    public SetSkinFons script1;
    public GameOverUIManager gameOverUI;

    private bool isGameOver = false;

    private void Start()
    {
        allobject.SetActive(true);
        canvas.SetActive(true);
        gameOverCanvas.SetActive(false); // Убеждаемся, что экран проигрыша скрыт при старте
    }

    private void Update()
    {
        // Убрали сохранение счета отсюда ради оптимизации производительности

        // Включение нужных фонов (можно позже оптимизировать через Switch)
        if (script1.index1 == 0)
        {
            paralaxfonswamp.SetActive(true);
        }
        else if (script1.index1 == 1)
        {
            ParalaxFonSnow.SetActive(true);
            spawnsnow.SetActive(true);
            spawnsnowwhite.SetActive(true);
        }
        else if (script1.index1 == 2)
        {
            ParalaxFonFire.SetActive(true);
        }
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        // 1. Отключаем игровые объекты
        allobject.SetActive(false);
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

        if (paralaxfonswamp.activeSelf) 
        {
            paralaxfonswamp.GetComponent<Paralax>().enabled = false;
        }
        // То же самое для других фонов, если они активны
        if (ParalaxFonFire.activeSelf) ParalaxFonFire.GetComponent<Paralax>().enabled = false;
        if (ParalaxFonSnow.activeSelf) ParalaxFonSnow.GetComponent<Paralax>().enabled = false;
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
        allobject.SetActive(true);
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
        allobject.SetActive(true);
        canvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        saw.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<SpriteRenderer>().enabled = true;
        scriptspanwer.enabled = true;
        
        for (int i = 0; i < scriptspawneryellowballon.Length; i++)
        {
            scriptspawneryellowballon[i].enabled = true;
        }
        
        mainfon.GetComponent<SpriteRenderer>().sortingOrder = -5;
        rb2d.constraints = RigidbodyConstraints2D.None;
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void Replay()
    {
        Instantiate(sound, transform.position, Quaternion.identity);
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