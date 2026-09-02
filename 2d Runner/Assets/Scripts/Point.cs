using UnityEngine;

public class Point : MonoBehaviour 
{
    public GameObject gear; // Префаб конкретного врага (например, SpiderEnemy)

    [Header("Настройка границ")]
    [Tooltip("Отступ от края экрана, чтобы враг влезал целиком (в юнитах)")]
    public float margin = 1.2f; 

    void Start () 
    {
        if (gear != null)
        {
            // ИЗМЕНЕНИЕ ЗДЕСЬ: Вычисляем реальную ширину видимой зоны КАМЕРЫ, а не экрана
            float cameraAspect = Camera.main.aspect;
            float cameraHeight = Camera.main.orthographicSize;
            float cameraWidth = cameraHeight * cameraAspect;

            // 2. Берем текущую позицию точки (из инспектора)
            Vector3 safePosition = transform.position;

            // 3. Сдвигаем позицию X, если она оказалась за пределами экрана
            safePosition.x = Mathf.Clamp(safePosition.x, -cameraWidth + margin, cameraWidth - margin);

            // 4. Спавним объект в вычисленной безопасной позиции. 
            // ВАЖНО: Последний параметр — null! Враг появляется и летит сам по себе, 
            // независимо от того, что произойдет с папкой Variant.
            ObjectPoolManager.Instance.SpawnFromPool(gear, safePosition, Quaternion.identity, null);
        }
    }
}