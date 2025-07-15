using UnityEngine;

public class PlayerHealthController : MonoBehaviour, IDamageable
{
    public static PlayerHealthController instance; // Static instance for singleton pattern
    public int maxHealth = 5; // Maximum health of the player
    public int currentHealth; // Current health of the player

    public float invLength = 1f; // Invincibility duration after taking damage
    private float invCounter; // Timer for invincibility duration

    private void Awake()
    {
        instance = this; // Set the static instance to this instance of PlayerHealthController
    }
    void Start()
    {

        currentHealth = maxHealth; // Initialize current health to maximum health
        UIController.instance.healthSlider.maxValue = maxHealth; // Set the maximum value of the health slider
        UIController.instance.healthSlider.value = currentHealth; // Set the initial value of the health slider
        UIController.instance.healthText.text = "Health: " + currentHealth + "/" + maxHealth; // Set the initial health text
    }

    // Update is called once per frame
    void Update()
    {
        if (invCounter > 0)
            invCounter -= Time.deltaTime;// Decrease the invincibility timer

    }

    // 受到伤害时调用此方法
    public void DamagePlayer(int damage)
    {


    }

    public void TakeDamage(int damage, bool attackplayer)
    {
        if (attackplayer)
        {
            if (invCounter <= 0)
            {
                currentHealth -= damage;

                if (currentHealth <= 0)
                {

                    currentHealth = 0; // Ensure current health does not go below zero
                    Debug.Log("Player is dead!"); // Log player death
                    transform.parent.gameObject.SetActive(false); // Deactivate the player game object
                    GameManager.instance.PlayerDied(); // Call the PlayerDied method in GameManager 

                }
                invCounter = invLength; // Reset the invincibility timer
                UIController.instance.healthSlider.value = currentHealth; // Set the initial value of the health slider
                UIController.instance.healthText.text = "Health: " + currentHealth + "/" + maxHealth; // Set the initial health text
            }
        }
    }
}
