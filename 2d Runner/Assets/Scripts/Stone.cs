using UnityEngine;

public class Stone : MonoBehaviour
{
    [Header("Effects")]
    public GameObject effect;
    public GameObject sound;
    
    [Header("Settings")]
    public int damage = 1;
    public bool isdamage = true;
    
    private Player script;
    private SetSkinFons scriptnew;
    private const string achiv7 = "CgkIu-eNx_IEEAIQCA";

    private void Start()
    {
        // Ищем объекты только один раз при старте
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) script = playerObj.GetComponent<Player>();

        GameObject imageObj = GameObject.FindGameObjectWithTag("Image");
        if (imageObj != null) scriptnew = imageObj.GetComponent<SetSkinFons>();
    }

    // ВАЖНО: Этот метод вызывается каждый раз, когда пул достает камень.
    // Здесь мы сбрасываем все настройки до стандартных!
    private void OnEnable()
    {
        isdamage = true;
    }

    private void GetTheAchiv(string id)
    {
        Social.ReportProgress(id, 100.0f, (bool success) =>
        {
            if (success) print("Получено достижение " + id);
        });
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Используем CompareTag — это работает намного быстрее, чем collision.gameObject.tag == "..."
        if (collision.gameObject.CompareTag("DiagonalEnemy") || 
            collision.gameObject.CompareTag("Enemy") || 
            collision.gameObject.CompareTag("EnemyTeleport") || 
            collision.gameObject.CompareTag("Player") || 
            collision.gameObject.CompareTag("IronEnemy"))
        {
            Debug.Log("Пшл нфг урд");
            SpawnEffects();
            
            // Заменили Destroy на возврат в пул
            ObjectPoolManager.Instance.ReturnToPool(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isdamage)
        {
            SpawnEffects();
            
            // Наносим урон
            if (script != null)
            {
                script.TakeDamage();
            }
            
            isdamage = false; // Блокируем повторный урон в этом же кадре
            
            // Заменили Destroy на возврат в пул
            ObjectPoolManager.Instance.ReturnToPool(gameObject);
        }
    }

    // Вынес создание эффектов в отдельный метод, чтобы не дублировать код
    private void SpawnEffects()
    {
        if (effect != null) Instantiate(effect, transform.position, Quaternion.identity);
        if (sound != null) Instantiate(sound, transform.position, Quaternion.identity);
    }
}