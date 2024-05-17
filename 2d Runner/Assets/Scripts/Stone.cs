using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public GameObject effect;
    public int damage = 1;
    public GameObject sound;
    public Player script;
    public SetSkinFons scriptnew;
    public bool isdamage = true;
    private const string achiv7 = "CgkIu-eNx_IEEAIQCA";
    // Start is called before the first frame update
    void Start()
    {
        script = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        scriptnew = GameObject.FindGameObjectWithTag("Image").GetComponent<SetSkinFons>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void GetTheAchiv(string id)
    {
        Social.ReportProgress(id, 100.0f, (bool success) =>
        {
            if (success) print("Получено достижение" + id);
        });
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DiagonalEnemy" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyTeleport"|| collision.gameObject.tag == "Player" || collision.gameObject.tag == "IronEnemy")
        {
            Debug.Log("Пшл нфг урд");
            Instantiate(effect, transform.position, Quaternion.identity);
            Instantiate(sound, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"&&script.IsDamage == true&&isdamage == true)
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Instantiate(sound, transform.position, Quaternion.identity);
            script.health = script.health -1;
            Destroy(gameObject);
            isdamage = false;
        }
    }
}
