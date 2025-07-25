using UnityEngine;

public class AKAnimationController : MonoBehaviour
{
    public static AKAnimationController instance;

    [Header("Animators")]
    public Animator handAnimator;  // 手部动画
    public Animator gunAnimator;   // 枪械动画

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip reloadSound;
    public AudioClip fireSound;

    [Header("Fire Settings")]
    public float fireRate = 0.2f; // 每0.2秒开一枪
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
        // 手动换弹
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && PlayerController.instance.activeGun.maxAmmo != 0)
        {
            StartReload();
            return; // 防止继续开火逻辑
        }

        // 左键按下尝试开火（或判断是否需要自动换弹）
        if (Input.GetMouseButton(0) && !isReloading)
        {
            int currentAmmo = PlayerController.instance.activeGun.currentAmmo;

            if (currentAmmo > 0)
            {
                TryFire();
            }
            else if(PlayerController.instance.activeGun.maxAmmo > 0)
            {
                StartReload(); // 没子弹自动换弹
                StopFire();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            StopFire(); // 松开鼠标左键停止开火动画
        }

        // 冷却时间更新
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
            handAnimator.SetTrigger("Reload");
            gunAnimator.SetTrigger("Reload");
        }

        if (reloadSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(reloadSound);
        }

        Invoke(nameof(ResetReload), 2.3f); // 替换成实际的换弹动画时长
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
            handAnimator.SetBool("IsFiring", true);
            gunAnimator.SetBool("IsFiring", true);
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
            handAnimator.SetBool("IsFiring", false);
            gunAnimator.SetBool("IsFiring", false);
        }
    }
}
