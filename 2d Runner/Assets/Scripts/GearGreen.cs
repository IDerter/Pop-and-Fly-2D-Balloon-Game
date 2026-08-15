using UnityEngine;
using DG.Tweening; 

public class GearGreen : MonoBehaviour
{
    public int yellowballon = 1;
    public float speed;
    public GameObject effect;
    public Transform yellowballon1;
    Animator anim;
    public bool istrigger = true;
    public GameObject sound;
    public bool IsEntrance = true;
    public CircleCollider2D collider2d;
    public SpriteRenderer spriteRenderer;
    public SetSkin scriptskin;

    void Start()
    {
        anim = GetComponent<Animator>();
        
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            scriptskin = playerObj.GetComponent<SetSkin>();
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.down * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("CentrePlayer"))
        {
            speed = 0f;
        }
        
        // Столкновение с игроком (Ам Нямом)
        else if (other.CompareTag("Player") && IsEntrance == true)
        {
            Player playerScript = other.GetComponent<Player>();
            
            if (playerScript != null)
            {
                // 1. Анимация еды и "глоток" персонажа
                playerScript.PlayEatJuice();

                // 2. Начисление очков через наш новый метод с событием (х2 для 7-го скина)
                int pointsToAdd = yellowballon;
               // if (scriptskin != null && scriptskin.index == 7) // пока отключаю, потом можно добавить x2 от скина
               // {
              //      pointsToAdd *= 2;
              //  }

                // Вызываем метод AddScore, который триггерит событие для UI!
                playerScript.AddScore(pointsToAdd);
            }

            // 3. Звук и эффекты
            Instantiate(sound, transform.position, Quaternion.identity);
            IsEntrance = false;
            
            if (anim != null) 
            {
                anim.SetInteger("greenfly", 2);
            }
            
            Instantiate(effect, transform.position, Quaternion.identity);

            // 4. Мгновенное удаление с растворением (Fade Out)
            if (collider2d != null) collider2d.enabled = false;
            
            if (spriteRenderer != null)
            {
                spriteRenderer.DOFade(0f, 0.15f).OnComplete(() => Destroy(gameObject));
            }
            else
            {
                Destroy(gameObject);
            }
            
            return;
        }
        
        else if (other.gameObject.CompareTag("DiagonalEnemy") || other.gameObject.CompareTag("EnemyTeleport") || other.gameObject.CompareTag("IronEnemy"))
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
        istrigger = true;
    }
}