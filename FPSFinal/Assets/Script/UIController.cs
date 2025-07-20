using UnityEngine;
using UnityEngine.UI;

public class UIController : PanelBase
{
    public static UIController instance; // Singleton instance of UIController
    public Slider HealthSlider;
    [Header("UI Reference")]
    public TMPro.TextMeshProUGUI ammoText;
    [Header("Weapon UI")]
    public Image weaponIcon; // 显示当前武器图标的Image组件
    public Sprite[] weaponSprites; // 4种武器的图标（按顺序对应）



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // Assign the singleton instance
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    protected override void Init()
    {
      
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateAmmoUI()
    {
        // 确保UI引用不为空
        if (ammoText != null)
        {
            Debug.Log("Max = " + PlayerController.instance.activeGun.maxAmmo);
            // 更新弹药UI文本（格式："当前弹药/最大容量"）
            ammoText.text = (PlayerController.instance.activeGun.currentAmmo + "/" + PlayerController.instance.activeGun.maxAmmo);
              // $"{PlayerController.instance.activeGun.currentAmmo}/{PlayerController.instance.activeGun.maxAmmo}";
        }
        else
        {
            Debug.LogWarning("PlayerController or activeGun is null, cannot update ammo UI.");
        }
    }
    public void UpdateWeaponUI()
    {
        // 直接替换为当前武器的图标
        if (weaponIcon != null && weaponSprites.Length > PlayerController.instance.currentGunIndex)
        {
            weaponIcon.sprite = weaponSprites[PlayerController.instance.currentGunIndex];
        }
    }

}
