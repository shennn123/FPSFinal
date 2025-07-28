using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public enum Level { Winter, Beach, Forest }

    public Level currentLevel;

    void Update()
    {
        if (/* �ж�boss�Ƿ����� */)
        {
            // ��ȡ��ǰ�ؿ���Ӧ�Ĺ��³���
            string nextStoryScene = GetStorySceneForLevel(currentLevel);
            if (!string.IsNullOrEmpty(nextStoryScene))
            {
                // ���ض�Ӧ�Ĺ��³���
                SceneManager.LoadScene(nextStoryScene);
            }
        }
    }

    // ����ö�ٻ�ȡ��Ӧ�Ĺ��³���
    string GetStorySceneForLevel(Level level)
    {
        switch (level)
        {
            case Level.Winter:
                return "story1";  // ��Ӧ Winter �ؿ�
            case Level.Beach:
                return "story2";  // ��Ӧ Beach �ؿ�
            case Level.Forest:
                return "story3";  // ��Ӧ Forest �ؿ�
            default:
                return null;
        }
    }
}