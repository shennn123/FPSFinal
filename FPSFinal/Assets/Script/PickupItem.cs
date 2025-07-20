using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public enum PickupType { Health, Armor } // ʰȡ����
    public PickupType pickupType;

    [Header("Health Settings")]
    public int healAmount = 30;

    [Header("Armor Settings")]
    public float armorReduction = 0.4f; // ���˱�������0.4��ʾ��40%��

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
                            Debug.Log($"Picked up armor: -{armorReduction * 100f}% damage");
                        }
                        break;
                }

                Destroy(gameObject); // ʰȡ������
            }
        }
    }
}