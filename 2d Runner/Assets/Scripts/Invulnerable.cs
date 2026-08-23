using UnityEngine;

public class Invulnerable : MonoBehaviour 
{
    public Vector2 direction;
    public GameObject effect;

    private void FixedUpdate()
    {
        // Просто двигаем объект
        transform.Translate(direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Используем CompareTag для оптимизации
        if (collision.gameObject.CompareTag("DiagonalEnemy") || collision.gameObject.CompareTag("Enemy"))
        {
            if (effect != null) Instantiate(effect, transform.position, Quaternion.identity);
            
            // Если ты уже перевел спавн этого объекта на Object Pool, 
            // замени Destroy на ObjectPoolManager.Instance.ReturnToPool(gameObject);
            Destroy(gameObject); 
        }
    }
}