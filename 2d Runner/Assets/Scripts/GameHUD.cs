using UnityEngine;
using TMPro;

public class GameHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI candyCountText;
    private Player player;

    private void Start()
    {
        // Находим игрока на сцене (можно закешировать)
        player = FindObjectOfType<Player>();

        if (player != null)
        {
            // Подписываемся на событие изменения счета
            player.OnScoreChanged += UpdateScoreUI;
            
            // Сразу выводим текущее значение на старте
            UpdateScoreUI(player.score);
        }
    }

    private void OnDestroy()
    {
        // Обязательно отписываемся, чтобы не было утечек памяти при уничтожении объекта
        if (player != null)
        {
            player.OnScoreChanged -= UpdateScoreUI;
        }
    }

    private void UpdateScoreUI(int newScore)
    {
        if (candyCountText != null)
        {
            candyCountText.text = newScore.ToString();
        }
    }
}