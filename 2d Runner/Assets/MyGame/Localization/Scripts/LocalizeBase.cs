﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Localization
{
    public abstract class LocalizeBase : MonoBehaviour
    {
        #region Public Fields
        public string localizationKey;

        #endregion Public Fields

        #region Public Properties

        #endregion Public Properties

        #region Private Properties

        #endregion Private Properties

        #region Public Methods

        public abstract void UpdateLocale();

        protected virtual void Start()
        {
            if (!Locale.currentLanguageHasBeenSet)
            {
                Locale.currentLanguageHasBeenSet = true;
                SetCurrentLanguage(Locale.PlayerLanguage);
            }
            UpdateLocale();
        }
        public static string GetLocalizedString(string key)
        {
            if (Locale.CurrentLanguageStrings.ContainsKey(key))
                return Locale.CurrentLanguageStrings[key];
            else
                return string.Empty; //возвращать будет нечего
        }

        public static void SetCurrentLanguage(SystemLanguage language)
        {
            Locale.CurrentLanguage = language.ToString();
            Locale.PlayerLanguage = language;
            Localize[] allTexts = GameObject.FindObjectsOfType<Localize>();
            //LocalizeTM[] allTextsTM = GameObject.FindObjectsOfType<LocalizeTM>();

            for(int i = 0;i < allTexts.Length; i++)
            {
                allTexts[i].UpdateLocale();
            }
           // for (int i = 0; i < allTextsTM.Length; i++)
           // {
              //  allTextsTM[i].UpdateLocal();
           // }
        }
        #endregion Public Methods
    }
}