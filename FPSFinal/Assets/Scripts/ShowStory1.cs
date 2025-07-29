using UnityEngine;

public class ShowStory1 : MonoBehaviour
{
    void Start()
    {
        UIManager.Show("Introduce1");
        UIManager.Hide("MainMenu");
        AudioManager.AddMusic("smile-booth-305345");
    }

    void OnDestroy()
    {
        UIManager.Hide("Introduce1");
        AudioManager.EndMusic("smile-booth-305345");
    }


    void Update()
    {
        // ����Բ�д Update�����û�õĻ�
    }
}
