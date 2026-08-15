using UnityEngine;

namespace AmNyamRunner
{
    [RequireComponent(typeof(Camera))]
    public class CameraAspectCrop : MonoBehaviour
    {
        // Указываем вертикальное разрешение (например, Full HD Portrait)
        [SerializeField] private Vector2 _targetResolution = new Vector2(1024, 576);
        [SerializeField] private float _targetOrthographicSize = 5f; // Подбери размер под свою сцену

        private Camera _camera;
        private float _targetAspect;
        private float _lastScreenWidth;
        private float _lastScreenHeight;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _targetAspect = _targetResolution.x / _targetResolution.y;
        }

        private void Start()
        {
            AdjustCameraSize();
        }

        private void Update()
        {
            // Проверяем изменение разрешения экрана (поворот или изменение окна в редакторе)
            if (Screen.width != _lastScreenWidth || Screen.height != _lastScreenHeight)
            {
                AdjustCameraSize();
            }
        }

        private void AdjustCameraSize()
        {
            _lastScreenWidth = Screen.width;
            _lastScreenHeight = Screen.height;

            float currentScreenAspect = (float)_lastScreenWidth / _lastScreenHeight;

            if (currentScreenAspect > _targetAspect)
            {
                _camera.orthographicSize = _targetOrthographicSize * (_targetAspect / currentScreenAspect);
            }
            else
            {
                _camera.orthographicSize = _targetOrthographicSize;
            }

            // Принудительно обновляем UI, чтобы элементы Canvas не дергались
            Canvas.ForceUpdateCanvases();
        }
    }
}