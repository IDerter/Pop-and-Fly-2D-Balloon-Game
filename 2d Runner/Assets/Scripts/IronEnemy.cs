using UnityEngine;

public class IronEnemy : MonoBehaviour
{
    [Header("Settings")]
    public float speed;
    public Vector2 direction = Vector2.right; // В Инспекторе поставьте (-1, 0) для левого врага
    
    [Header("Effects & References")]
    public GameObject effect;
    public GameObject sound;
    
    private SetSkin scriptSkin;
    public static int countEnemies = 0;

    private void Start()
    {
        // 1. Ищем игрока ТОЛЬКО один раз при появлении объекта, а не каждый кадр!
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            scriptSkin = playerObj.GetComponent<SetSkin>();
        }
        
        countEnemies = PlayerPrefs.GetInt("CountIronEnemy", 0);
    }

    private void FixedUpdate()
    {
        // Умножаем направление на скорость. 
        // Теперь один скрипт может двигать врага куда угодно!
        transform.Translate(direction * speed);
    }

    public void OnMouseDown()
    {
        SpawnEffects();
        
        countEnemies++;
        PlayerPrefs.SetInt("CountIronEnemy", countEnemies);
        
        if (countEnemies >= 10)
        {
            Debug.Log("YbrbnfLox"); // Ваша пасхалка сохранена :)
        }

        // 2. Используем Пул Объектов вместо Destroy
        ObjectPoolManager.Instance.ReturnToPool(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnEffects();
            
            // 3. Логика скина: если индекс НЕ 13, то наносим урон
            if (scriptSkin != null && scriptSkin.index != 13)
            {
                Player playerScript = other.GetComponent<Player>();
                if (playerScript != null)
                {
                    playerScript.TakeDamage();
                }
                Debug.Log("не13скин - урон нанесен");
            }
            else
            {
                Debug.Log("13скин - защита сработала");
            }
            
            // Враг в любом случае исчезает при касании
            ObjectPoolManager.Instance.ReturnToPool(gameObject);
        }
    }

    // Вспомогательный метод, чтобы не дублировать код создания эффектов
    private void SpawnEffects()
    {
        if (effect != null) Instantiate(effect, transform.position, Quaternion.identity);
        if (sound != null) Instantiate(sound, transform.position, Quaternion.identity);
    }
}