using UnityEngine;

/// <summary>
/// 플레이어 레벨업 결과를 실제 능력치 변화로 적용하는 역할.
/// </summary>
public class PlayerStatController : MonoBehaviour
{
    [Header("참조 설정")]
    [SerializeField] private PlayerMovementState playerMovementState;
    [SerializeField] private ProjectileShooter projectileShooter;

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

    public void ApplyMoveSpeedUpgrade(float upgradeValue)
    {
        if (playerMovementState != null)
        {
            float currentMoveSpeed = playerMovementState.GetMoveSpeed();
            float newMoveSpeed = currentMoveSpeed + upgradeValue;
            playerMovementState.SetMoveSpeed(newMoveSpeed);
        }
    }

    public void ApplyAttackSpeedUpgrade(float upgradeValue)
    {
        if(projectileShooter != null)
        {
            float currentAttackInterval = projectileShooter.GetAttackInterval();
            float newAttackInterval = currentAttackInterval - upgradeValue;

            if (newAttackInterval < minimumAttackInterval)
            {
                newAttackInterval = minimumAttackInterval;
            }

            projectileShooter.SetAttackInterval(newAttackInterval);
        }
    }

    public void ApplyProjectileSpeedUpgrade(float upgradeValue)
    {
        if(projectileShooter != null)
        {
            float currentProjectileSpeed = projectileShooter.GetProjectileSpeed();
            float newProjectileSpeed = currentProjectileSpeed + upgradeValue;
            projectileShooter.SetProjectileSpeed(newProjectileSpeed);
        }
    }
}
