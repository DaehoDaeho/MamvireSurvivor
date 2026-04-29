using UnityEngine;

/// <summary>
/// GameBalanceProfile에 저장된 밸런스 배율을 실제 게임 시스템에 적용하는 역할.
/// </summary>
public class BalanceApplier : MonoBehaviour
{
    [SerializeField] private GameBalanceProfile balanceProfile;

    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private PlayerExperience playerExperience;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ApplyBalance();
    }

    /// <summary>
    /// 밸런스 프로파일의 값을 실제 시스템에 적용.
    /// </summary>
    void ApplyBalance()
    {
        ApplyEnemyBalance();
        ApplyWeaponBalance();
        ApplyExperienceBalance();
    }

    void ApplyEnemyBalance()
    {
        float currentSpawnInterval = enemySpawner.GetSpawnInterval();
        int currentSpawnCountPerCycle = enemySpawner.GetSpawnCountPerCycle();

        float adjustedSpawnInterval = currentSpawnInterval * balanceProfile.enemySpawnIntervalMultiplier;

        float adjustedSpawnCountFloat = currentSpawnCountPerCycle * balanceProfile.enemySpawnCountMultiplier;

        int adjustedSpawnCount = Mathf.RoundToInt(adjustedSpawnCountFloat);

        enemySpawner.SetSpawnInterval(adjustedSpawnInterval);
        enemySpawner.SetSpawnCountPerCycle(adjustedSpawnCount);
    }

    void ApplyWeaponBalance()
    {
        WeaponData[] allWeapons = weaponController.GetAllWeapons();

        for(int i=0; i<allWeapons.Length; ++i)
        {
            WeaponData currentWeapon = allWeapons[i];

            int originalDamage = currentWeapon.projectileDamage;
            float originalAttackInterval = currentWeapon.attackInterval;
            float originalProjectileSpeed = currentWeapon.projectileSpeed;

            float adjustedDamageFloat = originalDamage * balanceProfile.weaponDamageMultiplier;
            int adjustesDamage = Mathf.RoundToInt(adjustedDamageFloat);

            float adjustedAttackInterval = originalAttackInterval * balanceProfile.weaponAttackIntervalMultiplier;
            float adjustedProjectileSpeed = originalProjectileSpeed * balanceProfile.projectileSpeedMultiplier;

            currentWeapon.projectileDamage = adjustesDamage;
            currentWeapon.attackInterval = adjustedAttackInterval;
            currentWeapon.projectileSpeed = adjustedProjectileSpeed;
        }
    }

    void ApplyExperienceBalance()
    {
        playerExperience.SetRequiredExperienceMultiplier(balanceProfile.requiredExperienceMultiplier);
    }
}
