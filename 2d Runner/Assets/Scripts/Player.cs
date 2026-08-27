using System;
using UnityEngine;
using DG.Tweening;
using YG; // Не забываем Яндекс

public class Player : MonoBehaviour
{
    [Header("Components & Animation")]
    public Animator characterAnimator;
    public SpriteRenderer playerSprite;
    public SpriteRenderer sawSprite;
    public Rigidbody2D rb;
    public CircleCollider2D circleCollider;

    [Header("Skins (Контроллеры)")]
    public RuntimeAnimatorController redDragonController;
    public RuntimeAnimatorController blueDragonController;
    // public RuntimeAnimatorController lickController; // Для Лямзи в будущем

    [Header("Skins (Стартовые спрайты)")]
    public Sprite redDragonIdleSprite;
    public Sprite blueDragonIdleSprite;

    [Header("Stats & Physics")]
    public float speed = 15f;
    public int score = 0;
    public bool isInvulnerable = false;

    // События
    public event Action<int> OnScoreChanged; 
    public event Action OnPlayerDied;        
    public event Action OnFirstClick;        

    [Header("Upgrades & Assets")]
    public AmNuamRunner.UpgradeAsset shieldUpgradeAsset;
    public AmNuamRunner.UpgradeAsset magnetUpgradeAsset;
    public float baseAbilityDuration = 5f; 

    [Header("Ability UI")]
    public AbilityCooldown shieldUI; 
    public AbilityCooldown magnetUI; 

    [Header("In-Game Visuals (На персонаже)")]
    public GameObject areol; 
    public GameObject magnitObject; 
    public PointEffector2D pointEffector;

    private Vector3 defaultScale;
    private bool isDead = false;
    private bool isStarted = false;
    
    // Переменная для кэширования текущего скина
    private string currentSkin; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        if (playerSprite != null) defaultScale = playerSprite.transform.localScale;

        ApplySkin(); // Применяем скин сразу при загрузке
    }

    private void Start()
    {
        if (characterAnimator != null) characterAnimator.enabled = false;
    }

    // --- ЛОГИКА СКИНОВ ---
    private void ApplySkin()
    {
        currentSkin = string.IsNullOrEmpty(YG2.saves.currentSkin) ? "RedDragon" : YG2.saves.currentSkin;

        if (currentSkin == "BlueDragon")
        {
            characterAnimator.runtimeAnimatorController = blueDragonController;
            
            if (playerSprite != null)
            {
                // Сразу ставим нужную картинку до включения аниматора
                playerSprite.sprite = blueDragonIdleSprite; 
                
                playerSprite.transform.localScale = new Vector3(2.93f, 2.93f, 1f);
                defaultScale = playerSprite.transform.localScale; 
            }
        }
        else if (currentSkin == "Lick")
        {
            // characterAnimator.runtimeAnimatorController = lickController;
        }
        else 
        {
            characterAnimator.runtimeAnimatorController = redDragonController;
            
            if (playerSprite != null)
            {
                // Сразу ставим нужную картинку до включения аниматора
                playerSprite.sprite = redDragonIdleSprite;

                playerSprite.transform.localScale = new Vector3(2.93f, 2.93f, 1f);
                defaultScale = playerSprite.transform.localScale; 
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isDead && isStarted) rb.velocity = new Vector2(rb.velocity.x, 0);
    }

    private void Update()
    {
        if (isDead) return;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ButtonDown(); 
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            ButtonUp(); 
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

            if (!YG2.saves.isTutorialCompleted)
            {
                AnalyticsManager.Instance.SaveLearningStep("first_move");
            }

            // --- ПАССИВКА БУКИ ПРИ СТАРТЕ ---
            if (currentSkin == "BlueDragon")
            {
                ActivateShield(null); // Даем щит без подбора объекта
            }
        }
        Sound.PlayerSound.Play();

        rb.velocity = new Vector2(speed * direction, rb.velocity.y);

        if (playerSprite != null)
        {
            DOTween.Kill("RotateTween");
            playerSprite.transform.DORotate(new Vector3(0, 0, tiltAngle), 0.35f)
                .SetEase(Ease.OutBack).SetId("RotateTween");
        }
    }

    public void AddScore(int amount)
    {
        if (currentSkin == "BlueDragon")
        {
            amount *= 2; 
        }

        score += amount;
        OnScoreChanged?.Invoke(score); 
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isDead) return;

        if (col.CompareTag("Invulnerable")) ActivateShield(col.gameObject);
        else if (col.CompareTag("Magnit")) ActivateMagnet(col.gameObject);
        else if (col.CompareTag("Ship")) TakeDamage();
    }

    // ИЗМЕНЕНИЕ: Сделали pickup необязательным параметром (null), чтобы можно было вызывать пассивку
    private void ActivateShield(GameObject pickup)
    {
        isInvulnerable = true;

        if (pickup != null)
        {
            PlayEatJuice();
            ObjectPoolManager.Instance.ReturnToPool(pickup);
        }

        int level = AmNuamRunner.Upgrades.GetUpgradeLevel(shieldUpgradeAsset);
        float totalDuration = baseAbilityDuration + (level * shieldUpgradeAsset.step);

        if (currentSkin == "BlueDragon")
        {
            totalDuration += 3f; // Даем Буке бонусные 3 секунды щита
        }

        if (shieldUI != null) shieldUI.StartCooldown(totalDuration);
        if (areol != null) areol.SetActive(true);

        CancelInvoke(nameof(DeactivateShield));
        Invoke(nameof(DeactivateShield), totalDuration);
    }

    private void ActivateMagnet(GameObject pickup)
    {
        PlayEatJuice();
        if (pointEffector != null) pointEffector.enabled = true;
        ObjectPoolManager.Instance.ReturnToPool(pickup);

        int level = AmNuamRunner.Upgrades.GetUpgradeLevel(magnetUpgradeAsset);
        float totalDuration = baseAbilityDuration + (level * magnetUpgradeAsset.step);

        if (magnetUI != null) magnetUI.StartCooldown(totalDuration);
        if (magnitObject != null) magnitObject.SetActive(true);

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
            playerSprite.transform.DOPunchScale(new Vector3(0.05f, -0.05f, 0f), 0.3f, 1, 0.5f).SetId("ScaleTween");
        }
    }
}