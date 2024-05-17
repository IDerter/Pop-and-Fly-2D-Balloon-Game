using UnityEngine.UI;
using UnityEngine;
namespace MyGame.Localization
{
    [RequireComponent(typeof(Image))]
    public class LocalizeImage : MonoBehaviour
    {

        #region Public Fields
        public string localizationKey;
        #endregion Public Fields

        #region PrivateFields

        private const string STR_LOCALIZATION_PREFIX = "Localization/UI/";
        private Image image;
        #endregion PrivateFields

        #region Public Methods
        public static void SetCurrentLanguage()
        {
            LocalizeImage[] allTexts = GameObject.FindObjectsOfType<LocalizeImage>();
            for (int i = 0;i<allTexts.Length;i++)
            {
                allTexts[i].UpdateLocale();
               
            }
        }
        public void UpdateLocale()
        {
            if (!image) return;
            Invoke("_updateLocale", 0.1f);
        }
        #endregion Public Methods
        #region Private Methods
        private void _updateLocale()
        {
            if(Locale.CurrentLanguageHasBeenSet)
            {
                Sprite tmp = Resources.Load(STR_LOCALIZATION_PREFIX + Locale.PlayerLanguage.ToString() + "/" + localizationKey, typeof(Sprite)) as Sprite;
                if (tmp!=null)
                {
                    image.sprite = tmp;
                    Debug.Log("555");
                }
                return;
            }
            UpdateLocale();
        }
        private void Start()
        {
            image = GetComponent<Image>();
            UpdateLocale();
        }
        #endregion Private Methods
    }
}