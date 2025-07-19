using UnityEngine;
using UnityEngine.InputSystem.XR;

public class AmmoController : MonoBehaviour
{
    public static AmmoController instance; // Singleton instance of AmmoController
    public int MaxAmmo = 60; // Maximum ammo the player can hold
    public int currentAmmo = 30; // Current ammo the player has
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("UI Reference")]
    public TMPro.TextMeshProUGUI ammoText;
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
    void Start()
    {
        //UIController.instance.ammoText.text = "Ammo: " + currentAmmo + "/" + MaxAmmo; // Set the initial health text
        UpdateAmmoUI();
    }
    public void UpdateAmmoUI()
    {
        // 确保UI引用不为空
        if (ammoText != null)
        {
            // 更新弹药UI文本（格式："当前弹药/最大容量"）
            ammoText.text = $"{currentAmmo}/{MaxAmmo}";
        }
    }

    // Update is called once per frame
    void Update()
    {

        // UIController.instance.ammoText.text = "Ammo: " + currentAmmo + "/" + MaxAmmo; // Set the initial health text
    }

    public void Reload()
    {
        if (currentAmmo < MaxAmmo)
        {
            currentAmmo = MaxAmmo; // Reload to maximum ammo
            //UIController.instance.ammoText.text = "Ammo: " + currentAmmo + "/" + MaxAmmo; // Update the ammo text
            Debug.Log("Reloaded to max ammo: " + currentAmmo);
        }
        else
        {
            Debug.Log("Already at max ammo: " + currentAmmo);
        }
    }
    // 消耗弹药的方法（射击时调用）
    public void UseAmmo(int amount = 1)
    {
        if (currentAmmo >= amount)
        {
            currentAmmo -= amount;
            UpdateAmmoUI(); // 更新UI
        }
        else
        {
            Debug.Log("弹药不足!");
            // 这里可以触发空枪声音或其他反馈
        }
    }

}
