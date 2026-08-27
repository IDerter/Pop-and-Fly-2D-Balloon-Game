using UnityEngine;

public class KeepPlayerInBounds : MonoBehaviour
{
    [Header("Настройки границ")]
    [Tooltip("Отступ от края, чтобы персонаж не прилипал к самой рамке (в юнитах)")]
    public float margin = 0.5f;

    // LateUpdate вызывается каждый кадр ПОСЛЕ того, как отработала физика и передвижение
    private void LateUpdate()
    {
        if (Camera.main == null) return;

        // Вычисляем текущую ширину экрана
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * screenAspect;

        // Берем текущую позицию дракоши
        Vector3 pos = transform.position;

        // Проверяем: если дракоша оказался левее левого края экрана...
        if (pos.x < -cameraWidth + margin)
        {
            pos.x = -cameraWidth + margin; // ...силой возвращаем его в экран
        }
        // Если оказался правее правого края...
        else if (pos.x > cameraWidth - margin)
        {
            pos.x = cameraWidth - margin; // ...силой возвращаем его в экран
        }

        // Применяем исправленную позицию
        transform.position = pos;
    }
}