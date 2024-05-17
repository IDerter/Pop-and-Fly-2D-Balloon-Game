using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
public class IronEnemyLeft : MonoBehaviour
{
    public SetSkin scriptskin;
    public GameObject effect;
    public GameObject sound;
    public GameObject ironenemy;
    public float speed;
    public int damage = 1;
    private const string achiv8 = "CgkIu-eNx_IEEAIQCQ";
    public static int countenemies = 0;
    // Start is called before the first frame update
    void Start()
    {
        scriptskin = GameObject.FindGameObjectWithTag("Player").GetComponent<SetSkin>();
        countenemies = PlayerPrefs.GetInt("CountIronEnemy");
    }
    private void GetTheAchiv(string id)
    {
        Social.ReportProgress(id, 100.0f, (bool success) =>
        {
            if (success) print("Получено достижение" + id);
        });
    }
    // Update is called once per frame
    void Update()
    {
        scriptskin = GameObject.FindGameObjectWithTag("Player").GetComponent<SetSkin>();
    }
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed);
    }
    public void OnMouseDown()
    {
        //Destroy(gameObject);
        ironenemy.SetActive(false);
        Debug.Log("Destroy");
        Instantiate(sound, transform.position, Quaternion.identity);
        Instantiate(effect, transform.position, Quaternion.identity);
        countenemies += 1;
        PlayerPrefs.SetInt("CountIronEnemy", countenemies);
        if (PlayerPrefs.GetInt("CountIronEnemy") >= 10)
        {
            GetTheAchiv(achiv8);
            Debug.Log("YbrbnfLox");
        }
    }
    void Achiv()
    {
        Debug.Log("получил ачивку!");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && scriptskin.index != 13)
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Instantiate(sound, transform.position, Quaternion.identity);
            other.GetComponent<Player>().health -= damage;
            Destroy(gameObject);
            Debug.Log("не13скин");
        }
        else
        if (other.CompareTag("Player") && scriptskin.index == 13)
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Instantiate(sound, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Debug.Log("13скин");
        }
    }
}
