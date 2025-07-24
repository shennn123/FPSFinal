using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

[InitializeOnLoad]
public static class RememerPush
{
    static RememerPush()
    {
        EditorApplication.quitting += OnQuit;
    }

    private static void OnQuit()
    {

        string repoUrl = "https://github.com/Howchilll/DMT_FPS";
        bool confirm = EditorUtility.DisplayDialog(
            "别急着关！",
            "要不要 push 一下代码？\n\n现在是保存更改的好时机！",
            "打开github",
            "算了"
        );

        if (!confirm)
        {
            return;
        }

#if UNITY_EDITOR_OSX
    try
    {
        // macOS：优先尝试通过 URL 协议打开 GitHub Desktop
        Process.Start("open", "x-github-client://");
    }
    catch
    {
        try
        {
            // 尝试通过 app 名称打开
            Process.Start("open", "-a \"GitHub Desktop\"");
        }
        catch
        {
            // 跳转网页
            Application.OpenURL(repoUrl);
            Debug.LogWarning("GitHub Desktop 打不开（macOS），已跳转网页");
        }
    }
#elif UNITY_EDITOR_WIN
        try
        {
            Process.Start("x-github-client://");
        }
        catch
        {
            string fallbackPath = @"C:\Users\" + System.Environment.UserName +
                                  @"\AppData\Local\GitHubDesktop\GitHubDesktop.exe";
            if (System.IO.File.Exists(fallbackPath))
            {
                Process.Start(fallbackPath);
            }
            else
            {
                Application.OpenURL(repoUrl);
                Debug.LogWarning("GitHub Desktop 打不开（Windows），已跳转网页");
            }
        }
#else
    // Linux 或其他平台默认跳转网页
    Application.OpenURL(repoUrl);
#endif
    }
}

