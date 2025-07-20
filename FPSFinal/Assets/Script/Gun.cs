using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;

    public bool canAutoFire = false;
    public float firerate = 0.5f;
    public bool isReloading = false;

    public int maxAmmo = 60;
    public int currentAmmo = 30;
    public int ammoPerReload = 30; // 每次换弹补充的弹药数量

    public float reloadTime = 1.5f; // ÿ��ǹ�Լ��Ļ���ʱ��

    [HideInInspector]
    public float fireCounter;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            isReloading = true;
            StartCoroutine(ReloadGun());
        }

        if (fireCounter > 0)
        {
            fireCounter -= Time.deltaTime;
        }
    }

    public System.Collections.IEnumerator ReloadGun()
    {
        isReloading = true;
        Debug.Log($"Reloading {gameObject.name} for {reloadTime} seconds");

        yield return new WaitForSeconds(reloadTime);

        int bulletsNeeded = ammoPerReload - currentAmmo;
        int bulletsToReload = Mathf.Min(bulletsNeeded, maxAmmo); // 防止 maxAmmo 不够

        currentAmmo += bulletsToReload;
        maxAmmo -= bulletsToReload;

        UIController.instance.UpdateAmmoUI();
        isReloading = false;

        Debug.Log($"Reloaded {gameObject.name}. Ammo: {currentAmmo}/{maxAmmo}");
    }

}
