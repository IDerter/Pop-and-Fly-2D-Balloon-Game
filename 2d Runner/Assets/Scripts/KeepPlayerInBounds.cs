using UnityEngine;

public class KeepPlayerInBounds : MonoBehaviour
{
    [Header("Настройки границ")]
    [Tooltip("Отступ от края, чтобы персонаж не прилипал к самой рамке (в юнитах)")]
    public float margin = 0.5f;

    private void LateUpdate()
    {
        if (Camera.main == null) return;

        // ИЗМЕНЕНИЕ ЗДЕСЬ: Берем аспект камеры (Camera.main.aspect), а не экрана!
        // Он автоматически учитывает черные полосы от CameraAspectCrop.
        float cameraAspect = Camera.main.aspect; 
        float cameraHeight = Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * cameraAspect;

        Vector3 pos = transform.position;

        if (pos.x < -cameraWidth + margin)
        {
            pos.x = -cameraWidth + margin; 
        }
        else if (pos.x > cameraWidth - margin)
        {
            pos.x = cameraWidth - margin; 
        }

        transform.position = pos;
    }
}