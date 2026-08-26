using UnityEngine;

public class PointGreen : MonoBehaviour
{
    public GameObject gearyellow; // Ваш префаб пчелы
    
    [Header("Настройка направления")]
    [Tooltip("Куда полетит пчела, появившаяся из этой точки?")]
    public Vector2 flyDirection; 

    void Start()
    {
        if (gearyellow != null)
        {
            // 1. Используем ПУЛ ОБЪЕКТОВ вместо Instantiate!
            GameObject spawnedBee = ObjectPoolManager.Instance.SpawnFromPool(gearyellow, transform.position, Quaternion.identity, null);
            
            // 2. Находим скрипт пчелы и передаем ей направление от этой точки
            DiagonalEnemy beeScript = spawnedBee.GetComponent<DiagonalEnemy>();
            if (beeScript != null)
            {
                beeScript.direction = flyDirection;
            }
        }
    }
}