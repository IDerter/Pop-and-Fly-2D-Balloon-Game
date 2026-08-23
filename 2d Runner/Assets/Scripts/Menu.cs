using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Гарантирует наличие AudioSource на объекте
[RequireComponent(typeof(AudioSource))]
public class Menu : MonoBehaviour
{
    [Header("Панели и Холсты")]
    [SerializeField] private GameObject mainMenuPanel;    // Родительский объект со всеми кнопками главного меню
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject exitConfirmPanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject localizationPanel;
    [SerializeField] private GameObject backgroundFon;

    [Header("Анимация и Персонаж")]
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private GameObject playerObject;

    [Header("Настройки Аудио")]
    [SerializeField] private GameObject musicSourceObject; // Объект, где висит фоновая музыка
    [SerializeField] private GameObject musicOnButton;      // Кнопка "Вкл музыку"
    [SerializeField] private GameObject musicOffButton;     // Кнопка "Выкл музыку"
    [SerializeField] private GameObject soundOnButton;      // Кнопка "Вкл звук"
    [SerializeField] private GameObject soundOffButton;     // Кнопка "Выкл звук"

    [Header("Имена Сцен")]
    [SerializeField] private string chooseLocationSceneName = "ChooseLocation";
    [SerializeField] private string mainLevelSceneName = "MainLvl";

    // Кэшированные компоненты
    private AudioSource clickAudioSource;

    // Состояния настроек
    private static bool isMusicEnabled = true;
    private static bool isSoundEnabled = true;

    private const string MusicPrefsKey = "checkmusic"; // 0 - On, 1 - Off

    private void Awake()
    {
        // Кэшируем компоненты при старте для производительности
        clickAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        LoadSettings();
        UpdateUI();
    }

    // --- ЛОГИКА ЗАГРУЗКИ И ПЕРЕКЛЮЧЕНИЯ ---

    public void StartFadeToLevel()
    {
        PlayClickSound();
        fadeAnimator.SetTrigger("fade");
    }

    // Этот метод должен вызываться событием из анимации Fade
    public void OnFadeComplete()
    {
        // Загружаем сцену по индексу (как в старом скрипте)
        SceneManager.LoadScene(2); 
    }

    public void PlayChooseLocation()
    {
        PlayClickSound();
        SceneManager.LoadScene(chooseLocationSceneName);
    }

    public void PlayMainLevel()
    {
        PlayClickSound();
        SceneManager.LoadScene(mainLevelSceneName);
    }

    // --- УПРАВЛЕНИЕ ПАНЕЛЯМИ ---

    public void OpenSettings()
    {
        PlayClickSound();
        SetMainElementsActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        PlayClickSound();
        settingsPanel.SetActive(false);
        SetMainElementsActive(true);
    }

    public void OpenExitConfirmation()
    {
        PlayClickSound();
        SetMainElementsActive(false);
        exitConfirmPanel.SetActive(true);
    }

    public void CloseExitConfirmation()
    {
        PlayClickSound();
        exitConfirmPanel.SetActive(false);
        SetMainElementsActive(true);
    }

    public void OpenShop()
    {
        PlayClickSound();
        shopPanel.SetActive(true);
        // В старом коде фон и кнопки выключались. Правильнее выключать только кнопки.
        mainMenuPanel.SetActive(false);
    }

    public void CloseShop()
    {
        PlayClickSound();
        // В старом скрипте ExitShop перезагружал сцену "Menu". 
        // Если это отдельная панель, лучше просто закрыть её.
        shopPanel.SetActive(false);
        mainMenuPanel.SetActive(true);

        // Если Shop - это действительно отдельная сцена:
        // SceneManager.LoadScene("Menu");
    }

    public void OpenLocalization()
    {
        PlayClickSound();
        localizationPanel.SetActive(true);
    }

    public void CloseLocalization()
    {
        PlayClickSound();
        localizationPanel.SetActive(false);
    }

    public void ConfirmQuitGame()
    {
        PlayClickSound();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    // --- НАСТРОЙКИ (АУДИО) ---

    public void ToggleMusic()
    {
        PlayClickSound();
        isMusicEnabled = !isMusicEnabled;
        
        // Сохраняем состояние (0 - On, 1 - Off для совместимости со старым кодом)
        PlayerPrefs.SetInt(MusicPrefsKey, isMusicEnabled ? 0 : 1);
        PlayerPrefs.Save();

        UpdateUI();
    }

    public void ToggleSoundMute()
    {
        PlayClickSound();
        isSoundEnabled = !isSoundEnabled;

        // Управление общей громкостью игры
        AudioListener.volume = isSoundEnabled ? 1f : 0f;

        UpdateUI();
    }

    // --- ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ ---

    private void PlayClickSound()
    {
        if (clickAudioSource != null && isSoundEnabled)
        {
            clickAudioSource.Play();
        }
    }

    // Управляет видимостью основных элементов меню (персонажа, кнопок Play/Settings/Exit)
    private void SetMainElementsActive(bool active)
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(active);
        if (playerObject != null) playerObject.SetActive(active);
        // Фон обычно должен оставаться включенным
    }

    private void LoadSettings()
    {
        // Загружаем музыку. По умолчанию (если ключа нет) - Включена (0).
        isMusicEnabled = PlayerPrefs.GetInt(MusicPrefsKey, 0) == 0;

        // Звук (общий AudioListener) по умолчанию Включен
        AudioListener.volume = isSoundEnabled ? 1f : 0f;
    }

    private void UpdateUI()
    {
        // Управление фоновой музыкой
        if (musicSourceObject != null)
        {
            musicSourceObject.SetActive(isMusicEnabled);
        }

        // Обновление кнопок в настройках
        if (musicOnButton != null) musicOnButton.SetActive(isMusicEnabled);
        if (musicOffButton != null) musicOffButton.SetActive(!isMusicEnabled);

        if (soundOnButton != null) soundOnButton.SetActive(isSoundEnabled);
        if (soundOffButton != null) soundOffButton.SetActive(!isSoundEnabled);
    }
}