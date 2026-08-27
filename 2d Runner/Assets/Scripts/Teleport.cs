using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour 
{
    public GameObject tl;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("EnemyTeleport"))
        {
            // Получаем скрипт врага
            GhostEnemy ghost = col.GetComponent<GhostEnemy>();
            
            // Если враг найден и он ЕЩЕ НЕ телепортируется
            if (ghost != null && !ghost.isTeleporting)
            {
                StartCoroutine(TeleportWithAnimation(ghost));
            }
        }
    }

    private IEnumerator TeleportWithAnimation(GhostEnemy ghost)
    {
        // 1. Блокируем движение и двойные срабатывания
        ghost.isTeleporting = true; 

        Animator animator = ghost.GetComponent<Animator>();
        if (animator != null)
        {
            // Очищаем случайные старые триггеры перед запуском
            animator.ResetTrigger("Teleport"); 
            animator.SetTrigger("Teleport");
        }
        Sound.Whoosh.Play();

        // 2. Ждем 0.5 секунды
        yield return new WaitForSeconds(0.5f);

        // 3. Телепортируем
        if (tl != null)
        {
            ghost.transform.position = tl.transform.position;
        }

        // 4. Отпускаем врага, он летит дальше
        ghost.isTeleporting = false; 
    }
}