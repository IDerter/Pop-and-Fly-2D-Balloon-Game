using UnityEngine;
using MyGame.Localization;
public class MenuManager : MonoBehaviour {

    #region Public Methods
    public void SetEnglish()
    {
        Localize.SetCurrentLanguage(SystemLanguage.English);
        Debug.Log("Англ");
        LocalizeImage.SetCurrentLanguage();
    }
    public void SetRussian()
    {
        Localize.SetCurrentLanguage(SystemLanguage.Russian);
        Debug.Log("Рус");
        LocalizeImage.SetCurrentLanguage();
    }
    public void SetSpanish()
    {
        Localize.SetCurrentLanguage(SystemLanguage.Spanish);
        Debug.Log("Исп");
        LocalizeImage.SetCurrentLanguage();
    }
    #endregion Public Methods
}
