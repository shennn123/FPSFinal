using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TypewriterEffect1 : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI textComponent;
    public Button closeButton; // ֻ�����رհ�ť
    public Image closeButtonIcon; // �رհ�ťͼ��

    [Header("Settings")]
    public float typingSpeed = 0.05f;
    public Color enabledButtonColor = Color.white;
    public Color disabledButtonColor = new Color(1, 1, 1, 0.3f);

    [TextArea(3, 10)]
    public string text; // ��ҳ�ı�������ʹ�����飩

    private bool isTyping = false;
    private Coroutine typingCoroutine;

    void Start()
    {
        // ��ʼ����ť״̬
        closeButton.onClick.AddListener(ClosePanel);
        closeButton.gameObject.SetActive(false); // ��ʼ���عرհ�ť

        // ��ʼ��ʾ�ı�
        StartTyping();
    }

    void StartTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeText(text));
    }

    IEnumerator TypeText(string textToType)
    {
        isTyping = true;
        textComponent.text = "";

        // ������ʾ
        foreach (char c in textToType)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        // �ı���ʾ��ɺ���ʾ�رհ�ť
        closeButton.gameObject.SetActive(true);
    }

    void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    // ���������ǰ���ֵĹ���
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                SkipTyping();
            }
            else if (closeButton.gameObject.activeSelf)
            {
                // ����ı��Ѿ���ʾ��ɣ��������λ�ùر�
                ClosePanel();
            }
        }
    }

    void SkipTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        textComponent.text = text;
        isTyping = false;
        // ������ʾ�رհ�ť
        closeButton.gameObject.SetActive(true);
    }
}