using UnityEngine;

public class ShowStory3 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIManager.Show("Introduce3");
    }

    void OnDestroy()
    {
        // 确保在销毁时隐藏界面
        UIManager.Hide("Introduce3");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
