using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TypewriterEffect1 : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI textComponent;
    public Button closeButton; // 只保留关闭按钮
    public Image closeButtonIcon; // 关闭按钮图标

    [Header("Settings")]
    public float typingSpeed = 0.05f;
    public Color enabledButtonColor = Color.white;
    public Color disabledButtonColor = new Color(1, 1, 1, 0.3f);

    [TextArea(3, 10)]
    public string text; // 单页文本（不再使用数组）

    private bool isTyping = false;
    private Coroutine typingCoroutine;

    void Start()
    {
        // 初始化按钮状态
        closeButton.onClick.AddListener(ClosePanel);
        closeButton.gameObject.SetActive(false); // 初始隐藏关闭按钮

        // 开始显示文本
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

        // 逐字显示
        foreach (char c in textToType)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        // 文本显示完成后显示关闭按钮
        closeButton.gameObject.SetActive(true);
    }

    void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    // 添加跳过当前打字的功能
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
                // 如果文本已经显示完成，点击任意位置关闭
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
        // 立即显示关闭按钮
        closeButton.gameObject.SetActive(true);
    }
}