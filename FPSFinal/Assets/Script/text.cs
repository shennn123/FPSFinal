using UnityEngine;
using TMPro; // ������� TextMeshPro

public class UIPickupPrompt : MonoBehaviour
{
    public static UIPickupPrompt instance;

    public GameObject promptRoot;

    private void Awake()
    {
        instance = this;
        promptRoot.SetActive(false); // ��ʼ����
    }

    public void Show()
    {
        Debug.Log("��ʾ��ʾUI");
        promptRoot.SetActive(true);
    }

    public void Hide()
    {
        promptRoot.SetActive(false);
    }
}