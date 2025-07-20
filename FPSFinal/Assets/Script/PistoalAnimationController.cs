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
        // ����
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            StartReload();
        }

        // ���������ֻ�ڵ��˲�䴥��
        if (Input.GetMouseButtonDown(0) && !isReloading)
        {
            TryFire();
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
            handAnimator.SetTrigger("IsReloading");
            gunAnimator.SetTrigger("IsReloading");
        }

        if (reloadSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(reloadSound);
        }

        Invoke(nameof(ResetReload), 2.35f); // �뻻������ʱ��һ��
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
