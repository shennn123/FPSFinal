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
        // 你可以不写 Update，如果没用的话
    }
}
