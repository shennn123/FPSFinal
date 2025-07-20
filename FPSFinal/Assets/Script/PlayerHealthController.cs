using UnityEngine;

public class PlayerHealthController : MonoBehaviour, IDamageable
{
    public static PlayerHealthController instance; // Static instance for singleton pattern
    public int maxHealth = 5; // Maximum health of the player
    public int currentHealth = 5; // Current health of the player

    public float invLength = 1f; // Invincibility duration after taking damage
    private float invCounter; // Timer for invincibility duration

    public bool hasArmor = false;
    public float damageReduction; // ��ʰȡ���趨

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


    }

    // �ܵ��˺�ʱ���ô˷���
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
        if (attackplayer)
        {
            if (invCounter <= 0)
            {
                if (hasArmor)
                    damage = Mathf.CeilToInt(damage * (1f - damageReduction));

                currentHealth -= damage;

                if (currentHealth <= 0)
                {

                    currentHealth = 0; // Ensure current health does not go below zero
                    Debug.Log("Player is dead!"); // Log player death
                    transform.parent.gameObject.SetActive(false); // Deactivate the player game object
                    GameManager.instance.PlayerDied(); // Call the PlayerDied method in GameManager 

                }
                invCounter = invLength; // Reset the invincibility timer
                UIController.instance.HealthSlider.value = currentHealth; // Set the initial value of the health slider
            }
        }
    }

    public void HealPlayer(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        UIController.instance.HealthSlider.value = currentHealth;
    }
}
