using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;

    public bool canAutoFire = false;
    public float firerate = 0.5f;
    public bool isReloading = false;

    public int maxAmmo = 30;
    public int currentAmmo = 30;

    public float reloadTime = 1.5f; // 每把枪自己的换弹时间

    [HideInInspector]
    public float fireCounter;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && maxAmmo>0)
        {
            isReloading = true;
            StartCoroutine(ReloadGun());
        }

        if(currentAmmo <= 0 && !isReloading && maxAmmo > 0)
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

        // 可加动画或音效
        yield return new WaitForSeconds(reloadTime);
        maxAmmo = Mathf.Max(maxAmmo, 0); // 确保最大弹药不小于0
        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log($"Reloaded {gameObject.name}. Ammo: {currentAmmo}/{maxAmmo}");
    }
}
