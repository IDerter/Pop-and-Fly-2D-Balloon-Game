using UnityEngine;
using DG.Tweening; 

public class YellowEnemy : MonoBehaviour 
{
    public float speed; // Оставил, если эта переменная читается спавнером
    public Transform gear; // Позиция желтого шарика
    public GameObject effect;
    public GameObject sound;
    public int damage = 1;
    public Vector2 direction;
    public bool isdamage = true;


    private void FixedUpdate()
    {
        // Вся логика движения теперь работает максимально быстро
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
                playerScript.TakeDamage();
            }
            
            isdamage = false;
            ObjectPoolManager.Instance.ReturnToPool(gameObject);
        }
        // 2. Столкновение с границей для удаления
        else if (other.CompareTag("Destroyer"))
        {
            if (effect != null) Instantiate(effect, transform.position, Quaternion.identity);
            if (sound != null) Instantiate(sound, transform.position, Quaternion.identity);
            
            ObjectPoolManager.Instance.ReturnToPool(gameObject);
        }
    }
}