using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MyGame.Localization
{
    [RequireComponent(typeof(Text))]

    public class Localize : LocalizeBase
    {//создаем класс Localize
        #region Private Fields
        private Text text;
        #endregion

        #region public Methods

        public override void UpdateLocale()
        {
            if (!text) return;
            if (!System.String.IsNullOrEmpty(localizationKey) && Locale.CurrentLanguageStrings.ContainsKey(localizationKey)) //если мы возращаем текст с не пустым ключом и он содержит текст
                text.text = Locale.CurrentLanguageStrings[localizationKey].Replace(@"\n", "" + '\n');
            
        }

        #endregion public Methods
        #region Private Methods
        protected override void Start()
        {
            text = GetComponent<Text>();//считываем компонент текста
            base.Start();
        }


        #endregion Private Methods
    }
}


