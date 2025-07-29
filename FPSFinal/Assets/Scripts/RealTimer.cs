using UnityEngine;
using UnityEngine.SceneManagement;
public class RealTimer : MonoBehaviour
{
    public static RealTimer Instance;
    public float totalTime = 42f;
    private bool isRunning = true;
    private bool timeOver = false;

    public Transform player; // 玩家对象
    public Transform bossSpawnPoint; // Boss 区域的目标位置

    //Boss Prefab
    public GameObject BossPrefab1;
    public GameObject BossPrefab2;
    public GameObject BossPrefab3;



    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        CountdownTimer.Instance.resetColor();
        if (bossSpawnPoint == null)
        {
            GameObject bossPoint = GameObject.Find("bossSpawnPoint");
            if (bossPoint != null)
            {
                bossSpawnPoint = bossPoint.transform;
            }
            else
            {
                Debug.LogWarning("找不到名为 'bossSpawnPoint' 的物体！");
            }
        }
        if (player == null)
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
                CountdownTimer.Instance.OnTimeOver();
                OnTimeOverBoss();
            }

            CountdownTimer.Instance.UpdateTimerDisplay(totalTime);
        }

        if (timeOver && Input.GetKeyDown(KeyCode.I))
        {
            TeleportToBoss();
        }
    }

    public void OnTimeOverBoss()
    {
        //根据对应场景在指定位置生成对应boss
        if (SceneManager.GetActiveScene().name == "WinterScene")
        {
            Instantiate(BossPrefab1, bossSpawnPoint.position, Quaternion.identity);
        }
        else if (SceneManager.GetActiveScene().name == "BeachScene")
        {
            Instantiate(BossPrefab2, bossSpawnPoint.position, Quaternion.identity);
        }
        else if (SceneManager.GetActiveScene().name == "3")
        {
            Instantiate(BossPrefab3, bossSpawnPoint.position, Quaternion.identity);
        }

    }


    public void TeleportToBoss()
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
