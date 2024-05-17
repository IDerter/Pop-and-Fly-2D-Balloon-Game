using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnit : MonoBehaviour {
    public Vector2 direction;
    public GameObject effect;
    public GameObject magnit;
    // Use this for initialization
    void Start()
    {
        GameObject.Find("AllObjectOnScene");
        magnit.transform.SetParent(GameObject.Find("AllObjectOnScene").transform); //делаем магнит дочерним 
    }
    private void FixedUpdate()
    {
        transform.Translate(direction);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DiagonalEnemy" || collision.gameObject.tag == "Enemy"||collision.gameObject.tag == "EnemyTeleport"|| collision.gameObject.tag == "IronEnemy")
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}