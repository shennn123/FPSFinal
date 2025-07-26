using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float typingSpeed = 0.05f;
    public Button nextButton;     // 拖入NextButton
    public Button closeButton;    // 拖入CloseButton

    [TextArea(3, 10)]
    public string[] pages;        // 多页文本数组

    private int currentPage = 0;
    private bool isTyping = false;

    void Start()
    {
        // 初始设置
        closeButton.gameObject.SetActive(false);
        nextButton.onClick.AddListener(GoToNextPage);
        closeButton.onClick.AddListener(ClosePanel);

        // 开始第一页
        StartCoroutine(TypeText(pages[currentPage]));
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        textComponent.text = "";

        // 逐字显示
        foreach (char c in text)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        UpdateButtonState();
    }

    void GoToNextPage()
    {
        if (isTyping) return; // 正在打字时忽略点击

        currentPage++;

        if (currentPage < pages.Length)
        {
            StartCoroutine(TypeText(pages[currentPage]));
        }

        UpdateButtonState();
    }

    void UpdateButtonState()
    {
        // 最后一页显示关闭按钮
        bool isLastPage = currentPage >= pages.Length - 1;
        nextButton.gameObject.SetActive(!isLastPage);
        closeButton.gameObject.SetActive(isLastPage);
    }

    void ClosePanel()
    {
        gameObject.SetActive(false); // 关闭整个对话框
    }
}