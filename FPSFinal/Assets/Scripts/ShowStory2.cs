using UnityEngine;

public class ShowStory2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIManager.Show("Introduce2"); // ��ʾ��̲UI
        AudioManager.AddMusic("smile-booth-305345"); // ��ӱ�������
    }

    private void OnDestroy()
    {
        UIManager.Hide("Introduce2"); // ����ʱ���غ�̲UI
        AudioManager.EndMusic("smile-booth-305345"); // ֹͣ��������
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
