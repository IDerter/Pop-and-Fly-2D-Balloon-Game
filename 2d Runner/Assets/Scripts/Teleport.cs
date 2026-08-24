using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour 
{
    public GameObject tl; // Точка, куда переместится враг

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("EnemyTeleport"))
        {
            // Запускаем корутину анимации и телепортации
            StartCoroutine(TeleportWithAnimation(col.transform));
        }
    }

    private IEnumerator TeleportWithAnimation(Transform enemyTransform)
    {
        // 1. Проверяем, есть ли у врага компонент Animator
        Animator animator = enemyTransform.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Teleport");
        }

        // 2. Ждем ровно 0.5 секунды, пока проигрывается анимация исчезновения/входа в портал
        yield return new WaitForSeconds(0.5f);

        // 3. Проверяем, существует ли целевая точка, и перемещаем врага
        if (tl != null)
        {
            enemyTransform.position = tl.transform.position;
        }
    }
}