using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance; // Static instance for singleton pattern
    public int maxHealth = 100; // Maximum health of the player
    public int currentHealth = 5; // Current health of the player

    public float invLength = 1f; // Invincibility duration after taking damage
    private float invCounter; // Timer for invincibility duration

    public bool hasArmor = false;
    public float damageReduction; 

    public float remainingArmorAbsorb = 0f; // 护甲剩余可吸收伤害值
    public float maxArmorAbsorb = 50f;      // 最大护甲吸收值（可通过拾取设置）


    public int HealthBoxAmount = 0; // Amount of health restored by a health box 


    private void Awake()
    {
        instance = this; // Set the static instance to this instance of PlayerHealthController
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            if (currentHealth > 0 && HealthBoxAmount > 0)
            {
                Debug.Log("Current Health: " + currentHealth); // Log the current health
                IncreaseHealth(); // Call the method to increase health box amount when pressing Alpha1 key
            }
        }

        invCounter -= Time.deltaTime; 
    }


     private void IncreaseHealth()

    { 
        if (HealthBoxAmount > 0)
        {
            HealthBoxAmount--;
            UIController.instance.UpdateHealthPack( );
            HealPlayer(50); // Heal the player by 1 health point
            // Decrease the health box amount by 1
            Debug.Log("Health Box Amount decreased to: " + HealthBoxAmount); // Log the decrease
        }
        else
        {
            Debug.Log("No Health Boxes left!"); // Log if no health boxes are left
        }
    }


    public void IncreaseHealthBox()
    {

        if (HealthBoxAmount < 5)
        {
            HealthBoxAmount++; // Increase the health box amount by 1
            UIController.instance.UpdateHealthPack();
            Debug.Log("Health Box Amount increased to: " + HealthBoxAmount); // Log the increase
        }
        else
        {
            Debug.Log("Health Box Amount is already at maximum!"); // Log if already at maximum
        }
    }

    public void DamagePlayer(int damage, bool attackplayer)
    {
        if (!attackplayer) return;

        if (invCounter <= 0)
        {
            int finalDamage = damage;

            if (hasArmor && remainingArmorAbsorb > 0f)
            {
                // 计算护甲本应吸收的伤害量
                float absorbable = damage * damageReduction;

                // 实际吸收不能超过剩余可吸收值
                float absorbed = Mathf.Min(absorbable, remainingArmorAbsorb);

                finalDamage = Mathf.CeilToInt(damage - absorbed);
                remainingArmorAbsorb -= absorbed;


                // 护甲吸收完毕
                if (remainingArmorAbsorb <= 0f)
                {
                    hasArmor = false;
                    damageReduction = 0f;
                    Debug.Log("Armor broken!");
                }
            }

            // 扣血
            currentHealth -= finalDamage;
            Debug.Log($"Player took {finalDamage} damage. Current Health: {currentHealth}, Remaining Armor Absorb: {remainingArmorAbsorb}");
            UIController.instance.HealthSlider.value = (float)currentHealth / maxHealth;
            UIController.instance.AmrorSlider.value = (float)remainingArmorAbsorb / maxArmorAbsorb;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Debug.Log("Player is dead!");
                transform.gameObject.SetActive(false);
                GameManager.instance.PlayerDied();
 
         
            }

            // 更新血量UI & 无敌帧
            invCounter = invLength;
        }
    }


    public void HealPlayer(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        // 更新 UI 血条
        UIController.instance.HealthSlider.value = (float)currentHealth / maxHealth;
    }

    public void ResetHealthStats()
    {
        // 血量回满
        currentHealth = maxHealth;
        UIController.instance.HealthSlider.value = 1f;

        // 血包回满（看你需求可以不回满）
        HealthBoxAmount = 1; // 或者你想设置 0、5 都可以
        UIController.instance.UpdateHealthPack();

        // 护甲回满（如果你希望复活时带护甲）
        hasArmor = true;
        remainingArmorAbsorb = maxArmorAbsorb;
        damageReduction = 0.5f; // 可配置：你实际用的值是多少就写多少
        UIController.instance.AmrorSlider.value = 1f;

        // 清除无敌时间（可选）
        invCounter = 0f;

        Debug.Log("Player health & armor reset.");
    }

}
