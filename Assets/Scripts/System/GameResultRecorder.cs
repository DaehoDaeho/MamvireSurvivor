using UnityEngine;

/// <summary>
/// 게임 종료 시 필요한 결과 데이터를 모아서 GameResultData에 기록하는 역할.
/// </summary>
public class GameResultRecorder : MonoBehaviour
{
    [SerializeField] private GameProgressController gameProgressController;
    [SerializeField] private PlayerExperience playerExperience;
    [SerializeField] private WeaponController weaponController;

    /// <summary>
    /// 현재 플레이 결과를 GameResultData에 저장.
    /// </summary>
    public void RecordCurrentResult()
    {
        if(gameProgressController != null)
        {
            GameResultData.lastSurviveTime = gameProgressController.GetSurvivalTime();
        }

        if(playerExperience != null)
        {
            GameResultData.lastLevel = playerExperience.GetCurrentLevel();
        }

        if(weaponController != null)
        {
            WeaponData currentWeapon = weaponController.GetCurrentWeapon();
            GameResultData.lastWeaponName = currentWeapon.weaponName;
        }
    }
}
