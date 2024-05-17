using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowEnemy : MonoBehaviour {
    public float speed;
    private Vector2 targetPos;
    public Transform gear;//позиция желтого шарика
    public GameObject effect;
    public GameObject sound;
    public float maxX;
    public float minX;
    public int damage = 1;
    public Player script;
    public Vector2 direction;
    public float Yincrement;
    public Rigidbody2D rb2d;
    public bool right = true;
    public SetSkin scriptskin;
    public bool isdamage = true;
    // Use this for initialization
    void Start()
    {
        scriptskin = GameObject.FindGameObjectWithTag("Player").GetComponent<SetSkin>();
        rb2d = GetComponent<Rigidbody2D>();
        GameObject.Find("AllObjectOnScene");
        //GameObject.Find("SpawnerEnemyBlue");
        gear.transform.SetParent(GameObject.Find("AllObjectOnScene").transform);
    }

    // Update is called once per frame
    private void Update()
    {
        scriptskin = GameObject.FindGameObjectWithTag("Player").GetComponent<SetSkin>();
    }
    private void FixedUpdate()
    {
        scriptskin = GameObject.FindGameObjectWithTag("Player").GetComponent<SetSkin>();
        transform.Translate(direction);
        //transform.Translate(direction);
        //transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.fixedDeltaTime);
        
        /*if (transform.position.x < maxX && right==true)
        {
            right = true;
            targetPos = new Vector2(transform.position.x + Yincrement,transform.position.y - (Yincrement/3));
            transform.position = targetPos;

            
            
            
        }
       else if (transform.position.x > minX)
        {
            right = false;
            targetPos = new Vector2(transform.position.x - Yincrement, transform.position.y - (Yincrement / 3));
            transform.position = targetPos;
            
            
        }
        else if (transform.position.x <= minX)
        {
            right = true;
            targetPos = new Vector2(transform.position.x + Yincrement, transform.position.y);
            transform.position = targetPos;
          

        }
        */
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player") && (scriptskin.index != 10&& scriptskin.index != 13)&&isdamage == true)
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Instantiate(sound, transform.position, Quaternion.identity);
            other.GetComponent<Player>().health -= damage;
            Destroy(gameObject);
            isdamage = false;
        }
        // if (other.CompareTag("Player") && script.IsDamage == true)
        //{
        //    other.GetComponent<Player>().health -= damage;
        //    Destroy(gameObject);
        //  }
        if(other.CompareTag("Player")&& (scriptskin.index != 10 || scriptskin.index != 13))
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (other.CompareTag("Destroyer"))
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Instantiate(sound, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}