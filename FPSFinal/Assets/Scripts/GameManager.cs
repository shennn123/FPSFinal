using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // For working with UI

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int playerScore = 0; // Player's score
    public CanvasGroup deathCanvasGroup;      // Fade-in panel
    public Transform respawnPoint;            // Respawn position
    public GameObject player;                 // Player object
    public int deathCount = 5;                // Remaining respawns
    public GameObject gameOverPanel;          // Panel to show when the game is over

    public List<Image> heartsUI;              // List of heart icons in UI
    private bool bossDefeated = false;        // Flag to check if the boss is defeated

    void Awake()
    {
        if (instance == null)
        {
            instance = this; // Ensure only one instance of GameManager exists
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
        DontDestroyOnLoad(gameObject); // Keep GameManager persistent across scenes
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "1")
        {
            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
            if (UIController.instance != null)
            {
                UIController.instance.UpdateHeartsUI(deathCount);
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor when not in the game scene
            UIManager.Show("MainMenu");
            Cursor.visible = true; // Make the cursor visible
        }

        
        UpdateHeartsUI(); // Initialize hearts UI on start
    }

    void Update()
    {
        // Optionally, add any game-related updates here
    }

    public void PlayerDied()
    {
        Debug.Log("Player has died!");
        StartCoroutine(PlayerDiedCo());
    }

    public IEnumerator PlayerDiedCo()
    {
        // Fade in death panel
        if (deathCanvasGroup != null)
        {
            deathCanvasGroup.gameObject.SetActive(true);
            float duration = 1f;
            float t = 0f;
            while (t < duration)
            {
                t += Time.deltaTime;
                deathCanvasGroup.alpha = Mathf.Lerp(0, 1, t / duration);
                yield return null;
            }
        }

        // Decrease remaining death count
        deathCount--;
        UIController.instance.UpdateHeartsUI(deathCount); // Update the heart UI to reflect remaining lives

        yield return new WaitForSeconds(5f);

        if (deathCount > 0)
        {
            RespawnPlayer();
            // Fade out death panel
            if (deathCanvasGroup != null)
            {
                float t = 0f;
                while (t < 1f)
                {
                    t += Time.deltaTime;
                    deathCanvasGroup.alpha = Mathf.Lerp(1, 0, t);
                    yield return null;
                }
                deathCanvasGroup.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("No more respawns, Game Over!");
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }

            SceneManager.LoadScene("MainMenu");
        }
    }

    void RespawnPlayer()
    {
        if (player != null && respawnPoint != null)
        {
            player.transform.position = respawnPoint.position;
            player.transform.rotation = respawnPoint.rotation; // Reset player position

            // Reset player Rigidbody velocity and angular velocity
            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Option: Temporarily disable physics (sleep)
                rb.Sleep(); // This stops the Rigidbody from moving temporarily
            }
        }
    }


    // Update hearts UI based on remaining deathCount
    void UpdateHeartsUI()
    {
        for (int i = 0; i < heartsUI.Count; i++)
        {
            if (i < deathCount)
            {
                heartsUI[i].enabled = true;  // Enable heart (visible)
            }
            else
            {
                heartsUI[i].enabled = false; // Disable heart (hidden)
            }
        }
    }

    // Boss defeat logic
    public void BossDefeated()
    {
        bossDefeated = true;
        Debug.Log("Boss defeated! Proceeding to next level...");
        // Logic to load next level
        LoadNextLevel();
    }

    void LoadNextLevel()
    {
        string nextLevel = "Level" + (int.Parse(SceneManager.GetActiveScene().name.Replace("Level", "")) + 1);
        SceneManager.LoadScene(nextLevel);
    }
}