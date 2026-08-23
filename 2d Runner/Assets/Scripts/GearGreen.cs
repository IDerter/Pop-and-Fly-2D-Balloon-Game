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
    public bool IsEntrance = true;
    public CircleCollider2D collider2d;

    private float initialSpeed; // Чтобы помнить стартовую скорость

    void Awake()
    {
        anim = GetComponent<Animator>();
        initialSpeed = speed; // Сохраняем исходную скорость
    }

    // Срабатывает при рождении из пула — включаем всё обратно
    private void OnEnable()
    {
        IsEntrance = true;
        istrigger = true;
        speed = initialSpeed; // Возвращаем скорость, если она сбрасывалась об CentrePlayer

        if (collider2d != null)
        {
            collider2d.enabled = true; // Включаем коллайдер
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
            ObjectPoolManager.Instance.ReturnToPool(gameObject);
        }
        else if (other.gameObject.CompareTag("CentrePlayer"))
        {
            speed = 0f;
        }
        else if (other.CompareTag("Player") && IsEntrance)
        {
            Player playerScript = other.GetComponent<Player>();
            
            if (playerScript != null)
            {
                playerScript.PlayEatJuice();
                playerScript.AddScore(yellowballon);
            }

            Sound.PopUp.Play();
            IsEntrance = false;
            
            if (anim != null) 
            {
                anim.SetInteger("greenfly", 2);
            }
            
            Instantiate(effect, transform.position, Quaternion.identity);

            if (collider2d != null) collider2d.enabled = false;
            
            ObjectPoolManager.Instance.ReturnToPool(gameObject);
            return;
        }
        else if (other.gameObject.CompareTag("DiagonalEnemy") || other.gameObject.CompareTag("EnemyTeleport") || other.gameObject.CompareTag("IronEnemy"))
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            ObjectPoolManager.Instance.ReturnToPool(gameObject);
        }
        
        istrigger = true;
    }
}