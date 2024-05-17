﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyScript : MonoBehaviour
{
    public static int bestscore = 0;
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
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        GameObject.Find("AllObjectOnScene");
        scriptskin = GameObject.FindGameObjectWithTag("Player").GetComponent<SetSkin>();
        bestscore = PlayerPrefs.GetInt("Score1");
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
        if(other.gameObject.tag == "Player"&&IsEntrance == true)
        {
            Instantiate(sound, transform.position, Quaternion.identity);
            Instantiate(effect, transform.position, Quaternion.identity);
            other.GetComponent<Player>().scoremoney += 1;
            other.GetComponent<Player>().coin1 += 1;
            Destroy(gameObject);
            IsEntrance = false;
        }
        //Instantiate(sound, transform.position, Quaternion.identity);
        //Instantiate(effect, transform.position, Quaternion.identity);

        if (other.gameObject.tag == "DiagonalEnemy" || other.gameObject.tag == "EnemyTeleport" || other.gameObject.tag == "IronEnemy")
        {
            Instantiate(effect, transform.position, Quaternion.identity);

            //Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        istrigger = true;
    }

}