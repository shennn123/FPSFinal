using UnityEngine;

public class WaterFlow : MonoBehaviour
{
    public float scrollSpeedX = 0.1f;  // 水流沿X轴的速度
    public float scrollSpeedY = 0.0f;  // 水流沿Y轴的速度（一般设置为0，除非有特殊要求）
    
    private Renderer rend;
    private Vector2 offset;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        // 根据时间和滚动速度更新偏移
        offset.x += scrollSpeedX * Time.deltaTime;
        offset.y += scrollSpeedY * Time.deltaTime;
        
        // 应用偏移到材质的_MainTex（纹理的偏移）
        rend.material.SetTextureOffset("_MainTex", offset);
    }
}
