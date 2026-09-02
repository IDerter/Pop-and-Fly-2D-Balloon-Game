using UnityEngine;

public class PointGreen : MonoBehaviour
{
    public GameObject gearyellow; 
    
    [Header("Настройка направления")]
    public Vector2 flyDirection; 

    [Header("Настройка границ")]
    [Tooltip("Отступ от края экрана, чтобы враг влезал целиком (в юнитах)")]
    public float margin = 1.2f; 

    void Start()
    {
        if (gearyellow != null)
        {
            // ИЗМЕНЕНИЕ ЗДЕСЬ: Берем аспект камеры, а не физического экрана телефона!
            float cameraAspect = Camera.main.aspect;
            float cameraHeight = Camera.main.orthographicSize;
            float cameraWidth = cameraHeight * cameraAspect;

            // 2. Берем текущую позицию точки (например, X = -6)
            Vector3 safePosition = transform.position;

            // 3. "Зажимаем" позицию X так, чтобы она не выходила за края камеры минус наш отступ
            safePosition.x = Mathf.Clamp(safePosition.x, -cameraWidth + margin, cameraWidth - margin);

            // 4. Спавним объект в безопасной позиции (safePosition)
            GameObject spawnedBee = ObjectPoolManager.Instance.SpawnFromPool(gearyellow, safePosition, Quaternion.identity, null);
            
            DiagonalEnemy beeScript = spawnedBee.GetComponent<DiagonalEnemy>();
            if (beeScript != null)
            {
                beeScript.direction = flyDirection;
            }
        }
    }
}