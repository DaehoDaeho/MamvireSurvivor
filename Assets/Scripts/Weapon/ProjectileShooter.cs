using Unity.VisualScripting;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField] private WeaponController weaponController;

    [Header("발사 참조 설정")]    
    [SerializeField] private Transform attackPoint = null;
    [SerializeField] private PlayerMovementState playerMovementState;

    [Header("디버그 확인용")]
    [SerializeField] private float attackTimer = 0.0f;
    [SerializeField] private int totalShotCount = 0;

    [SerializeField] private GameManager gameManager;

    [SerializeField] private AudioSource fireAudioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(attackPoint == null)
        {
            attackPoint = transform;
        }

        if(playerMovementState == null)
        {
            playerMovementState = GetComponent<PlayerMovementState>();
        }

        if(fireAudioSource == null)
        {
            fireAudioSource = GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager != null && gameManager.IsPlaying() == false)
        {
            return;
        }

        WeaponData currentWeapon = GetCurrentWeapon();

        if(currentWeapon == null)
        {
            return;
        }

        attackTimer += Time.deltaTime;

        if(attackTimer >= currentWeapon.attackInterval)
        {
            //FireProjectile(currentWeapon);
            FirePattern(currentWeapon);
            attackTimer -= currentWeapon.attackInterval;
        }
    }

    WeaponData GetCurrentWeapon()
    {
        if(weaponController == null)
        {
            return null;
        }

        return weaponController.GetCurrentWeapon();
    }

    void FireProjectile(WeaponData currentWeapon)
    {
        if(currentWeapon.projectilePrefab == null)
        {
            return;
        }

        Vector2 fireDirection = playerMovementState.GetLastMoveDirection();
        if(fireDirection == Vector2.zero)
        {
            fireDirection = Vector2.right;
        }

        GameObject spawnedProjectile = Instantiate(currentWeapon.projectilePrefab, attackPoint.position, Quaternion.identity);
        if(spawnedProjectile != null)
        {
            Projectile projectile = spawnedProjectile.GetComponent<Projectile>();
            if(projectile != null)
            {
                projectile.Initialize(fireDirection, currentWeapon.projectileSpeed, currentWeapon.projectileLifetime,
                    currentWeapon.projectileDamage, currentWeapon.hitFeedbackStrength);
                ++totalShotCount;
            }
        }

        // 사운드 재생.
        PlayFireFeedback(currentWeapon);
    }

    void FirePattern(WeaponData currentWeapon)
    {
        PlayFireFeedback(currentWeapon);

        if(currentWeapon.patternType == WeaponPatternType.Straight)
        {
            FireStraightPattern(currentWeapon);
        }
        else if(currentWeapon.patternType == WeaponPatternType.Spread)
        {
            FireSpreadPattern(currentWeapon);
        }
        else if(currentWeapon.patternType == WeaponPatternType.Circle)
        {
            FireCirclePattern(currentWeapon);
        }
    }

    /// <summary>
    /// 직선형 패턴으로 한 방향에 투사체를 발사.
    /// </summary>
    /// <param name="currentWeapon">현재 장착 무기 데이터</param>
    void FireStraightPattern(WeaponData currentWeapon)
    {
        Vector2 fireDirection = GetBaseFireDirection();
        SpawnProjectile(currentWeapon, fireDirection);
    }

    /// <summary>
    /// 산탄형 패턴으로 여러 방향에 투사체를 퍼뜨려 발사.
    /// </summary>
    /// <param name="currentWeapon">현재 장착 무기 데이터</param>
    void FireSpreadPattern(WeaponData currentWeapon)
    {
        Vector2 baseDirection = GetBaseFireDirection();

        int projectileCount = currentWeapon.projectileCount;
        float angleStep = currentWeapon.spreadAngleStep;

        float centerOffset = (projectileCount - 1) * 0.5f;

        for(int i=0; i<projectileCount; ++i)
        {
            float angleOffset = (i - centerOffset) * angleStep;
            Vector2 spreadDirection = RotateDirection(baseDirection, angleOffset);

            SpawnProjectile(currentWeapon, spreadDirection);
        }
    }

    /// <summary>
    /// 원형 패턴으로 360도 방향에 균등하게 투사체를 발사.
    /// </summary>
    /// <param name="currentWeapon">현재 장착 무기 데이터</param>
    void FireCirclePattern(WeaponData currentWeapon)
    {
        int projectileCount = currentWeapon.projectileCount;
        float angleStep = 360.0f / projectileCount;

        for(int i=0; i<projectileCount; ++i)
        {
            float angle = angleStep * i;
            Vector2 circleDirection = RotateDirection(Vector2.right, angle);

            SpawnProjectile(currentWeapon, circleDirection);
        }
    }

    /// <summary>
    /// 현재 기준 발사 방향을 반환.
    /// </summary>
    /// <returns>기준 발사 방향</returns>
    Vector2 GetBaseFireDirection()
    {
        Vector2 fireDirection = playerMovementState.GetLastMoveDirection();

        if(fireDirection == Vector2.zero)
        {
            fireDirection = Vector2.right;
        }

        return fireDirection.normalized;
    }

    /// <summary>
    /// 방향 벡터를 지정한 각도만큼 회전시켜 새 방향을 반환.
    /// </summary>
    /// <param name="direction">원래 방향</param>
    /// <param name="angle">회전 각도</param>
    /// <returns>회전된 방향 벡터</returns>
    Vector2 RotateDirection(Vector2 direction, float angle)
    {
        // z 축 기준으로 angle 각도만큼 회전하는 2D 회전값을 만드는 처리.
        Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, angle);

        // 회전을 벡터에 적용하는 방식
        // 기존 방향 벡터를 회전시킨 새 방향을 구하는 처리.
        Vector2 rotateDirection = rotation * direction;

        return rotateDirection.normalized;
    }

    /// <summary>
    /// 현재 무기 설정과 방향을 사용해 투사체를 생성.
    /// </summary>
    /// <param name="currentWeapon">현재 장착 무기 데이터</param>
    /// <param name="direction">발사 방향</param>
    void SpawnProjectile(WeaponData currentWeapon, Vector2 direction)
    {
        GameObject spawnProjectile = Instantiate(currentWeapon.projectilePrefab, attackPoint.position, Quaternion.identity);

        Projectile projectile = spawnProjectile.GetComponent<Projectile>();
        projectile.Initialize(direction, currentWeapon.projectileSpeed, currentWeapon.projectileLifetime,
            currentWeapon.projectileDamage, currentWeapon.hitFeedbackStrength);
    }

    void PlayFireFeedback(WeaponData currentWeapon)
    {
        if(currentWeapon == null)
        {
            return;
        }

        if(fireAudioSource != null && currentWeapon.fireSound != null)
        {
            // 사운드 재생 함수.
            fireAudioSource.PlayOneShot(currentWeapon.fireSound);
        }

        if(currentWeapon.fireEffectPrefab != null)
        {
            GameObject fireEffectObject = Instantiate(currentWeapon.fireEffectPrefab, attackPoint.position, Quaternion.identity);
            //Destroy(fireEffectObject, 0.5f);
        }
    }

    public float GetAttackInterval()
    {
        WeaponData currentWeapon = GetCurrentWeapon();

        if(currentWeapon == null)
        {
            return 0.0f;
        }

        return currentWeapon.attackInterval;
    }

    public void SetAttackInterval(float newAttackInterval)
    {
        WeaponData currentWeapon = GetCurrentWeapon();

        if (currentWeapon == null)
        {
            return;
        }

        currentWeapon.attackInterval = newAttackInterval;
    }

    public float GetProjectileSpeed()
    {
        WeaponData currentWeapon = GetCurrentWeapon();

        if (currentWeapon == null)
        {
            return 0.0f;
        }

        return currentWeapon.projectileSpeed;
    }

    public void SetProjectileSpeed(float newProjectileSpeed)
    {
        WeaponData currentWeapon = GetCurrentWeapon();

        if (currentWeapon == null)
        {
            return;
        }

        currentWeapon.projectileSpeed = newProjectileSpeed;
    }
}
