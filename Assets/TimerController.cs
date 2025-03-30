using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerController : MonoBehaviour
{
    public float timeRemaining = 60f; // ตั้งเวลาเริ่มต้น (60 วินาที)
    public bool isCountdown = true; // true = นับถอยหลัง, false = นับเพิ่ม
    private bool isRunning = true; // ให้ Timer ทำงาน
    public TextMeshProUGUI timerText; // Text ที่ใช้แสดงผล

    void Update()
    {
        if (isRunning)
        {
            if (isCountdown)
            {
                // นับถอยหลัง
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                }
                else
                {
                    timeRemaining = 0;
                    isRunning = false; // หยุดจับเวลาเมื่อถึงศูนย์
                }
            }
            else
            {
                // นับเพิ่ม
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
