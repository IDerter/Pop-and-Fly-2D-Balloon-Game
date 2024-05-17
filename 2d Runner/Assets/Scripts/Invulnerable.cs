using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invulnerable : MonoBehaviour {
    public Vector2 direction;
    public GameObject effect;
    public GameObject invulnerable;
    // Use this for initialization
    void Start () {
        GameObject.Find("AllObjectOnScene");
        invulnerable.transform.SetParent(GameObject.Find("AllObjectOnScene").transform); 
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.Translate(direction);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DiagonalEnemy" || collision.gameObject.tag == "Enemy")
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
