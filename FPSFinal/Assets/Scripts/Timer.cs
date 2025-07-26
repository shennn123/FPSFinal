using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float totalTime = 420f; // 7 分钟 = 420 秒
    public Text timerText;         // UI Text 来显示倒计时
    private bool isRunning = true;

    void Update()
    {
        if (isRunning && totalTime > 0f)
        {
            totalTime -= Time.deltaTime;
            if (totalTime < 0f)
            {
                totalTime = 0f;
                isRunning = false;
                OnTimeOver(); // 时间结束时调用事件（可选）
            }

            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(totalTime / 60f);
        int seconds = Mathf.FloorToInt(totalTime % 60f);
        timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }

    void OnTimeOver()
    {
        Debug.Log("时间到！");
        // 你可以在这里触发游戏失败、结束场景等逻辑
    }

    public void ResetTimer(float newTime)
    {
        totalTime = newTime;
        isRunning = true;
    }

    public void PauseTimer()
    {
        isRunning = false;
    }

    public void ResumeTimer()
    {
        isRunning = true;
    }
}
