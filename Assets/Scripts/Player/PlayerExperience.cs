using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 플레이어의 경험치를 관리하는 클래스.
/// </summary>
public class PlayerExperience : MonoBehaviour
{
    [Header("레벨 상태")]
    [SerializeField] private int currentLevel = 1;

    [Header("경험치 상태")]
    [SerializeField] private int currentExperience = 0;
    [SerializeField] private int requiredExperience = 5;

    [Header("증가 규칙 설정")]
    [SerializeField] private int requiredExperienceIncrease = 2;

    [Header("UI 참조")]
    [SerializeField] private LevelUpSelectionUI levelUpSelectionUI;

    [SerializeField] private GameManager gameManager;

    public void AddExperience(int amount)
    {
        currentExperience += amount;

        Debug.Log("경험치 획득 = " + amount + ", 현재 경험치 = " + currentExperience);

        // 레벨업 체크.
        TryLevelUp();
    }

    void TryLevelUp()
    {
        // 현재 경험치가 필요 경험치 이상인 동안 계속 레벨업 시도.
        while(currentExperience >= requiredExperience)
        {
            // 계속 레벨업 처리.

            // 현재 레벨 구간에서 필요했던 경험치만 차감.
            currentExperience -= requiredExperience;

            LevelUp();
        }
    }

    void LevelUp()
    {
        ++currentLevel;

        requiredExperience += requiredExperienceIncrease;

        Debug.Log("현재 레벨 : " + currentLevel + ", 남은 경험치 : " + currentExperience +
            ", 다음 필요 경험치 : " + requiredExperience);

        //if(levelUpSelectionUI != null)
        //{
        //    levelUpSelectionUI.Show();
        //}
        if(gameManager != null)
        {
            gameManager.EnterLevelUpSelection();
        }
    }

    public int GetCurrentExperience()
    {
        return currentExperience;
    }

    public int GetRequiredExperience()
    {
        return requiredExperience;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}
