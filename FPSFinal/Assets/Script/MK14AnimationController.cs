using UnityEngine;

public class MK14AnimationController: MonoBehaviour
{
    public static MK14AnimationController instance;
    [Header("Animators")]
    public Animator handAnimator;  // �����ֲ�
    public Animator gunAnimator;   // ����ǹе

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip reloadSound;
    public AudioClip fireSound;

    [Header("Fire Settings")]
    public float fireRate = 0.2f; // ÿ0.2�뷢һǹ
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
        // ����
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            StartReload();
        }

        // ���𣺳������
        if (Input.GetMouseButton(0) && !isReloading && PlayerController.instance.activeGun.currentAmmo > 0)
        {
            TryFire();
        }


        if (PlayerController.instance.activeGun.currentAmmo <= 0 && !isReloading)
        {
            StartReload();
        }

        // ��ȴ��ʱ
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

        Invoke(nameof(ResetReload), 2.3f); // �뻻������ʱ��һ��
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
