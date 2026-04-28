using UnityEngine;
using System.Collections.Generic;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField] private WeaponController weaponController;

    [Header("발사 참조 설정")]    
    [SerializeField] private Transform attackPoint = null;
    [SerializeField] private PlayerMovementState playerMovementState;

    [Header("디버그 확인용")]
    [SerializeField] private float attackTimer = 0.0f;
    [SerializeField] private int totalShotCount = 0;

    [SerializeField] private GameStateController gameStateController;

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
        if (gameStateController != null && gameStateController.IsPlaying() == false)
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
            //FirePattern(currentWeapon);
            FireCurrentWeapon(currentWeapon);
            attackTimer -= currentWeapon.attackInterval;
        }
    }

    WeaponData GetCurrentWeapon()
    {
        //if(weaponController == null)
        //{
        //    return null;
        //}

        return weaponController.GetCurrentWeapon();
    }

    /// <summary>
    /// 현재 무기의 발사 흐름 전체를 수행.
    /// </summary>
    /// <param name="currentWeapon">현재 장착한 무기</param>
    void FireCurrentWeapon(WeaponData currentWeapon)
    {
        Vector2 baseDirection = GetBaseFireDirection();

        PlayFireFeedback(currentWeapon);

        // 발사 방향 목록을 계산해서 저장.
        List<Vector2> fireDirections = ProjectilePatternCalculator.GetDirections(currentWeapon, baseDirection);

        // 총알 발사.
        SpawnProjectiles(currentWeapon, fireDirections);
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
    /// 방향 목록을 순회하면서 투사체를 생성.
    /// </summary>
    /// <param name="currentWeapon">현재 장착 무기 데이터</param>
    /// <param name="fireDirections">발사 방향 목록</param>
    void SpawnProjectiles(WeaponData currentWeapon, List<Vector2> fireDirections)
    {
        for(int i=0; i<fireDirections.Count; ++i)
        {
            SpawnProjectile(currentWeapon, fireDirections[i]);
        }
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
            //GameObject fireEffectObject = Instantiate(currentWeapon.fireEffectPrefab, attackPoint.position, Quaternion.identity);
            //Destroy(fireEffectObject, 0.5f);
            EffectSpawnUtility.SpawnEffect(currentWeapon.fireEffectPrefab, attackPoint.position);
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
