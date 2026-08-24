using UnityEngine;

public class Magnit : MonoBehaviour 
{
    public Vector2 direction;
    public GameObject effect;

    private void FixedUpdate()
    {
        // Двигаем магнит
        transform.Translate(direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Быстрая проверка тегов без создания мусора в памяти
        if (collision.gameObject.CompareTag("DiagonalEnemy") || 
            collision.gameObject.CompareTag("Enemy") || 
            collision.gameObject.CompareTag("EnemyTeleport") || 
            collision.gameObject.CompareTag("IronEnemy"))
        {
            if (effect != null) Instantiate(effect, transform.position, Quaternion.identity);
            
            // Замени на ObjectPoolManager.Instance.ReturnToPool(gameObject); если используешь пул
            ObjectPoolManager.Instance.ReturnToPool(gameObject);
        }
    }
}