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
            FireProjectile(currentWeapon);
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
