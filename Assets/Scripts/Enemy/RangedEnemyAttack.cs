using UnityEngine;

/// <summary>
/// 원거리 적이 일정 주기마다 플레이어 방향으로 투사체를 발사하게 만드는 역할.
/// </summary>
public class RangedEnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject enemyProjectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float attackRange = 10.0f;
    [SerializeField] private float attackInterval = 1.5f;

    [SerializeField] private float projectileSpeed = 6.0f;
    [SerializeField] private float projectileLifeTime = 3.0f;
    [SerializeField] private int projectileDamage = 1;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private float attackTimer = 0.0f;

    private bool canFire = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(firePoint == null)
        {
            firePoint = transform;
        }

        if(playerTransform == null)
        {
            PlayerHealth playerHealth = FindAnyObjectByType<PlayerHealth>();
            if(playerHealth != null)
            {
                playerTransform = playerHealth.transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform == null)
        {
            return;
        }

        CheckCanFireByDistance();

        attackTimer += Time.deltaTime;

        if(attackTimer >= attackInterval)
        {
            TryFire();
            attackTimer = 0.0f;
        }
    }

    void CheckCanFireByDistance()
    {
        Vector2 directioinToPlayer = playerTransform.position - transform.position;
        float distanceToPlayer = directioinToPlayer.magnitude;  // 벡터의 크기를 통해 거리를 구한다.

        if (distanceToPlayer > attackRange)
        {
            canFire = false;
        }
        else
        {
            canFire = true;
        }
    }

    /// <summary>
    /// 플레이어가 공격 범위 안에 있으면 투사체를 발사.
    /// </summary>
    void TryFire()
    {
        Vector2 directioinToPlayer = playerTransform.position - transform.position;
        float distanceToPlayer = directioinToPlayer.magnitude;  // 벡터의 크기를 통해 거리를 구한다.

        if(distanceToPlayer > attackRange)
        {
            return;
        }

        GameObject projectileObject = Instantiate(enemyProjectilePrefab, firePoint.position, Quaternion.identity);

        EnemyProjectile enemyProjectile = projectileObject.GetComponent<EnemyProjectile>();
        if(enemyProjectile != null)
        {
            enemyProjectile.Initialize(directioinToPlayer.normalized, projectileSpeed, projectileLifeTime,
                projectileDamage);
        }
    }

    public bool CanFire()
    {
        return canFire;
    }
}
