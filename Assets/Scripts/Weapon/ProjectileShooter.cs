using Unity.VisualScripting;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [Header("공격 주기 설정")]
    [SerializeField] private float attackInterval = 1.0f;

    [Header("발사 참조 설정")]
    [SerializeField] private GameObject projectilePrefab = null;
    [SerializeField] private Transform attackPoint = null;
    [SerializeField] private PlayerMovementState playerMovementState;

    [Header("투사체 기본 설정")]
    [SerializeField] private float projectileSpeed = 8.0f;
    [SerializeField] private float projectileLifetime = 2.0f;

    [Header("디버그 확인용")]
    [SerializeField] private float attackTimer = 0.0f;
    [SerializeField] private int totalShotCount = 0;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(projectilePrefab == null || playerMovementState == null)
        {
            return;
        }

        attackTimer += Time.deltaTime;

        if(attackTimer >= attackInterval)
        {
            FireProjectile();
            attackTimer -= attackInterval;
        }
    }

    void FireProjectile()
    {
        Vector2 fireDirection = playerMovementState.GetLastMoveDirection();
        if(fireDirection == Vector2.zero)
        {
            fireDirection = Vector2.right;
        }

        GameObject spawnedProjectile = Instantiate(projectilePrefab, attackPoint.position, Quaternion.identity);
        if(spawnedProjectile != null)
        {
            Projectile projectile = spawnedProjectile.GetComponent<Projectile>();
            if(projectile != null)
            {
                projectile.Initialize(fireDirection, projectileSpeed, projectileLifetime);
                ++totalShotCount;
            }
        }
    }

    public float GetAttackInterval()
    {
        return attackInterval;
    }

    public void SetAttackInterval(float newAttackInterval)
    {
        attackInterval = newAttackInterval;
    }

    public float GetProjectileSpeed()
    {
        return projectileSpeed;
    }

    public void SetProjectileSpeed(float newProjectileSpeed)
    {
        projectileSpeed = newProjectileSpeed;
    }
}
