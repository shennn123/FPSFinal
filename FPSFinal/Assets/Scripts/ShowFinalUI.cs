using UnityEngine;

public class ShowFinalUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIManager.Show("Final");
        AudioManager.AddMusic("my-comfortable-home-297586"); // Ìí¼Ó±³¾°ÒôÀÖ
    }

    void OnDestroy()
    {
        // È·±£ÔÚÏú»ÙÊ±Òş²ØUI
        UIManager.Hide("Final");
        AudioManager.EndMusic("my-comfortable-home-297586"); // Í£Ö¹±³¾°ÒôÀÖ
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
