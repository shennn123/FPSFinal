using UnityEngine;

public class ShowStory2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIManager.Show("Introduce2"); // œ‘ æ∫£Ã≤UI
        AudioManager.AddMusic("smile-booth-305345"); // ÃÌº”±≥æ∞“Ù¿÷
    }

    private void OnDestroy()
    {
        UIManager.Hide("Introduce2"); // œ˙ªŸ ±“˛≤ÿ∫£Ã≤UI
        AudioManager.EndMusic("smile-booth-305345"); // Õ£÷π±≥æ∞“Ù¿÷
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
