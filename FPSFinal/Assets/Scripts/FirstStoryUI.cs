using UnityEngine;

public class FirstStoryUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIManager.Show("Introduce0");
    }


    private void OnDestroy()
    {
        // ÔÚÏú»ÙÊ±Òþ²Ø UI
        UIManager.Hide("Introduce0");
    }
}

