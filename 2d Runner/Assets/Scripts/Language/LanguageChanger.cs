using System;
using System.Collections;
using UnityEngine;
using YG;

namespace AmNuamRunner
{
    public class LanguageChanger : SingletonBase<LanguageChanger>
    {
        public event Action OnChangeLanguage;
        [SerializeField] private Language[] _buttonsLanguage;

        [SerializeField] private int _index = 0;
        public int GetLanguageIndex => _index;

        [SerializeField] private Sound _clickSound = Sound.Click; 
        
        private static bool _isSessionInitialized = false;

        private void Start()
        {
            StartCoroutine(InitializeLanguageCoroutine());
        }

        private IEnumerator InitializeLanguageCoroutine()
        {
            Debug.Log("[LanguageChanger] Waiting for Yandex SDK initialization...");

            float timeout = 10f;
            float elapsed = 0f;

            while (!IsYandexReady() && elapsed < timeout)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }

            InitializeLanguage();
        }

        private bool IsYandexReady()
        {
            try
            {
                return !string.IsNullOrEmpty(YG2.platform) || !string.IsNullOrEmpty(YG2.lang);
            }
            catch
            {
                return false;
            }
        }

        private void InitializeLanguage()
        {
            int newIndex = _index;

            try
            {
                if (YG2.platform == "YandexGames")
                {
                    var yandexLanguage = YG2.lang;
                    int yandexIndex = 0; 

                    if (!string.IsNullOrEmpty(yandexLanguage))
                    {
                        if (yandexLanguage == "ru" || yandexLanguage == "be" || yandexLanguage == "kk" || yandexLanguage == "uk" || yandexLanguage == "uz")
                            yandexIndex = 1;
                    }

                    if (!_isSessionInitialized)
                    {
                        newIndex = yandexIndex;
                        _isSessionInitialized = true;
                        PlayerPrefs.SetInt("IndexLanguageSave", newIndex);
                        PlayerPrefs.Save();
                    }
                    else
                    {
                        newIndex = PlayerPrefs.GetInt("IndexLanguageSave", yandexIndex);
                    }
                }
                else
                {
                    newIndex = PlayerPrefs.GetInt("IndexLanguageSave", 0);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[LanguageChanger] Error accessing YG2: {e.Message}");
                newIndex = PlayerPrefs.GetInt("IndexLanguageSave", 0);
            }

            if (LocaleSelector.Instance != null)
            {
                StartCoroutine(ApplyLanguageWhenReady(newIndex));
            }
        }

        private IEnumerator ApplyLanguageWhenReady(int newIndex)
        {
            float timeout = 5f;
            float startTime = Time.time;

            while (!LocaleSelector.Instance.IsLocalizationReady() && Time.time - startTime < timeout)
            {
                yield return new WaitForSeconds(0.1f);
            }

            _index = newIndex;
            ApplyLanguage();
        }

        private void ApplyLanguage()
        {
            foreach (var button in _buttonsLanguage)
            {
                if (button != null) button.gameObject.SetActive(false);
            }

            if (_index >= 0 && _index < _buttonsLanguage.Length && _buttonsLanguage[_index] != null)
            {
                _buttonsLanguage[_index].gameObject.SetActive(true);
            }

            if (LocaleSelector.Instance != null)
            {
                LocaleSelector.Instance.ChangeLocale(_index);
            }
        }

        public void ChooseNextLanguage()
        {
            ToggleLanguage(1);
        }

        public void ChoosePreviousLanguage()
        {
            ToggleLanguage(-1);
        }

        // ��������������� �����, ����� �� ����������� ���
        private void ToggleLanguage(int direction)
        {
            if (_buttonsLanguage[_index] != null)
                _buttonsLanguage[_index].gameObject.SetActive(false);

            _index += direction;

            if (_index >= _buttonsLanguage.Length) _index = 0;
            else if (_index < 0) _index = _buttonsLanguage.Length - 1;

            PlayerPrefs.SetInt("IndexLanguageSave", _index);
            PlayerPrefs.Save();

            if (_buttonsLanguage[_index] != null)
                _buttonsLanguage[_index].gameObject.SetActive(true);

            if (LocaleSelector.Instance != null)
                LocaleSelector.Instance.ChangeLocale(_index);

        }
    }
}