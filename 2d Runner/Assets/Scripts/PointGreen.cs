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
            // 1. Вычисляем реальную ширину видимой зоны камеры
            float screenAspect = (float)Screen.width / (float)Screen.height;
            float cameraHeight = Camera.main.orthographicSize;
            float cameraWidth = cameraHeight * screenAspect;

            // 2. Берем текущую позицию точки (например, X = -6)
            Vector3 safePosition = transform.position;

            // 3. "Зажимаем" позицию X так, чтобы она не выходила за края экрана минус наш отступ
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