using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // Обязательно подключаем DOTween

public class Destroyer : MonoBehaviour
{
    public float lifeTime = 6f; // Общее время жизни
    public float fadeDuration = 1f; // За сколько секунд до смерти начинать анимацию

    void Start()
    {
        // Запускаем корутину вместо обычного Destroy
        StartCoroutine(DestroyWithAnimation());
    } 

    IEnumerator DestroyWithAnimation()
    {
        // 1. Ждем основное время жизни МИНУС время на анимацию
        float waitTime = lifeTime - fadeDuration;
        if (waitTime > 0)
        {
            yield return new WaitForSeconds(waitTime);
        }

        // ==========================================
        // 2. НАЧИНАЕТСЯ КРАСИВАЯ АНИМАЦИЯ ИСЧЕЗНОВЕНИЯ
        // ==========================================

        // АНИМАЦИЯ ПАУТИНЫ: Ищем LineRenderer
        LineRenderer web = GetComponentInChildren<LineRenderer>();
        if (web != null)
        {
            // Берем текущую ширину и плавно сужаем её до нуля за fadeDuration секунд.
            // Паутина будет выглядеть так, словно она истаяла в воздухе.
            float currentWidth = web.startWidth;
            DOTween.To(() => currentWidth, x => { web.startWidth = x; web.endWidth = x; }, 0f, fadeDuration);
        }

        // АНИМАЦИЯ САМОГО ВРАГА: Ищем SpriteRenderer
        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
        if (sprite != null)
        {
            // Плавно растворяем врага (уводим альфа-канал в 0)
            sprite.DOFade(0f, fadeDuration);
        }

        // 3. Ждем, пока анимация (fadeDuration) полностью проиграется
        yield return new WaitForSeconds(fadeDuration);

        // 4. Окончательно удаляем пустой и прозрачный объект
        Destroy(gameObject);
    }
}