using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxTrigger : MonoBehaviour
{
    public GameObject boss; // Boss ����
    public float bossHealth = 100f;
    private bool bossDead = false; // ���ڼ�� Boss �Ƿ�����

    public GameObject targetBlock; // ����������������������

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
        if (bossHealth <= 0f && !bossDead)
        {
            bossDead = true;
            OnBossDefeated();
        }
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
        if (other.CompareTag("Player") && bossDead)
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