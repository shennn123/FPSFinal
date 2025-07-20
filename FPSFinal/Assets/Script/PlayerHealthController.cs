using UnityEngine;

public class PlayerHealthController : MonoBehaviour, IDamageable
{
    public static PlayerHealthController instance; // Static instance for singleton pattern
    public int maxHealth = 100; // Maximum health of the player
    public int currentHealth = 5; // Current health of the player

    public float invLength = 1f; // Invincibility duration after taking damage
    private float invCounter; // Timer for invincibility duration

    public bool hasArmor = false;
    public float damageReduction; // ��ʰȡ���趨

    public float remainingArmorAbsorb = 0f; // 护甲剩余可吸收伤害值
    public float maxArmorAbsorb = 50f;      // 最大护甲吸收值（可通过拾取设置）


    public int HealthBoxAmount = 0; // Amount of health restored by a health box 

    private void Awake()
    {
        instance = this; // Set the static instance to this instance of PlayerHealthController
    }
    void Start()
    {

        currentHealth = maxHealth; // Initialize current health to maximum health
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

    }


     private void IncreaseHealth()

    { 
        if (HealthBoxAmount > 0)
        {
            HealthBoxAmount--;
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
            Debug.Log("Health Box Amount increased to: " + HealthBoxAmount); // Log the increase
        }
        else
        {
            Debug.Log("Health Box Amount is already at maximum!"); // Log if already at maximum
        }
    }

    public void DamagePlayer(int damage)
    {
        Debug.Log("Player took damage: " + damage); // Log the damage taken
        currentHealth -= damage; // Reduce current health by damage amount
        if (currentHealth <= 0)
        {
            currentHealth = 0; // Ensure current health does not go below zero
            Debug.Log("Player is dead!"); // Log player death
            Destroy(gameObject); // Destroy the player game object
            GameManager.instance.PlayerDied(); // Call the PlayerDied method in GameManager 
        }

    }

    public void TakeDamage(int damage, bool attackplayer)
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

                Debug.Log($"Armor absorbed {absorbed} damage. Remaining armor: {remainingArmorAbsorb}");

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
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Debug.Log("Player is dead!");
                transform.parent.gameObject.SetActive(false);
                GameManager.instance.PlayerDied();
            }

            // 更新血量UI & 无敌帧
            invCounter = invLength;
            UIController.instance.HealthSlider.value = currentHealth;
        }
    }

    public void HealPlayer(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

    }
}
