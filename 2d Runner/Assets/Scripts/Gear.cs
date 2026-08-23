using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public float speed;
    public Transform gear; 
    public GameObject effect;
    public GameObject sound;
    public bool isdamage = true;

    // Срабатывает при доставании из пула — сбрасываем логику урона
    private void OnEnable()
    {
        isdamage = true;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.down * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isdamage)
        {
            if (effect != null) Instantiate(effect, transform.position, Quaternion.identity);
            if (sound != null) Instantiate(sound, transform.position, Quaternion.identity);
            
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.health -= 1;
            }
            
            isdamage = false;
            ObjectPoolManager.Instance.ReturnToPool(gameObject);
        }
        else if (other.CompareTag("DiagonalEnemy") || other.CompareTag("EnemyTeleport") || other.CompareTag("IronEnemy"))
        {
            if (effect != null) Instantiate(effect, transform.position, Quaternion.identity);
            ObjectPoolManager.Instance.ReturnToPool(gameObject);
        }
        else if (other.CompareTag("Destroyer"))
        {
            ObjectPoolManager.Instance.ReturnToPool(gameObject);
        }
    }
}