using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // For scene management

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is create
    public static GameManager instance;

    public int playerScore = 0; // Player's score

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
        if(SceneManager.GetActiveScene().name != "1")
        {
            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        }
        else
        {
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor when not in the game scene
            Cursor.visible = true; // Make the cursor visible
        }

        UIManager.Show("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayerDied()
    {
        Debug.Log("Player has died!"); // Log player death
        StartCoroutine(PlayerDiedCo()); // Start the coroutine to handle player death
    }

    public IEnumerator PlayerDiedCo()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds before respawning

        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Load the GameOver scene
    }
}
