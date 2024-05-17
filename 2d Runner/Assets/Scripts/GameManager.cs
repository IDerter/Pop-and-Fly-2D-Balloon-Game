using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public Player script;
    public GameObject allobject;
    public GameObject canvas;
    public AudioSource sound;
    public GameObject spawnergreen;
    public GameObject spawneryellow;
    public SpriteRenderer player;
    public SpriteRenderer saw;
    public Spawner scriptspanwer;
    public SpriteRenderer mainfon;
    public SpawnerYellowBallon[] scriptspawneryellowballon;
    public Rigidbody2D rb2d;
    public GameObject paralaxfonswamp;
    public GameObject ParalaxFonFire;
    public GameObject ParalaxFonSnow;
    public GameObject spawnsnow;
    public GameObject spawnsnowwhite;
    public  SetSkinFons script1;
    // Use this for initialization
    private void Start()
    {
        allobject.SetActive(true);
        canvas.SetActive(true);
        
    }
    private void Update()
    {
        if (PlayerPrefs.GetInt("Score0") <= script.score) //&& gameOverCanvas.activeSelf == true)// && PlayerPrefs.HasKey("Score0") == true)
        {
            PlayerPrefs.SetInt("Score0", script.score);
            Debug.Log("Сохраняем лучший результат");
        }
        if (script1.index1==0)
        {
            paralaxfonswamp.SetActive(true);
        }
        if (script1.index1 == 1)
        {
            ParalaxFonSnow.SetActive(true);
            spawnsnow.SetActive(true);
            spawnsnowwhite.SetActive(true);
        }
        if (script1.index1 == 2)
        {
            ParalaxFonFire.SetActive(true);
        }
    }
    public void GameOver()
    {
        allobject.SetActive(false);
        canvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        saw.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(GameObject.FindGameObjectWithTag("Enemy"), 0f);
        Destroy(GameObject.FindGameObjectWithTag("DiagonalEnemy"), 0f);
        Destroy(GameObject.FindGameObjectWithTag("EnemyTeleport"), 0f);
        Debug.Log("2");

    }
    public void Break()
    {
        gameOverCanvas.SetActive(true);
        Debug.Log("Break");
    }
    public void GameStart()
    {
        Debug.Log("1");
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
        Debug.Log("1");
        allobject.SetActive(true);
        canvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        saw.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<SpriteRenderer>().enabled = true;
        scriptspanwer.enabled = true;
        for (int i =0;i<6;i++)
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
        //mainfon.GetComponent<SpriteRenderer>().sortingOrder = -5;
        Application.LoadLevel("MainLvl");
        
    }
    public void IsDamage()
    {
        script.IsDamage = true;
    }
    public void OpenStatistic()
    {

    }
}
