using UnityEngine;
using TMPro; // 如果你用 TextMeshPro

public class UIPickupPrompt : MonoBehaviour
{
    public static UIPickupPrompt instance;

    public GameObject promptRoot;

    private void Awake()
    {
        instance = this;
        promptRoot.SetActive(false); // 初始隐藏
    }

    public void Show()
    {
        Debug.Log("显示提示UI");
        promptRoot.SetActive(true);
    }

    public void Hide()
    {
        promptRoot.SetActive(false);
    }
}