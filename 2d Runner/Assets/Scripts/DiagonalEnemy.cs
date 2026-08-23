using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalEnemy : MonoBehaviour 
{
    public float speed; // Скорость (если она нужна для расчетов, хотя сейчас используется только direction)
    public Transform gear; // Позиция красного шарика
    public GameObject effect;
    public GameObject sound;
    public int damage = 1;
    public Vector2 direction;
    public bool isdamage = true;
    
    private void FixedUpdate()
    {
        // Просто двигаем объект в заданном направлении
        transform.Translate(direction);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Столкновение с игроком
        if (other.CompareTag("Player") && isdamage)
        {
            if (effect != null) Instantiate(effect, transform.position, Quaternion.identity);
            if (sound != null) Instantiate(sound, transform.position, Quaternion.identity);
            
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.health -= damage;
            }
            
            isdamage = false;
            ObjectPoolManager.Instance.ReturnToPool(gameObject);
        }
        // 2. Столкновение с границами или другими препятствиями
        else if (other.CompareTag("Destroyer") || other.CompareTag("IronEnemy") || other.CompareTag("EnemyTeleport"))
        {
            if (effect != null) Instantiate(effect, transform.position, Quaternion.identity);
            if (sound != null) Instantiate(sound, transform.position, Quaternion.identity);
            
            ObjectPoolManager.Instance.ReturnToPool(gameObject);
        }
    }
}