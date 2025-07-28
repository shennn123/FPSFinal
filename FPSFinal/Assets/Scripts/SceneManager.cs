using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public enum Level { Winter, Beach, Forest }

    public Level currentLevel;

    void Update()
    {
        if (/* 判断boss是否死亡 */)
        {
            // 获取当前关卡对应的故事场景
            string nextStoryScene = GetStorySceneForLevel(currentLevel);
            if (!string.IsNullOrEmpty(nextStoryScene))
            {
                // 加载对应的故事场景
                SceneManager.LoadScene(nextStoryScene);
            }
        }
    }

    // 根据枚举获取对应的故事场景
    string GetStorySceneForLevel(Level level)
    {
        switch (level)
        {
            case Level.Winter:
                return "story1";  // 对应 Winter 关卡
            case Level.Beach:
                return "story2";  // 对应 Beach 关卡
            case Level.Forest:
                return "story3";  // 对应 Forest 关卡
            default:
                return null;
        }
    }
}