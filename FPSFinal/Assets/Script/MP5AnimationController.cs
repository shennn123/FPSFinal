using UnityEngine;

public class MP5AnimationController : MonoBehaviour
{
    public static MP5AnimationController instance;
    [Header("Animators")]
    public Animator handAnimator;  // 控制手部
    public Animator gunAnimator;   // 控制枪械

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip reloadSound;
    public AudioClip fireSound;

    [Header("Fire Settings")]
    public float fireRate = 0.2f; // 每0.2秒发一枪
    private float fireCooldown = 0f;

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

        // 开火：长按左键
        if (Input.GetMouseButton(0) && !isReloading)
        {
            TryFire();
        }

        // 松开鼠标左键时停止射击动画
        if (Input.GetMouseButtonUp(0))
        {
            StopFire();
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
            handAnimator.SetTrigger("Reload1");
            gunAnimator.SetTrigger("Reload1");
        }

        if (reloadSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(reloadSound);
        }

        Invoke(nameof(ResetReload), 2.3f); // 与换弹动画时长一致
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
        {   handAnimator.SetBool("IsFiring1", true);
            gunAnimator.SetBool("IsFiring1", true);
        }

        if (fireSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(fireSound);
        }
    }

    private void StopFire()
    {
        if (handAnimator && gunAnimator)
        {
            handAnimator.SetBool("IsFiring1", false);
            gunAnimator.SetBool("IsFiring1", false);
        }
    }
}
