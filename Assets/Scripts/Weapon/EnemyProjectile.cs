using UnityEngine;

/// <summary>
/// 적이 발사하는 투사체를 이동시키고,
/// 플레이어와 충돌했을 때 데미지를 주는 역할.
/// </summary>
public class EnemyProjectile : MonoBehaviour
{
    // 투사체가 이동할 방향.
    [SerializeField] private Vector2 moveDirection = Vector2.zero;

    [SerializeField] private float moveSpeed = 6.0f;

    [SerializeField] private float lifeTime = 3.0f;

    [SerializeField] private int damageAmount = 1;

    // 투사체가 초기화 됐는지 여부.
    [SerializeField] private bool isInitialized = false;

    public void Initialize(Vector2 direction, float speed, float duration, int damage)
    {
        moveDirection = direction.normalized;
        moveSpeed = speed;
        lifeTime = duration;
        damageAmount = damage;

        isInitialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isInitialized == false)
        {
            return;
        }

        Move();
        UpdateLifeTime();
    }

    void Move()
    {
        Vector3 moveOffset = new Vector3(moveDirection.x, moveDirection.y, 0.0f) * moveSpeed * Time.deltaTime;
        transform.position += moveOffset;
    }

    void UpdateLifeTime()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

        if(playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);

            Destroy(gameObject);
        }
    }
}
