using UnityEngine;

public class ScreenBoundary : MonoBehaviour
{
    public enum BoundarySide { Left, Right }
    
    [Header("Настройки границы")]
    [Tooltip("Какая это сторона экрана?")]
    public BoundarySide side;

    [Tooltip("Отступ от края экрана. Положительное число сдвигает границу НАРУЖУ (за пределы камеры).")]
    public float offset = 0f;

    // Ставим -1, чтобы скрипт гарантированно обновил границы в первом же кадре
    private int _lastScreenWidth = -1;
    private int _lastScreenHeight = -1;

    private void Start()
    {
        // Убрали вызов отсюда. Ждем, пока отработает скрипт камеры!
    }

    // LateUpdate срабатывает строго ПОСЛЕ того, как все скрипты сделали свои дела в Update
    private void LateUpdate()
    {
        // Проверяем, не изменился ли размер окна (сработает сразу при запуске из-за -1)
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

        Camera cam = Camera.main;
        
        float cameraHeight = cam.orthographicSize;
        float cameraWidth = cameraHeight * cam.aspect;
        float cameraX = cam.transform.position.x;

        Vector3 newPos = transform.position;

        if (side == BoundarySide.Left)
        {
            newPos.x = cameraX - cameraWidth - offset; 
        }
        else if (side == BoundarySide.Right)
        {
            newPos.x = cameraX + cameraWidth + offset; 
        }

        transform.position = newPos;
    }
}