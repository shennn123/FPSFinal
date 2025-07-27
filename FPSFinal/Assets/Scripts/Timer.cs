using UnityEngine;
using UnityEngine.UI;
using TMPro; // 必须引入 TextMeshPro 命名空间

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // 使用 TMP 的 UI 组件  
    public float totalTime = 420f;
    private bool isRunning = true;
    private bool timeOver = false;

    public Transform player; // 玩家对象
    public Transform bossSpawnPoint; // Boss 区域的目标位置

    void Start()
    {
        if (bossSpawnPoint == null)
        {
            GameObject bossPoint = GameObject.Find("BossSpawnPoint");
            if (bossPoint != null)
            {
                bossSpawnPoint = bossPoint.transform;
            }
            else
            {
                Debug.LogWarning("找不到名为 'BossSpawnPoint' 的物体！");
            }
        }
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            if (player == null)
            {
                Debug.LogWarning("找不到玩家对象，请确保玩家有 'Player' 标签。");
            }
        }

    }

    void Update()
    {
        if (isRunning && totalTime > 0f)
        {
            totalTime -= Time.deltaTime;
            if (totalTime < 0f)
            {
                totalTime = 0f;
                isRunning = false;
                timeOver = true;
                OnTimeOver();
            }

            UpdateTimerDisplay();
        }

        if (timeOver && Input.GetKeyDown(KeyCode.I))
        {
            TeleportToBoss();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(totalTime / 60f);
        int seconds = Mathf.FloorToInt(totalTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void OnTimeOver()
    {
        Debug.Log("TimeOver");
        timerText.text = "Time Over! Press 'I' to go to BOSS!";
        timerText.color = Color.red;
    }

    void TeleportToBoss()
    {
        if (player != null && bossSpawnPoint != null)
        {
            player.position = bossSpawnPoint.position;
            Debug.Log("Teleported to BOSS area!");
        }
        else
        {
            Debug.LogWarning("Player or Boss spawn point not assigned.");
        }
    }

    public void ResetTimer(float newTime)
    {
        totalTime = newTime;
        isRunning = true;
        timeOver = false;
    }

    public void PauseTimer()
    {
        isRunning = false;
    }

    public void ResumeTimer()
    {
        isRunning = true;
    }
}
