using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxTrigger : MonoBehaviour
{
    //因为是全局管理器，所以可以设置为单例，便于其他物体上的代码引用，注意：场景中只能存在一个物体挂这个代码
    public static BoxTrigger Instance;

    public int bossHealth = 100; //默认血量数值不小于等于0就行

    //这个死亡信号我不知道你有没有用，感觉跟血量清零一个意思，我先注释掉了
    //private bool bossDead = false; // ���ڼ�� Boss �Ƿ�����

    public GameObject targetBlock; // ����������������������

    //单例的初始化
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // ȷ�������ʼʱ���ɼ�
        if (targetBlock != null)
        {
            targetBlock.SetActive(false);
        }
        else
        {
            Debug.LogWarning("�Ҳ������������ȷ���ѷ��� 'targetBlock'��");
        }
    }

    void Update()
    {
        // ��� Boss �Ƿ���������Ѫ��������
        if (bossHealth <= 0 /*&& !bossDead*/)
        {
            //bossDead = true;
            OnBossDefeated();
        }
    }

    //引用boss血量的函数，在MonsterHealth代码里，只有当boss类的怪物受到伤害时才会调用这个函数
    public void SetBossHealth(int bossCurrentHealth)
    {
        bossHealth = bossCurrentHealth;
    }

    // Boss ����ʱ���÷���
    void OnBossDefeated()
    {
        Debug.Log("Boss Defeated!");

        // ���÷���
        if (targetBlock != null)
        {
            targetBlock.SetActive(true); // ���÷���
        }
    }

    // �����������ʱ�����Ӧ�Ĺ��³���
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") /*&& bossDead*/)
        {
            LoadStoryScene();
        }
    }

    // ���ض�Ӧ�Ĺ��³���
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
            Debug.LogWarning("û���ҵ���Ӧ�Ĺ��³�����");
        }
    }
}