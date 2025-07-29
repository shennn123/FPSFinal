using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class BoxTrigger : MonoBehaviour
{
    //因为是全局管理器，所以可以设置为单例，便于其他物体上的代码引用，注意：场景中只能存在一个物体挂这个代码
    public static BoxTrigger Instance;

    public int bossHealth = 100; //默认血量数值不小于等于0就行

    public GameObject Portal; 

    public Transform bossSpawnPoint;

    //单例的初始化
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (Portal != null)
        {
            Portal.SetActive(false);
        }
        else
        {
            Debug.LogWarning("找不到Portal");
        }

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
    }

    void Update()
    {

        Debug.Log("bossHealth2 is "+bossHealth);
        // Boss血量小于0
        if (bossHealth <= 0)
        {
            OnBossDefeated();
            Debug.Log("bossHealth is " + bossHealth);
        }
    }

    //引用boss血量的函数，在MonsterHealth代码里，只有当boss类的怪物受到伤害时才会调用这个函数
    public void SetBossHealth(int bossCurrentHealth)
    {
        bossHealth = bossCurrentHealth;
        Debug.Log("set bossHealth is "+bossHealth);
    }

    // 
    void OnBossDefeated()
    {
        Debug.Log("Boss Defeated!");

        // 
        if (Portal != null)
        {
            Portal.SetActive(true); 
            Portal.transform.position = bossSpawnPoint.position;
        }

    }

    //Portal 代码里引用LoadStoryScene
    // 
    public void LoadStoryScene()
    {
        string sceneName = "";

        if (SceneManager.GetActiveScene().name == "WinterScene")
        {
            sceneName = "Story1";
        }
        else if (SceneManager.GetActiveScene().name == "BeachScene")
        {
            UIManager.Hide("InGameUI"); // 隐藏当前UI
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
            Debug.LogWarning("û���ҵ���Ӧ�Ĺ��³�����");
        }
    }
}