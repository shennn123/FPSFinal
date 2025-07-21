using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // For scene management

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is create
    public static GameManager instance;

    public int playerScore = 0; // Player's score
    public CanvasGroup deathCanvasGroup;      // 淡入面板
    public Transform respawnPoint;            // 复活位置
    public GameObject player;                 // 玩家对象
    public int deathCount = 5;                // 剩余复活次数
    public TMPro.TextMeshProUGUI deathCountText; // UI 显示剩余次数
    public GameObject gameOverPanel;          // 当死亡次数用尽时显示

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

        // 更新死亡次数
        deathCount--;
        UpdateDeathCountUI();

        yield return new WaitForSeconds(1f);

        if (deathCount > 0)
        {
            RespawnPlayer();
            // 淡出面板
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
            Debug.Log("死亡次数用尽，Game Over！");
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }
           
            SceneManager.LoadScene("MainMenu");
        }
        void RespawnPlayer()
        {
            if (player != null && respawnPoint != null)
            {
                player.transform.position = respawnPoint.position;
                player.transform.rotation = respawnPoint.rotation;//重置玩家位置

                // 若玩家有 Rigidbody 则清除速度
                Rigidbody rb = player.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }                                                       //这个我代码先放这了___ywx,不是很懂我们死亡系统是啥样的.
            }
        }

        void UpdateDeathCountUI()
        {
            if (deathCountText != null)
            {
                deathCountText.text = "x " + deathCount;
            }
        }
    }
}
