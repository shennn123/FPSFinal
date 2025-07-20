using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;

    public bool canAutoFire = false;
    public float firerate = 0.5f;
    public bool isReloading = false;

    public int maxAmmo = 30;
    public int currentAmmo = 30;

    public float reloadTime = 1.5f; // ÿ��ǹ�Լ��Ļ���ʱ��

    [HideInInspector]
    public float fireCounter;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < maxAmmo)
        {
            isReloading = true;
            StartCoroutine(ReloadGun());
        }

        if (fireCounter > 0)
        {
            fireCounter -= Time.deltaTime;
        }
    }

    private System.Collections.IEnumerator ReloadGun()
    {
        Debug.Log($"Reloading {gameObject.name} for {reloadTime} seconds");

        // �ɼӶ�������Ч
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log($"Reloaded {gameObject.name}. Ammo: {currentAmmo}/{maxAmmo}");
    }
}
