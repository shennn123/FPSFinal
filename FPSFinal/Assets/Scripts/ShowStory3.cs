using UnityEngine;

public class ShowStory3 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIManager.Show("Introduce3");
        AudioManager.AddMusic("smile-booth-305345"); // ��ӱ�������
    }

    void OnDestroy()
    {
        // ȷ��������ʱ���ؽ���
        UIManager.Hide("Introduce3");
        AudioManager.EndMusic("smile-booth-305345"); // ֹͣ��������
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
