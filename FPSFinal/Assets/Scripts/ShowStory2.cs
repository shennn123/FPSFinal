using UnityEngine;

public class ShowStory2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIManager.Show("Introduce2"); // 显示海滩UI
    }

    private void OnDestroy()
    {
        UIManager.Hide("Introduce2"); // 销毁时隐藏海滩UI
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
