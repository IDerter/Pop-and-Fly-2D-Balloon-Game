using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearGreenSnow : MonoBehaviour
{
    public int yellowballon = 1;
    public float speed;
    public GameObject effect;
    public Transform yellowballon1;
    Animator anim;
    public bool istrigger = true;
    public GameObject sound;
    public bool IsEntrance = true;
    public CircleCollider2D collider2d;
    public SetSkin scriptskin;
    public static int bestscore = 0;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        GameObject.Find("AllObjectOnScene");
        scriptskin = GameObject.FindGameObjectWithTag("Player").GetComponent<SetSkin>();
        //yellowballon1.transform.SetParent(GameObject.Find("AllObjectOnScene").transform);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        scriptskin = GameObject.FindGameObjectWithTag("Player").GetComponent<SetSkin>();
        transform.Translate(Vector2.down * speed);
    }
    private void Update()
    {
        scriptskin = GameObject.FindGameObjectWithTag("Player").GetComponent<SetSkin>();

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {

            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "CentrePlayer")
        {
            speed = 0f;
        }
        //Instantiate(effect, transform.position, Quaternion.identity);
        if (other.CompareTag("Player") && IsEntrance == true && scriptskin.index != 7)
        {
            other.GetComponent<Player>().score += yellowballon;
            other.GetComponent<Player>().coin += yellowballon;
            //speed = 0f;
            //collider2d.enabled = false; 
            Instantiate(sound, transform.position, Quaternion.identity);
            IsEntrance = false;
            anim.GetComponent<Animator>().enabled = true;
            Instantiate(effect, transform.position, Quaternion.identity);
            Debug.Log("Entrance");
            //other.GetComponent<Player>().score += yellowballon;
            Destroy(gameObject, 0.5f);

        }
        else if (other.CompareTag("Player") && IsEntrance == true && scriptskin.index == 7)
        {
            other.GetComponent<Player>().score += yellowballon + yellowballon;
            other.GetComponent<Player>().coin += yellowballon + yellowballon;
            //speed = 0f;
            //collider2d.enabled = false; 
            Instantiate(sound, transform.position, Quaternion.identity);
            IsEntrance = false;
            anim.GetComponent<Animator>().enabled = true;
            Instantiate(effect, transform.position, Quaternion.identity);
            Debug.Log("Entrance");
            //other.GetComponent<Player>().score += yellowballon;
            Destroy(gameObject, 0.5f);
        }
        if (other.gameObject.tag == "DiagonalEnemy" || other.gameObject.tag == "EnemyTeleport" || other.gameObject.tag == "IronEnemy")
        {
            Instantiate(effect, transform.position, Quaternion.identity);

            //Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        istrigger = true;
    }

}