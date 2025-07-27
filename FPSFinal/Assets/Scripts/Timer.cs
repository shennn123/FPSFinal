using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float totalTime = 420f; // 7 ���� = 420 ��
    public TMPro.TextMeshProUGUI timerText;         // UI Text ����ʾ����ʱ
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
                OnTimeOver(); // ʱ�����ʱ�����¼�����ѡ��
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
        Debug.Log("ʱ�䵽��");
        // ����������ﴥ����Ϸʧ�ܡ������������߼�
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
