using UnityEngine;

public class MK14AnimationController: MonoBehaviour
{
    public static MK14AnimationController instance;
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
        if (Input.GetMouseButton(0) && !isReloading && PlayerController.instance.activeGun.currentAmmo > 0)
        {
            TryFire();
        }


        if (PlayerController.instance.activeGun.currentAmmo <= 0 && !isReloading)
        {
            StartReload();
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
            handAnimator.SetTrigger("IsReload1");
            gunAnimator.SetTrigger("IsReload1");
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
        {
            handAnimator.SetTrigger("IsFiring");
            gunAnimator.SetTrigger("IsFiring");
        }

        if (fireSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(fireSound);
        }
    }

}
