using UnityEngine;

public class ShowFinalUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIManager.Show("Final");
    }

    void OnDestroy()
    {
        // 确保在销毁时隐藏UI
        UIManager.Hide("Final");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
