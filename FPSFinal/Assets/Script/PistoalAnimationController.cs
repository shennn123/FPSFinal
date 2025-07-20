using UnityEngine;

public class PistolAnimatorController : MonoBehaviour
{
    public static PistolAnimatorController instance;
    [Header("Animators")]
    public Animator handAnimator;  // 控制手部
    public Animator gunAnimator;   // 控制枪械

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip reloadSound;
    public AudioClip fireSound;

    [Header("Fire Settings")]
    public float fireRate = 0.2f; // 每0.2秒发一枪
    private float fireCooldown = 0.3f;

    public bool isReloading = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // 换弹
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            StartReload();
        }

        // 单点射击：只在点击瞬间触发
        if (Input.GetMouseButtonDown(0) && !isReloading)
        {
            TryFire();
        }

        // 冷却计时
        if (fireCooldown > 0f)
        {
            fireCooldown -= Time.deltaTime;
        }
    }


    private void StartReload()
    {
        isReloading = true;

        if (handAnimator && gunAnimator)
        {
            handAnimator.SetTrigger("IsReloading");
            gunAnimator.SetTrigger("IsReloading");
        }

        if (reloadSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(reloadSound);
        }

        Invoke(nameof(ResetReload), 2.35f); // 与换弹动画时长一致
    }

    private void ResetReload()
    {
        isReloading = false;
    }

    private void TryFire()
    {
        if (fireCooldown <= 0f)
        {
            TriggerFire();
            fireCooldown = fireRate;
        }
    }

    private void TriggerFire()
    {
        if (handAnimator && gunAnimator)
        {
            handAnimator.SetTrigger("IsFire");
            gunAnimator.SetTrigger("IsFire");
        }

        if (fireSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(fireSound);
        }
    }
}
