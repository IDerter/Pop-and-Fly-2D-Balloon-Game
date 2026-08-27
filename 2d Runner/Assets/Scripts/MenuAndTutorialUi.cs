using UnityEngine;
using YG;
using DG.Tweening; // Если захочешь добавить анимацию покачивания

public class MenuAndTutorialUi : MonoBehaviour
{
    [Header("Start Screen Panels")]
    public GameObject panel1;
    public GameObject panel2;
    
    [Header("Tutorial: Rules (Схемы)")]
    public GameObject mechanicsHints; // <-- СЮДА перетащи панель с картинками правил (собирай/уклоняйся)
    
    [Header("Tutorial: Mobile Controls")]
    public GameObject leftFinger;
    public GameObject rightFinger;
    
    [Header("Tutorial: PC Controls")]
    public GameObject pcKeysLeft;
    public GameObject pcKeysRight;

    [Header("In-Game Controls")]
    public GameObject buttonLeft;
    public GameObject buttonRight;

    private void OnEnable()
    {
        YG2.onGetSDKData += SetupTutorialState;
    }

    private void OnDisable()
    {
        YG2.onGetSDKData -= SetupTutorialState;
    }

    private void Start()
    {
        if (YG2.isSDKEnabled)
        {
            SetupTutorialState();
        }
    }

    private void SetupTutorialState()
    {
        bool isFirstGame = !YG2.saves.isTutorialCompleted;

        panel1.SetActive(true);
        panel2.SetActive(true);

        if (isFirstGame)
        {
            // Показываем картинки с правилами игры
            if (mechanicsHints) mechanicsHints.SetActive(true);

            // Анимация дыхания (покачивания) для подсказок (опционально, если есть DOTween)
            if (mechanicsHints) 
            {
                mechanicsHints.transform.DOKill(); // убиваем старую анимацию на всякий случай
                mechanicsHints.transform.DOScale(1.05f, 0.8f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
            }

            // Управление
            if (YG2.envir.isDesktop)
            {
                // ПК
                if (pcKeysLeft) pcKeysLeft.SetActive(true);
                if (pcKeysRight) pcKeysRight.SetActive(true);
                
                if (leftFinger) leftFinger.SetActive(false);
                if (rightFinger) rightFinger.SetActive(false);
            }
            else
            {
                // Мобайл
                if (leftFinger) leftFinger.SetActive(true);
                if (rightFinger) rightFinger.SetActive(true);
                
                if (pcKeysLeft) pcKeysLeft.SetActive(false);
                if (pcKeysRight) pcKeysRight.SetActive(false);
            }
        }
        else
        {
            // Обучение пройдено: прячем всё
            if (mechanicsHints) mechanicsHints.SetActive(false);
            
            if (leftFinger) leftFinger.SetActive(false);
            if (rightFinger) rightFinger.SetActive(false);
            if (pcKeysLeft) pcKeysLeft.SetActive(false);
            if (pcKeysRight) pcKeysRight.SetActive(false);
        }
    }

    public void HideStartMenu()
    {
        panel1.SetActive(false);
        panel2.SetActive(false);
        
        // Убиваем анимацию и прячем картинки правил
        if (mechanicsHints) 
        {
            mechanicsHints.transform.DOKill();
            mechanicsHints.SetActive(false);
        }
        
        if (leftFinger) leftFinger.SetActive(false);
        if (rightFinger) rightFinger.SetActive(false);
        if (pcKeysLeft) pcKeysLeft.SetActive(false);
        if (pcKeysRight) pcKeysRight.SetActive(false);
    }

    [ContextMenu("Сбросить обучение (Тест)")]
    public void ResetTutorialTest()
    {
        YG2.saves.isTutorialCompleted = false;
        YG2.saves.coin = 0; 
        YG2.SaveProgress();
        
        SetupTutorialState(); 
        Debug.Log("<color=yellow>[Тест] Обучение и монеты сброшены!</color>");
    }
}