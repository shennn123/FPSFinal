using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float typingSpeed = 0.05f;
    public Button nextButton;     // ����NextButton
    public Button closeButton;    // ����CloseButton

    [TextArea(3, 10)]
    public string[] pages;        // ��ҳ�ı�����

    private int currentPage = 0;
    private bool isTyping = false;

    void Start()
    {
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
        gameObject.SetActive(false); // �ر������Ի���
    }
}