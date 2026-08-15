using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class ManagerLocalization : SingletonBase<ManagerLocalization>
{
    public static int SelectedLanguage { get; private set; }

    public static event LanguageChangeHandler OnLanguageChange;
    public delegate void LanguageChangeHandler();
    public GameObject panelloc;
    private static Dictionary<string, List<string>> localization;

    [SerializeField]
    private TextAsset textFile;

    protected override void Awake()
    {
        // Обязательно вызываем базовый Awake, чтобы сработал SingletonBase (проверка дубликатов и DontDestroyOnLoad)
        base.Awake();

        if (localization == null)
            LoadLocalization();
    }

    private void Start()
    {
        SelectedLanguage = PlayerPrefs.GetInt("KeyId", SelectedLanguage); // присваиваем значение из PlayerPrefs (сохранения)
        SetLanguage(SelectedLanguage);
    }

    public void SetLanguage(int id)
    {
        SelectedLanguage = id;
        Debug.Log("Selectedlanguage");
        PlayerPrefs.SetInt("KeyId", SelectedLanguage);
        OnLanguageChange?.Invoke();
        
        if (panelloc != null)
            panelloc.SetActive(false);
    }

    private void LoadLocalization()
    {
        localization = new Dictionary<string, List<string>>();

        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(textFile.text);

        foreach (XmlNode key in xmlDocument["Keys"].ChildNodes)
        {
            string keyStr = key.Attributes["Name"].Value;

            var values = new List<string>();
            foreach (XmlNode translate in key["Translates"].ChildNodes)
                values.Add(translate.InnerText);
                
            localization[keyStr] = values;
            Debug.Log("localization[keyStr] = values");
        }
    }

    public static string GetTranslate(string key, int languageId = -1)
    {
        if (languageId == -1)
            languageId = SelectedLanguage;
        
        if (localization != null && localization.ContainsKey(key))
            return localization[key][languageId];
        
        return key;
    }
}