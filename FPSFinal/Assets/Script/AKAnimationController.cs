using UnityEngine;

public class AKAnimationController: MonoBehaviour
{
    public static AKAnimationController instance;
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
        if (Input.GetMouseButton(0) && !isReloading)
        {
            TryFire();
        }

        // �ɿ�������ʱֹͣ�������
        if (Input.GetMouseButtonUp(0))
        {
            StopFire();
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
            handAnimator.SetTrigger("Reload");
            gunAnimator.SetTrigger("Reload");
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
