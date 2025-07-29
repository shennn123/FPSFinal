using UnityEngine;
using UnityEngine.UI;
using TMPro; // 必须引入 TextMeshPro 命名空间

using UnityEditor;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // 使用 TMP 的 UI 组件  

    public static CountdownTimer Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {

    }
    public void UpdateTimerDisplay(float totalTime)
    {
        int minutes = Mathf.FloorToInt(totalTime / 60f);
        int seconds = Mathf.FloorToInt(totalTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void resetColor()
    {
        timerText.color = Color.white;
    }

    public void OnTimeOver()
    {
        Debug.Log("TimeOver");

        timerText.text = "Time Over! Press 'I' to go to BOSS!";
        timerText.color = Color.red;
    }

    public void ResetTimer(float newTime)
    {
        RealTimer.Instance.ResetTimer(newTime);
    }
    public void PauseTimer()
    {
        RealTimer.Instance.PauseTimer();
    }
    public void ResumeTimer()
    {
        RealTimer.Instance.ResumeTimer();
    }


}
