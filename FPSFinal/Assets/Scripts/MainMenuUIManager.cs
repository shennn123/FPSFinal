using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class MainMenuUIManager : PanelBase
{
    [SerializeField] private GameObject guidePanel;
    [SerializeField] private Button closeGuideButton;

    protected override void Init()
    {
        if (guidePanel != null)
        {
            guidePanel.SetActive(false);
        }

        // 设置关闭按钮事件
        if (closeGuideButton != null)
        {
            closeGuideButton.onClick.AddListener(CloseGuidePanel);
        }
    }
    void Update()
    {
        // ESC键关闭指南面板
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseGuidePanel();
        }
    }
    public void StartMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        UIManager.Hide("MainMenu");
    }
    public void OpenGuidePanel()
    {
        if (guidePanel != null)
        {
            guidePanel.SetActive(true);
        }
    }

    public void CloseGuidePanel()
    {
        if (guidePanel != null && guidePanel.activeSelf)
        {
            guidePanel.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
