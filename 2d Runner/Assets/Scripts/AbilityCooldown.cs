using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldown : MonoBehaviour 
{
    public Image fillImage; // Сюда перетащите ваш кружок (shieldUI)
    
    private float _currentCooldown;
    private float _maxCooldown;
    private bool _isCoolingDown = false;

    // Вызываем этот метод из Player.cs, передавая итоговое время
    public void StartCooldown(float duration) 
    {
        _maxCooldown = duration;
        _currentCooldown = duration;
        fillImage.fillAmount = 1f;
        _isCoolingDown = true;
        gameObject.SetActive(true);
    }
    
    private void Update() 
    {
        if (_isCoolingDown)
        {
            _currentCooldown -= Time.deltaTime;
            fillImage.fillAmount = _currentCooldown / _maxCooldown;

            if (_currentCooldown <= 0)
            {
                _isCoolingDown = false;
                gameObject.SetActive(false); // Таймер сам выключает свой объект
            }
        }
    }
}