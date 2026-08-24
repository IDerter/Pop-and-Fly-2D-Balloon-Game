using UnityEngine;

public class GhostEnemy : MonoBehaviour 
{
    public float speed; 
    public GameObject effect;
    public GameObject sound;
    public int damage = 1;
    public Vector2 direction;
    public bool isdamage = true;

    private void FixedUpdate()
    {
        // Призрак просто летит прямо, а всю работу по телепортации делают зоны!
        transform.Translate(direction * speed * Time.fixedDeltaTime);
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
                playerScript.TakeDamage();
            }
            
            isdamage = false;
            ObjectPoolManager.Instance.ReturnToPool(gameObject);
        }
        else if (other.CompareTag("Destroyer"))
        {
            ObjectPoolManager.Instance.ReturnToPool(gameObject);
        }
    }
}