using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public enum PickupType { Health, Armor  , HealthBox , AmmunitionBox , Adrenaline ,Hunt}
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
                            playerHealth.IncreaseArmor();
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

                    case PickupType.AmmunitionBox:
                        // 假设有一个方法可以增加弹药
                        playerHealth.IncreaseAmmunitionBox();
                        Debug.Log("Picked up ammunition box");
                        break;

                    case PickupType.Adrenaline:
                        playerHealth.IncreaseAdrenalineBox();
                        Debug.Log("Picked up adrenaline");
                        break;

                    case PickupType.Hunt:
                        BulletController bullet = PlayerController.instance.activeGun.bulletPrefab.GetComponent<BulletController>();
                        bullet.Damage += 5;
                        break;

                }

                Destroy(gameObject);
            }
        }
    }
}
