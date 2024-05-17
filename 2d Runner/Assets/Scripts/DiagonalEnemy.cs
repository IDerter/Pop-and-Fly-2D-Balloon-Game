using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalEnemy : MonoBehaviour {

    public float speed;
    public Transform gear;//позиция красного шарика
    public GameObject effect;
    public GameObject sound;
    public int damage = 1;
    public Player script;
    public Vector2 direction;
    public SetSkin scriptskin;
    public bool isdamage = true;
    // Use this for initialization
    void Start () {
        GameObject.Find("AllObjectOnScene");
        GameObject.Find("SpawnerEnemyBlue");
        GameObject.Find("Player");
        scriptskin = GameObject.FindGameObjectWithTag("Player").GetComponent<SetSkin>();
       // gear.transform.SetParent(GameObject.Find("AllObjectOnScene").transform);
    }
	
	// Update is called once per frame
	
    private void FixedUpdate()
    {
        scriptskin = GameObject.FindGameObjectWithTag("Player").GetComponent<SetSkin>();
        transform.Translate(direction);
       
    }
    private void Update()
    {
        scriptskin = GameObject.FindGameObjectWithTag("Player").GetComponent<SetSkin>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if  (other.CompareTag("Player")&&(scriptskin.index!=9&& scriptskin.index != 13)&&isdamage == true)
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Instantiate(sound, transform.position, Quaternion.identity);
            other.GetComponent<Player>().health -= damage;
            Destroy(gameObject);
            isdamage = false;
       }
        //if(other.CompareTag("Score") || other.CompareTag("Enemy"))
        //{
         //   Instantiate(effect, transform.position, Quaternion.identity);
          //  Instantiate(sound, transform.position, Quaternion.identity);
          //  Destroy(gameObject);
       // }
        else if((scriptskin.index == 9|| scriptskin.index == 13)&&other.CompareTag("Player"))
            {
            Instantiate(effect, transform.position, Quaternion.identity);
            Instantiate(sound, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        // if (other.CompareTag("Player") && script.IsDamage == true)
        //{
        //    other.GetComponent<Player>().health -= damage;
        //    Destroy(gameObject);
        //  }
        if (other.CompareTag("Destroyer")|| other.CompareTag("IronEnemy")|| other.CompareTag("EnemyTeleport"))
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Instantiate(sound, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
