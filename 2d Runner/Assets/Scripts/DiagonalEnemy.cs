using UnityEngine;

public class DiagonalEnemy : MonoBehaviour 
{
    public float speed; 
    public Transform gear; 
    public GameObject effect;
    public GameObject sound;
    public Vector2 direction;
    public bool isdamage = true;
    
    private void FixedUpdate()
    {
        transform.Translate(direction);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isdamage)
        {
            if (effect != null) Instantiate(effect, transform.position, Quaternion.identity);
            if (sound != null) Instantiate(sound, transform.position, Quaternion.identity);
            
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null)
            {
                // ВМЕСТО: playerScript.health -= damage;
                // ПИШЕМ:
                playerScript.TakeDamage(); 
            }
            
            isdamage = false;
            ObjectPoolManager.Instance.ReturnToPool(gameObject);
        }
        else if (other.CompareTag("Destroyer") || other.CompareTag("IronEnemy") || other.CompareTag("EnemyTeleport"))
        {
            if (effect != null) Instantiate(effect, transform.position, Quaternion.identity);
            if (sound != null) Instantiate(sound, transform.position, Quaternion.identity);
            
            ObjectPoolManager.Instance.ReturnToPool(gameObject);
        }
    }
}