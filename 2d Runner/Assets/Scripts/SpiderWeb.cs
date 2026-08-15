using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
public class SpiderWeb : MonoBehaviour
{
    private LineRenderer line;
    
    [Header("Настройки паутины")]
    // Смещение начала паутины (чтобы она росла из спинки паука, а не из центра)
    public float yOffset = 0.5f; 
    
    // Насколько высоко уходит нить (за пределы экрана)
    public float webLength = 20f; 

    void Start()
    {
        line = GetComponent<LineRenderer>();

        // Линия не должна использовать мировые координаты для первой точки,
        // но нам проще обновлять обе точки вручную
        line.useWorldSpace = true; 
    }

    // Используем LateUpdate, чтобы линия обновлялась строго после того, как отработала физика и DOTween
    void LateUpdate()
    {
        // Жестко ставим Z = 0f на обеих точках!
        Vector3 startPoint = new Vector3(transform.position.x, transform.position.y + yOffset, 0f);
        Vector3 endPoint = new Vector3(transform.position.x, transform.position.y + webLength, 0f);

        line.SetPosition(0, startPoint);
        line.SetPosition(1, endPoint);
    }
}