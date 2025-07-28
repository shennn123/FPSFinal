using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Typewriter : PanelBase
{
    public TextMeshProUGUI textComponent;
    public float typingSpeed = 0.05f;
    public Button nextButton;     // ����NextButton
    public Button closeButton;    // ����CloseButton

    [TextArea(3, 10)]
    public string[] pages;        // ��ҳ�ı�����

    [Header("��������")]
    public string sceneToLoad = "WinterScene"; // Ĭ�ϼ���WinterScene
    public bool lockCursorAfterLoad = true; // ���غ��Ƿ��������

    private int currentPage = 0;
    private bool isTyping = false;


    protected override void Init()
    {
        Cursor.lockState = CursorLockMode.None; // ȷ��������
        // ��ʼ����
        closeButton.gameObject.SetActive(false);
        nextButton.onClick.AddListener(GoToNextPage);
        closeButton.onClick.AddListener(ClosePanel);

        // ��ʼ��һҳ
        StartCoroutine(TypeText(pages[currentPage]));
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        textComponent.text = "";

        // ������ʾ
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
        if (isTyping) return; // ���ڴ���ʱ���Ե��

        currentPage++;

        if (currentPage < pages.Length)
        {
            StartCoroutine(TypeText(pages[currentPage]));
        }

        UpdateButtonState();
    }

    void UpdateButtonState()
    {
        // ���һҳ��ʾ�رհ�ť
        bool isLastPage = currentPage >= pages.Length - 1;
        nextButton.gameObject.SetActive(!isLastPage);
        closeButton.gameObject.SetActive(isLastPage);
    }

    void ClosePanel()
    {
        if (isTyping)
        {
            Debug.Log("���ڴ��֣��޷��ر�");
            return;
        }
        // ����ָ���ĳ���
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            UIManager.Hide("Introduce2"); // ���ص�ǰUI
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("û��ָ��Ҫ���صĳ�����");
        }

        // ���������������
        if (lockCursorAfterLoad)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

}