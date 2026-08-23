using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Destroyer : MonoBehaviour
{
    public float lifeTime = 6f; 
    public float fadeDuration = 1f; 
    
    // НОВАЯ ГАЛОЧКА: по умолчанию включена для пула (врагов)
    public bool isPooledObject = true; 

    private LineRenderer web;
    private SpriteRenderer sprite;
    private float defaultLineWidth;

    private void Awake()
    {
        web = GetComponentInChildren<LineRenderer>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        if (web != null) defaultLineWidth = web.startWidth;
    }

    private void OnEnable()
    {
        if (web != null) { web.startWidth = defaultLineWidth; web.endWidth = defaultLineWidth; }
        if (sprite != null) { sprite.DOKill(); sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f); }

        StartCoroutine(DestroyWithAnimation());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        if (sprite != null) sprite.DOKill();
        DOTween.Kill(this);
    }

    IEnumerator DestroyWithAnimation()
    {
        float waitTime = lifeTime - fadeDuration;
        if (waitTime > 0) yield return new WaitForSeconds(waitTime);

        if (web != null)
        {
            float currentWidth = web.startWidth;
            DOTween.To(() => currentWidth, x => { web.startWidth = x; web.endWidth = x; }, 0f, fadeDuration).SetId(this);
        }

        if (sprite != null) sprite.DOFade(0f, fadeDuration);

        yield return new WaitForSeconds(fadeDuration);

        // ВОТ ОНО: Разделяем логику!
        if (isPooledObject && gameObject.activeInHierarchy)
        {
            ObjectPoolManager.Instance.ReturnToPool(gameObject); // Враги идут в пул
        }
        else
        {
            Destroy(gameObject); // Папки Variant просто удаляются
        }
    }
}