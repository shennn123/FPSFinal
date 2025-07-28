using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxTrigger : MonoBehaviour
{
    public GameObject boss; // Boss 对象
    public float bossHealth = 100f;
    private bool bossDead = false; // 用于检查 Boss 是否死亡

    public GameObject targetBlock; // 方块对象，用来触发进入故事

    void Start()
    {
        // 确保方块初始时不可见
        if (targetBlock != null)
        {
            targetBlock.SetActive(false);
        }
        else
        {
            Debug.LogWarning("找不到方块对象，请确保已分配 'targetBlock'。");
        }
    }

    void Update()
    {
        // 检查 Boss 是否死亡（由血量决定）
        if (bossHealth <= 0f && !bossDead)
        {
            bossDead = true;
            OnBossDefeated();
        }
    }

    // Boss 死亡时启用方块
    void OnBossDefeated()
    {
        Debug.Log("Boss Defeated!");

        // 启用方块
        if (targetBlock != null)
        {
            targetBlock.SetActive(true); // 启用方块
        }
    }

    // 玩家碰到方块时进入对应的故事场景
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && bossDead)
        {
            LoadStoryScene();
        }
    }

    // 加载对应的故事场景
    void LoadStoryScene()
    {
        string sceneName = "";

        if (SceneManager.GetActiveScene().name == "WinterScene")
        {
            sceneName = "Story1";
        }
        else if (SceneManager.GetActiveScene().name == "BeachScene")
        {
            sceneName = "Story2";
        }
        else if (SceneManager.GetActiveScene().name == "3")
        {
            sceneName = "Story3";
        }

        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("没有找到对应的故事场景！");
        }
    }
}