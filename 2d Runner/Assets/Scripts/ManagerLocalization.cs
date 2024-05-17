using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class ManagerLocalization : MonoBehaviour
{
    public static int SelectedLanguage { get; private set; }

    public static event LanguageChangeHandler OnLanguageChange;
    public delegate void LanguageChangeHandler();
    public GameObject panelloc;
    private static Dictionary<string, List<string>> localization;

    [SerializeField]
    private TextAsset textFile;
    private void Start()
    {
        SelectedLanguage = PlayerPrefs.GetInt("KeyId", SelectedLanguage); //присваиваем значение из PlayerPrefs(сохранения)
        SetLanguage(SelectedLanguage);
    }
    private void Awake()
    {
        
        if (localization == null)
            LoadLocalization();
    }

    public void SetLanguage(int id)
    {
        SelectedLanguage = id;
        Debug.Log("Selectedlanguage");
        PlayerPrefs.SetInt("KeyId", SelectedLanguage);
        OnLanguageChange?.Invoke();
        panelloc.SetActive(false);

        //PlayerPrefs.SetInt(key, (int) SelectedLanguage);
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
        

        if (localization.ContainsKey(key))
            return localization[key][languageId];
        
        return key;
        
    }
}