using UnityEngine;

public class ScreenBoundary : MonoBehaviour
{
    public enum BoundarySide { Left, Right }
    
    [Header("Настройки границы")]
    [Tooltip("Какая это сторона экрана?")]
    public BoundarySide side;

    [Tooltip("Отступ от края экрана. Положительное число сдвигает внутрь экрана.")]
    public float offset = 0.5f;

    private int _lastScreenWidth;
    private int _lastScreenHeight;

    private void Start()
    {
        UpdateBoundaryPosition();
    }

    private void Update()
    {
        // Проверяем, не изменился ли размер окна браузера/экрана
        if (Screen.width != _lastScreenWidth || Screen.height != _lastScreenHeight)
        {
            UpdateBoundaryPosition();
        }
    }

    private void UpdateBoundaryPosition()
    {
        if (Camera.main == null) return;

        _lastScreenWidth = Screen.width;
        _lastScreenHeight = Screen.height;

        // Вычисляем реальные размеры камеры в игровых координатах
        float cameraHeight = Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        Vector3 newPos = transform.position;

        // Сдвигаем объект к левому или правому краю
        if (side == BoundarySide.Left)
        {
            newPos.x = -cameraWidth + offset;
        }
        else if (side == BoundarySide.Right)
        {
            newPos.x = cameraWidth - offset;
        }

        transform.position = newPos;
    }
}