using UnityEngine;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    [SerializeField] private string key; // Ключ для локализации

    private TextMeshProUGUI textMesh;

    void Awake()
    {
        if (textMesh == null)
        {
            textMesh = GetComponent<TextMeshProUGUI>();
        }
    }

    void Start()
    {
        UpdateText();
    }

    private void OnEnable()
    {
        // Подписываемся на событие смены языка, чтобы текст обновлялся на лету
        ManagerLocalization.OnLanguageChange += UpdateText;
    }

    private void OnDisable()
    {
        // Обязательно отписываемся
        ManagerLocalization.OnLanguageChange -= UpdateText;
    }

    public void UpdateText()
    {
        if (textMesh == null)
        {
            textMesh = GetComponent<TextMeshProUGUI>();
        }

        if (textMesh != null && !string.IsNullOrEmpty(key))
        {
            // Обращаемся напрямую к синглтону через статический метод
            textMesh.text = ManagerLocalization.GetTranslate(key);
        }
    }
}