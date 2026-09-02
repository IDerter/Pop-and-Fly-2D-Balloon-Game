using UnityEngine;

namespace AmNyamRunner
{
    [RequireComponent(typeof(Camera))]
    public class CameraAspectCrop : MonoBehaviour
    {
        [Header("Идеальное разрешение уровня")]
        [SerializeField] private Vector2 _targetResolution = new Vector2(1024, 576);
        
        // Переменную _targetOrthographicSize можно убрать, 
        // так как зум мы больше трогать не будем, он останется равен 5.

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
            AdjustCameraRect();
        }

        private void Update()
        {
            // Проверяем, изменился ли размер окна
            if (Screen.width != _lastScreenWidth || Screen.height != _lastScreenHeight)
            {
                AdjustCameraRect();
            }
        }

        private void AdjustCameraRect()
        {
            _lastScreenWidth = Screen.width;
            _lastScreenHeight = Screen.height;

            float currentScreenAspect = _lastScreenWidth / _lastScreenHeight;
            float scaleHeight = currentScreenAspect / _targetAspect;

            // Если экран у́же, чем нужно (добавляем полосы сверху и снизу)
            if (scaleHeight < 1.0f)
            {
                Rect rect = _camera.rect;
                rect.width = 1.0f;
                rect.height = scaleHeight;
                rect.x = 0;
                rect.y = (1.0f - scaleHeight) / 2.0f;
                _camera.rect = rect;
            }
            // Если экран шире, чем нужно (добавляем полосы по бокам)
            else
            {
                float scaleWidth = 1.0f / scaleHeight;
                Rect rect = _camera.rect;
                rect.width = scaleWidth;
                rect.height = 1.0f;
                rect.x = (1.0f - scaleWidth) / 2.0f;
                rect.y = 0;
                _camera.rect = rect;
            }
        }
    }
}