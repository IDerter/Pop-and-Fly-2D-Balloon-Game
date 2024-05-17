using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizationText : MonoBehaviour
{
    private TextMeshProUGUI text;
    private string key;

    private void Start()
    {
        Localize();
        //PlayerPrefs.SetInt("KeyId", id);
        ManagerLocalization.OnLanguageChange += OnLanguageChange;
    }

    private void OnDestroy()
    {
        ManagerLocalization.OnLanguageChange -= OnLanguageChange;
    }

    private void OnLanguageChange()
    {
        Localize();
    }

    private void Init()
    {
        text = GetComponent<TextMeshProUGUI>();
        key = text.text;
    }

    public void Localize(string newKey = null)
    {
        if (text == null)
            Init();

        if (newKey != null)
            key = newKey;

        text.SetText(ManagerLocalization.GetTranslate(key));
        Debug.Log("text.SetText(ManagerLocalization.GetTranslate(key))");
    }
}