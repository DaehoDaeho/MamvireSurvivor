using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

/// <summary>
/// 플레이어와 게임 진행 데이터를 읽어서 체력, 경험치, 레벨, 생존 시간을 UI에 표시하는 역할.
/// </summary>
public class HUDUIController : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerExperience playerExperience;
    [SerializeField] private GameProgressController gameProgressController;

    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text experienceText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text timeText;

    // Update is called once per frame
    void Update()
    {
        UpdateHealthUI();
        UpdateExperienceUI();
        UpdateLevelUI();
        UpdateTimeUI();
    }

    void UpdateHealthUI()
    {
        int currentHealth = playerHealth.GetCurrentHealth();
        int maximumHealth = playerHealth.GetMaximumHealth();

        healthText.text = "HP : " + currentHealth + "/" + maximumHealth;
    }

    void UpdateExperienceUI()
    {
        int currentExperience = playerExperience.GetCurrentExperience();
        int requiredExperience = playerExperience.GetRequiredExperience();

        experienceText.text = "EXP : " + currentExperience + "/" + requiredExperience;
    }

    void UpdateLevelUI()
    {
        int currentLevel = playerExperience.GetCurrentLevel();

        levelText.text = "Lv. " + currentLevel;
    }

    /// <summary>
    /// 시간을 분:초 형태로 표시.
    /// </summary>
    void UpdateTimeUI()
    {
        float survivalTime = gameProgressController.GetSurvivalTime();

        // Mathf.FloorToInt 함수.
        // 인자값을 내림 처리해서 int 형 값으로 반환해주는 함수.
        int totalSeconds = Mathf.FloorToInt(survivalTime);
        int minutes = totalSeconds / 60;    // 분 계산.
        int seconds = totalSeconds % 60;    // 초 계산.

        string minuteText = minutes.ToString("00"); // 두자릿수로 출력.
        string secondText = seconds.ToString("00"); // 두자릿수로 출력.

        timeText.text = minuteText + ":" + secondText;
    }
}
