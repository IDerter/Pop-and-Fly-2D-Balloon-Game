using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening; // Подключаем DOTween

public class LevelChangerDotween : MonoBehaviour
{
    [Header("Настройки перехода")]
    [SerializeField] private Image fadeImage; // Ссылка на черную картинку, перекрывающую экран
    [SerializeField] private float fadeDuration = 1f; // Длительность затемнения в секундах

    private void Start()
    {
        // Опционально: автоматическое плавное появление сцены (Fade In) при старте
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            fadeImage.raycastTarget = true; // Блокируем клики, пока сцена появляется
            
            // Устанавливаем альфу на 1 (полностью непрозрачный), затем плавно уводим в 0
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1f);
            fadeImage.DOFade(0f, fadeDuration).OnComplete(() => 
            {
                fadeImage.raycastTarget = false; // Разрешаем кликать по UI после появления сцены
            });
        }
    }

    // Метод для вызова при нажатии на кнопку перехода
    public void FadeToLevel(int levelIndex)
    {
        if (fadeImage == null) return;

        fadeImage.gameObject.SetActive(true);
        fadeImage.raycastTarget = true; // Сразу блокируем UI, чтобы игрок не нажал ничего лишнего
            
        // Плавно меняем прозрачность картинки до 1, и только потом грузим сцену
        fadeImage.DOFade(1f, fadeDuration).OnComplete(() =>
        {
            SceneManager.LoadScene(levelIndex);
        });
    }
}