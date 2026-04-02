using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 플레이어의 경험치를 관리하는 클래스.
/// </summary>
public class PlayerExperience : MonoBehaviour
{
    [Header("경험치 상태")]
    [SerializeField] private int currentExperience = 0;

    public void AddExperience(int amount)
    {
        currentExperience += amount;

        Debug.Log("경험치 획득 = " + amount + ", 현재 경험치 = " + currentExperience);
    }

    public int GetCurrentExperience()
    {
        return currentExperience;
    }
}
