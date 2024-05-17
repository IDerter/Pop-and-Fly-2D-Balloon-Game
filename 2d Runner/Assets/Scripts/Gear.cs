using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public float speed;
    public Transform gear;//позиция красного шарика
    public GameObject effect;
    private Rigidbody2D rb2d;
    public GameObject sound;
    public GameObject sound1;
    public SetSkin scriptskin;
    public bool isdamage = true;
    void Start()
    {
        GameObject.Find("AllObjectOnScene");
        scriptskin = GameObject.FindGameObjectWithTag("Player").GetComponent<SetSkin>();
    }

    private void FixedUpdate()
    {
        scriptskin = GameObject.FindGameObjectWithTag("Player").GetComponent<SetSkin>();
        transform.Translate(Vector2.down * speed);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player") && (scriptskin.index != 12 && scriptskin.index != 13)&&isdamage == true)
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Instantiate(sound, transform.position, Quaternion.identity);
            other.GetComponent<Player>().health -= 1;
            Destroy(gameObject);
            isdamage = false;
        }
        else if (other.CompareTag("Player")&&(scriptskin.index == 12|| scriptskin.index == 13))
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Instantiate(sound1, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "DiagonalEnemy" || other.gameObject.tag == "EnemyTeleport" || other.gameObject.tag == "IronEnemy")
        {

            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (other.CompareTag("Destroyer"))
        {
            Destroy(gameObject);
        }
    }


    /*private void Update()
 {
     scriptskin = GameObject.FindGameObjectWithTag("Player").GetComponent<SetSkin>();
 }
 */
}
