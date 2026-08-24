using UnityEngine;
using YG;

public class MenuAndTutorialUi : MonoBehaviour
{
    [Header("Start Screen")]
    public GameObject panel1;
    public GameObject panel2;
    public GameObject leftFinger;
    public GameObject rightFinger;
    
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
        // Если SDK уже загружен, настраиваем меню сразу
        if (YG2.isSDKEnabled)
        {
            SetupTutorialState();
        }
    }

    private void SetupTutorialState()
    {
        // ПРАВИЛЬНАЯ РАБОТА С YG2
        bool isFirstGame = !YG2.saves.isTutorialCompleted;

        panel1.SetActive(true);
        panel2.SetActive(true);

        // Если это не первая игра - отключаем гайды
        if (!isFirstGame)
        {
            leftFinger.SetActive(false);
            rightFinger.SetActive(false);
        }
    }

    public void HideStartMenu()
    {
        panel1.SetActive(false);
        panel2.SetActive(false);
        leftFinger.SetActive(false);
        rightFinger.SetActive(false);
    }
}