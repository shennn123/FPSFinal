using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;


public class MainMenuUIManager : PanelBase
{
    [SerializeField] private GameObject guidePanel;
    [SerializeField] private Button closeGuideButton;

    protected override void Init()
    {
        AudioManager.AddMusic("my-comfortable-home-297586");
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
    protected internal override void WhenHide()
    {
        // 显示主菜单时播放背景音乐
        AudioManager.EndMusic("my-comfortable-home-297586");
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
