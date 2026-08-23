using UnityEngine;
using DG.Tweening;
using YG;

public class PanelWindow : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RectTransform _windowRect;

    private Tween _autoCloseTween;

    public void OpenPanel()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);

        _canvasGroup.blocksRaycasts = true;

        _canvasGroup.alpha = 0f;
        _windowRect.localScale = Vector3.zero;

        _canvasGroup.DOKill();
        _windowRect.DOKill();

        _canvasGroup.DOFade(1f, 0.3f).SetUpdate(true);
        _windowRect.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack).SetUpdate(true);
    }

    public void OpenAndAutoClose(float duration = 2f)
    {
        OpenPanel();
        Time.timeScale = 1;

        // ���� ������ ������� ������ �� ����, ��� ������� 2 ������� - ������� ������ ������
        _autoCloseTween?.Kill();

        // ��������� ������, ������� ���� �������� ��� ����� (SetUpdate(true))
        _autoCloseTween = DOVirtual.DelayedCall(duration, () =>
        {
            // ���������, ������� �� ��� ������, ������ ��� � ���������
            if (gameObject.activeInHierarchy)
            {
                ClosePanel();
            }
            Time.timeScale = 1f;
        }).SetUpdate(true);
    }

    public void ClosePanel()
    {
        _canvasGroup.blocksRaycasts = false;

        // ���� ����� ������ ������ �������, �������� ������������
        _autoCloseTween?.Kill();

        _canvasGroup.DOKill();
        _windowRect.DOKill();

        // � ����� ���� ����������� .SetUpdate(true)
        _canvasGroup.DOFade(0f, 0.2f).SetUpdate(true);
        _windowRect.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack)
            .SetUpdate(true);

        Time.timeScale = 1f;
    }

    private void OnDestroy()
    {
        // ������� �������� ����: ������� ��� ����� ��� ����������� �������, 
        // ����� �������� ������ MissingReferenceException ��� ����� ����
        _autoCloseTween?.Kill();
        _canvasGroup.DOKill();
        _windowRect.DOKill();

        Time.timeScale = 1f;
    }
}