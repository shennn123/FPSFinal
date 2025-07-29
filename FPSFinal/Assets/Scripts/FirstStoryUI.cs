using UnityEngine;

public class FirstStoryUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIManager.Show("Introduce0");
        AudioManager.AddMusic("smile-booth-305345");
    }


    private void OnDestroy()
    {
        // ÔÚÏú»ÙÊ±Òþ²Ø UI
        UIManager.Hide("Introduce0");
        AudioManager.EndMusic("smile-booth-305345");
    }
}

