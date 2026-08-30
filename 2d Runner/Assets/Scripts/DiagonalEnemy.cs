using UnityEngine;

public class DiagonalEnemy : MonoBehaviour 
{
    public float speed = 1f; // Можно умножать direction на speed в FixedUpdate
    public Transform gear; 
    public GameObject effect;

    [Header("Audio")]
    [Tooltip("Выбери звук из выпадающего списка")]
    public Sound hitSound;
    
    // Направление теперь задается из PointGreen
    public Vector2 direction; 
    public bool isdamage = true;
    
    // ВАЖНО: Сбрасываем состояние при доставании из пула
    private void OnEnable()
    {
        isdamage = true;
    }

    private void FixedUpdate()
    {
        // Умножаем на скорость и Time.fixedDeltaTime для плавности
        transform.Translate(direction * speed * Time.fixedDeltaTime);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isdamage)
        {
            SpawnEffects();
            
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(); 
            }
            
            isdamage = false;
            ObjectPoolManager.Instance.ReturnToPool(gameObject);
        }
        else if (other.CompareTag("Destroyer") || other.CompareTag("IronEnemy") || other.CompareTag("EnemyTeleport"))
        {
            SpawnEffects();
            ObjectPoolManager.Instance.ReturnToPool(gameObject);
        }
    }

    // Вынес эффекты, чтобы не дублировать код
    private void SpawnEffects()
    {
        if (effect != null) Instantiate(effect, transform.position, Quaternion.identity);
        hitSound.Play();
    }
}