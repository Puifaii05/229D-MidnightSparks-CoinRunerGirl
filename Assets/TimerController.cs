using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerController : MonoBehaviour
{
    public float timeRemaining = 60f; // �������������� (60 �Թҷ�)
    public bool isCountdown = true; // true = �Ѻ�����ѧ, false = �Ѻ����
    private bool isRunning = true; // ��� Timer �ӧҹ
    public TextMeshProUGUI timerText; // Text ������ʴ���

    void Update()
    {
        if (isRunning)
        {
            if (isCountdown)
            {
                // �Ѻ�����ѧ
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                }
                else
                {
                    timeRemaining = 0;
                    isRunning = false; // ��ش�Ѻ��������Ͷ֧�ٹ��
                }
            }
            else
            {
                // �Ѻ����
                timeRemaining += Time.deltaTime;
            }

            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartTimer() => isRunning = true;
    public void StopTimer() => isRunning = false;
    public void ResetTimer(float newTime)
    {
        timeRemaining = newTime;
        isRunning = true;
    }
}
