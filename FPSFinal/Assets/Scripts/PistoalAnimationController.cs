using UnityEngine;

public class PistolAnimatorController : MonoBehaviour
{
    public static PistolAnimatorController instance;
    [Header("Animators")]
    public Animator handAnimator;  // �����ֲ�
    public Animator gunAnimator;   // ����ǹе

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip reloadSound;
    public AudioClip fireSound;

    [Header("Fire Settings")]
    public float fireRate = 0.2f; // ÿ0.2�뷢һǹ
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

        // 手动换弹
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && PlayerController.instance.activeGun.maxAmmo != 0)
        {
            StartReload();
            return; // 防止继续开火逻辑
        }

        // 左键按下尝试开火（或判断是否需要自动换弹）
        if (Input.GetMouseButtonDown(0) && !isReloading)
        {
            int currentAmmo = PlayerController.instance.activeGun.currentAmmo;

            if (currentAmmo > 0)
            {
                TryFire();
            }
            else
            {
                if (PlayerController.instance.activeGun.maxAmmo > 0)
                {
                    StartReload(); // 没子弹自动换弹
                }
                else
                {
                    Debug.Log("No ammo left and no reload available.");
                }
            }
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
            handAnimator.SetTrigger("IsReloading");
            gunAnimator.SetTrigger("IsReloading");
        }

        if (reloadSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(reloadSound);
        }

        Invoke(nameof(ResetReload), 2.35f); 
    }

    public void ResetReload()
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
