using UnityEngine;

public class ShowStory1 : MonoBehaviour
{
    void Start()
    {
        UIManager.Show("Introduce1");
    }

    void OnDestroy()
    {
        UIManager.Hide("Introduce1");
    }

    void Update()
    {
        // ����Բ�д Update�����û�õĻ�
    }
}
