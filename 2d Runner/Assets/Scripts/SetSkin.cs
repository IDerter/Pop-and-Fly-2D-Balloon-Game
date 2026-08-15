using UnityEngine;

public class SetSkin : MonoBehaviour {
    public int index;
    
    // Вместо спрайтов теперь массив контроллеров анимаций для каждого скина
    [SerializeField] private RuntimeAnimatorController[] skinControllers; 
    
    // Ссылка на аниматор (вешаем его на тот же объект, где раньше был SpriteRenderer)
    private Animator animator; 

    private void Awake()
    {
        index = PlayerPrefs.GetInt("index");
        // Предполагаем, что Animator висит на первом дочернем объекте, как и раньше
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Start()
    {
        UpdateSkin(index);
    }

    public void UpdateSkin(int index)
    {
        // Меняем логику контроллера, оставляя стейт-машину нетронутой
        if (skinControllers.Length > index && skinControllers[index] != null)
        {
            animator.runtimeAnimatorController = skinControllers[index];
        }
    }
}