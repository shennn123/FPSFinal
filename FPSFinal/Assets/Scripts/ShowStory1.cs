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
        // 你可以不写 Update，如果没用的话
    }
}
