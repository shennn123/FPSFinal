using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;

public class LineCountResultWindow : EditorWindow
{
    private string result = "统计中...";

    public static void ShowResult(string result)
    {
        LineCountResultWindow window = GetWindow<LineCountResultWindow>("C# 行数统计");
        window.result = result;
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("C# 代码行统计结果", EditorStyles.boldLabel);
        EditorGUILayout.TextArea(result, GUILayout.ExpandHeight(true));
    }
}

public class CalculateLines
{
    [MenuItem("Tools/Count C# Lines (Scripts Only)")]
    static void CountLines()
    {
        string targetPath = Path.Combine(Application.dataPath, "Scripts");
        if (!Directory.Exists(targetPath))
        {
            EditorUtility.DisplayDialog("错误", "Assets/Scripts 文件夹不存在。", "OK");
            return;
        }

        string[] files = Directory.GetFiles(targetPath, "*.cs", SearchOption.AllDirectories);

        int totalFiles = 0;
        int totalLines = 0;
        int codeLines = 0;
        int commentLines = 0;
        int blankLines = 0;
        int methodCount = 0;

        // 用正则来匹配方法定义（不算构造函数、属性等）
        Regex methodPattern = new Regex(@"\b(public|private|protected|internal|static|virtual|override|async|sealed)?\s+\b[\w\<\>\[\]]+\s+\w+\s*\(.*\)\s*(\{?|=>)", RegexOptions.Compiled);

        foreach (string file in files)
        {
            totalFiles++;

            string[] lines = File.ReadAllLines(file);
            totalLines += lines.Length;

            foreach (string rawLine in lines)
            {
                string line = rawLine.Trim();
                if (string.IsNullOrEmpty(line)) blankLines++;
                else if (line.StartsWith("//")) commentLines++;
                else
                {
                    codeLines++;
                    if (methodPattern.IsMatch(line))
                        methodCount++;
                }
            }
        }

        string result = $"📁 统计目录: Assets/Scripts\n\n" +
                        $"📄 C# 文件数: {totalFiles}\n" +
                        $"🔢 总行数: {totalLines}\n" +
                        $"🔧 代码行: {codeLines}\n" +
                        $"💬 注释行: {commentLines}\n" +
                        $"⬜ 空行: {blankLines}\n" +
                        $"🧩 方法数量: {methodCount}";

        LineCountResultWindow.ShowResult(result);
    }
}
