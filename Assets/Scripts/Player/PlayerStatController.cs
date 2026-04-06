using UnityEngine;

/// <summary>
/// 플레이어 레벨업 결과를 실제 능력치 변화로 적용하는 역할.
/// </summary>
public class PlayerStatController : MonoBehaviour
{
    [Header("참조 설정")]
    [SerializeField] private PlayerMovementState playerMovementState;
    [SerializeField] private ProjectileShooter projectileShooter;

    [Header("레벨업 당 증가량 설정")]
    [SerializeField] private float moveSpeedIncreasePerLevel = 0.5f;
    [SerializeField] private float attackIntervalDecreasePerLevel = 0.1f;
    [SerializeField] private float projectileSpeedIncreasePerLevel = 1.0f;

    [Header("안전 제한 설정")]
    [SerializeField] private float minimumAttackInterval = 0.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(playerMovementState == null)
        {
            playerMovementState = GetComponent<PlayerMovementState>();
        }

        if(projectileShooter == null)
        {
            projectileShooter = GetComponent<ProjectileShooter>();
        }
    }

    public void ApplyLevelUpBonus()
    {
        if(playerMovementState != null)
        {
            float currentMoveSpeed = playerMovementState.GetMoveSpeed();
            float newMoveSpeed = currentMoveSpeed + moveSpeedIncreasePerLevel;
            playerMovementState.SetMoveSpeed(newMoveSpeed);
        }

        if(projectileShooter != null)
        {
            float currentAttackInterval = projectileShooter.GetAttackInterval();
            float newAttackInterval = currentAttackInterval - attackIntervalDecreasePerLevel;

            if(newAttackInterval < minimumAttackInterval)
            {
                newAttackInterval = minimumAttackInterval;
            }

            projectileShooter.SetAttackInterval(newAttackInterval);

            float currentProjectileSpeed = projectileShooter.GetProjectileSpeed();
            float newProjectileSpeed = currentProjectileSpeed + projectileSpeedIncreasePerLevel;
            projectileShooter.SetProjectileSpeed(newProjectileSpeed);
        }
    }
}
