using UnityEngine;

public class ShowStory1 : MonoBehaviour
{
    void Start()
    {
        UIManager.Show("Introduce1");
        UIManager.Hide("MainMenu");
    }

    void OnDestroy()
    {
        UIManager.Hide("Introduce1");
        AudioManager.EndMusic("my-comfortable-home-297586");
    }

    void Update()
    {
        // ����Բ�д Update�����û�õĻ�
    }
}
