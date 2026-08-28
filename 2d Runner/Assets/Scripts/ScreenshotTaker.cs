using UnityEngine;

public class ScreenshotTaker : SingletonBase<ScreenshotTaker>
{
    void Update()
    {
        // При нажатии на английскую 'K' игра сделает скриншот
        if (Input.GetKeyDown(KeyCode.K))
        {
            // Формируем уникальное имя файла
            string fileName = "Screenshot_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
            
            // Сохраняем скриншот (цифра 1 означает масштаб 1x)
            ScreenCapture.CaptureScreenshot(fileName, 1);
            
            Debug.Log("Скриншот сохранен: " + fileName);
        }
    }
}