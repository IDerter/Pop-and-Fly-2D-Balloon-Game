using UnityEngine;

public class GhostEnemy : MonoBehaviour 
{
    public float speed; 
    public GameObject effect;

    [Header("Audio")]
    [Tooltip("Выбери звук из выпадающего списка")]
    public Sound hitSound; // Заменили GameObject на твой enum Sound

    public int damage = 1;
    public Vector2 direction;
    public bool isdamage = true;

    // ДОБАВИЛИ ФЛАГ
    public bool isTeleporting = false; 

    private void FixedUpdate()
    {
        // Двигаемся ТОЛЬКО если сейчас не телепортируемся
        if (!isTeleporting)
        {
            transform.Translate(direction * speed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isdamage)
        {
            if (effect != null) Instantiate(effect, transform.position, Quaternion.identity);
            hitSound.Play();
            
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null) playerScript.TakeDamage();
            
            isdamage = false;
            ObjectPoolManager.Instance.ReturnToPool(gameObject);
        }
        else if (other.CompareTag("Destroyer"))
        {
            ObjectPoolManager.Instance.ReturnToPool(gameObject);
        }
    }
}