using UnityEngine;

public class ShowFinalUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIManager.Show("Final");
        AudioManager.AddMusic("my-comfortable-home-297586"); // ��ӱ�������
    }

    void OnDestroy()
    {
        // ȷ��������ʱ����UI
        UIManager.Hide("Final");
        AudioManager.EndMusic("my-comfortable-home-297586"); // ֹͣ��������
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
