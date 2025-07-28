using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Typewriter : PanelBase
{
    public TextMeshProUGUI textComponent;
    public float typingSpeed = 0.05f;
    public Button nextButton;     // 拖入NextButton
    public Button closeButton;    // 拖入CloseButton

    [TextArea(3, 10)]
    public string[] pages;        // 多页文本数组

    [Header("场景设置")]
    public string sceneToLoad = "WinterScene"; // 默认加载WinterScene
    public bool lockCursorAfterLoad = true; // 加载后是否锁定光标

    private int currentPage = 0;
    private bool isTyping = false;


    protected override void Init()
    {
        Cursor.lockState = CursorLockMode.None; // 确保光标解锁
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
        if (isTyping)
        {
            Debug.Log("正在打字，无法关闭");
            return;
        }
        // 加载指定的场景
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            UIManager.Hide("Introduce2"); // 隐藏当前UI
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("没有指定要加载的场景！");
        }

        // 根据设置锁定光标
        if (lockCursorAfterLoad)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

}