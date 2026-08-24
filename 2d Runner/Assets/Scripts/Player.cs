using System;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [Header("Components & Animation")]
    public Animator characterAnimator;
    public SpriteRenderer playerSprite;
    public SpriteRenderer sawSprite;
    public Rigidbody2D rb;
    public CircleCollider2D circleCollider;

    [Header("Stats & Physics")]
    public float speed = 15f;
    public int score = 0;
    public bool isInvulnerable = false;

    // События
    public event Action<int> OnScoreChanged; // Для GameHUD
    public event Action OnPlayerDied;        // Для GameManager
    public event Action OnFirstClick;        // Для GameManager

    [Header("Upgrades & Assets")]
    public AmNuamRunner.UpgradeAsset shieldUpgradeAsset;
    public AmNuamRunner.UpgradeAsset magnetUpgradeAsset;
    public float baseAbilityDuration = 5f; // Базовое время (5 сек)

    [Header("Ability UI")]
    public AbilityCooldown shieldUI; // Замените старый GameObject areol на это
    public AbilityCooldown magnetUI; // Замените старый GameObject magnitObject на это

    [Header("In-Game Visuals (На персонаже)")]
    // Сюда перетащите ареол и объект MagnitAnim (со скриншота)
    public GameObject areol; 
    public GameObject magnitObject; 
    public PointEffector2D pointEffector;

    private Vector3 defaultScale;
    private bool isDead = false;
    private bool isStarted = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        if (playerSprite != null) defaultScale = playerSprite.transform.localScale;
    }

    private void Start()
    {
        if (characterAnimator != null) characterAnimator.enabled = false;
    }

    private void FixedUpdate()
    {
        if (!isDead && isStarted) rb.velocity = new Vector2(rb.velocity.x, 0);
    }

    private void Update()
    {
        if (isDead) return;

        // Управление для ПК (Стрелочки или A/D)
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ButtonDown(); // Вызываем тот же метод, что и левая кнопка на экране
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            ButtonUp(); // Вызываем тот же метод, что и правая кнопка на экране
        }
    }

    public void ButtonUp() { MovePlayer(1, -15f); }
    public void ButtonDown() { MovePlayer(-1, 15f); }

    private void MovePlayer(int direction, float tiltAngle)
    {
        if (isDead) return;

        if (!isStarted)
        {
            isStarted = true;
            if (characterAnimator != null) characterAnimator.enabled = true;
            OnFirstClick?.Invoke(); 
            Debug.Log("OnFirstClick");
        }

        rb.velocity = new Vector2(speed * direction, rb.velocity.y);

        if (playerSprite != null)
        {
            DOTween.Kill("RotateTween");
            playerSprite.transform.DORotate(new Vector3(0, 0, tiltAngle), 0.35f)
                .SetEase(Ease.OutBack).SetId("RotateTween");
        }
    }

    // Вызывайте этот метод при подборе очков (монеток/капель)
    public void AddScore(int amount)
    {
        score += amount;
        OnScoreChanged?.Invoke(score); // GameHUD сам обновит текст!
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isDead) return;

        if (col.CompareTag("Invulnerable")) ActivateShield(col.gameObject);
        else if (col.CompareTag("Magnit")) ActivateMagnet(col.gameObject);
        else if (col.CompareTag("Ship")) TakeDamage();
    }

    private void ActivateShield(GameObject pickup)
    {
        PlayEatJuice();
        isInvulnerable = true;
        ObjectPoolManager.Instance.ReturnToPool(pickup);

        // 1. Считаем итоговое время: База + (Уровень прокачки * Шаг)
        int level = AmNuamRunner.Upgrades.GetUpgradeLevel(shieldUpgradeAsset);
        float totalDuration = baseAbilityDuration + (level * shieldUpgradeAsset.step);

        // 2. Запускаем кружок таймера в интерфейсе (UI сам включится и выключится)
        if (shieldUI != null) shieldUI.StartCooldown(totalDuration);
        
        // 3. Включаем визуал на самом персонаже (Ареол)
        if (areol != null) areol.SetActive(true);

        // 4. Планируем отключение
        CancelInvoke(nameof(DeactivateShield));
        Invoke(nameof(DeactivateShield), totalDuration);
    }

    private void ActivateMagnet(GameObject pickup)
    {
        PlayEatJuice();
        if (pointEffector != null) pointEffector.enabled = true;
        ObjectPoolManager.Instance.ReturnToPool(pickup);

        // 1. Считаем итоговое время для магнита
        int level = AmNuamRunner.Upgrades.GetUpgradeLevel(magnetUpgradeAsset);
        float totalDuration = baseAbilityDuration + (level * magnetUpgradeAsset.step);

        // 2. Запускаем кружок таймера в интерфейсе
        if (magnetUI != null) magnetUI.StartCooldown(totalDuration);
        
        // 3. Включаем визуал на самом персонаже (объект MagnitAnim со скриншота)
        // Аниматор запустится автоматически при включении объекта!
        if (magnitObject != null) magnitObject.SetActive(true);

        // 4. Планируем отключение
        CancelInvoke(nameof(DeactivateMagnet));
        Invoke(nameof(DeactivateMagnet), totalDuration);
    }

    public void TakeDamage()
    {
        if (isInvulnerable) return;
        Die();
    }

    private void Die()
    {
        isDead = true;
        isInvulnerable = false;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;

        // ФИКС: Отключаем коллайдер, чтобы мертвый игрок не мог ничего подбирать
        if (circleCollider != null) circleCollider.enabled = false;

        if (sawSprite != null) sawSprite.enabled = false;
        if (characterAnimator != null) characterAnimator.SetTrigger("Die");

        DeactivateShield();
        DeactivateMagnet();

        OnPlayerDied?.Invoke(); 
    }

    public void Revive()
    {
        isDead = false;
        isInvulnerable = true;
        
        // ФИКС: Включаем коллайдер обратно при возрождении
        if (circleCollider != null) circleCollider.enabled = true;

        if (areol) areol.SetActive(true);

        Invoke(nameof(DeactivateShield), 3f);

        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (playerSprite != null) playerSprite.enabled = true;
        if (sawSprite != null) sawSprite.enabled = true;

        if (characterAnimator != null)
        {
            characterAnimator.Rebind();
            characterAnimator.enabled = true;
        }
    }

    private void DeactivateShield()
    {
        isInvulnerable = false;
        if (areol) areol.SetActive(false);
    }

    private void DeactivateMagnet()
    {
        if (pointEffector) pointEffector.enabled = false;
        if (magnitObject) magnitObject.SetActive(false);
    }

    public void PlayEatJuice()
    {
        if (characterAnimator != null) characterAnimator.SetTrigger("Eat");
        if (playerSprite != null)
        {
            DOTween.Kill("ScaleTween");
            playerSprite.transform.localScale = defaultScale;
            
            // ИСПРАВЛЕНИЕ: Уменьшили значения с 0.2 до 0.05 (эффект будет легким и пружинистым)
            playerSprite.transform.DOPunchScale(new Vector3(0.05f, -0.05f, 0f), 0.3f, 1, 0.5f).SetId("ScaleTween");
        }
    }
}