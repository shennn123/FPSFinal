using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public enum PickupType { Health, Armor  , HealthBox }
    public PickupType pickupType;

    [Header("Health Settings")]
    public int healAmount = 30;

    [Header("Armor Settings")]
    public float armorReduction = 0.4f; // 减伤比例
    public float maxAbsorbAmount = 50f; // 最大可吸收伤害值

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthController playerHealth = other.GetComponent<PlayerHealthController>();
            if (playerHealth != null)
            {
                switch (pickupType)
                {
                    case PickupType.Health:
                        if (playerHealth.currentHealth < playerHealth.maxHealth)
                        {
                            playerHealth.HealPlayer(healAmount);
                            Debug.Log($"Picked up health: +{healAmount} HP");
                        }
                        break;

                    case PickupType.Armor:
                        if (!playerHealth.hasArmor)
                        {
                            playerHealth.hasArmor = true;
                            playerHealth.damageReduction = armorReduction;
                            playerHealth.remainingArmorAbsorb = maxAbsorbAmount;
                            Debug.Log($"Picked up armor: -{armorReduction * 100f}% damage, up to {maxAbsorbAmount} total absorbed");
                        }
                        break;

                    case PickupType.HealthBox:
                        if (playerHealth.currentHealth>0)
                        {
                            playerHealth.IncreaseHealthBox();
                            Debug.Log($"Picked up health box: +{healAmount} HP");
                        }
                        break;

                }

                Destroy(gameObject);
            }
        }
    }
}
