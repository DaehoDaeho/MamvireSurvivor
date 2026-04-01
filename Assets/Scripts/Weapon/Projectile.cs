using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("런타임 상태")]
    [SerializeField] private Vector2 moveDirection = Vector2.zero;
    [SerializeField] private float moveSpeed = 0.0f;
    [SerializeField] private float remainingLifetime = 0.0f;
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private bool isInitialized = false;

    [Header("충돌 판별 설정")]
    [SerializeField] private string enemyTag = "Enemy";

    // Update is called once per frame
    void Update()
    {
        if(isInitialized == false)
        {
            return;
        }

        MoveProjectile();
        UpdateLifetime();
    }

    public void Initialize(Vector2 direction, float speed, float lifetime)
    {
        if(direction.magnitude > 0.0f)
        {
            moveDirection = direction.normalized;
        }
        else
        {
            moveDirection = Vector2.right;
        }

        moveSpeed = speed;
        remainingLifetime = lifetime;
        isInitialized = true;
    }

    void MoveProjectile()
    {
        Vector3 moveOffset = new Vector3(moveDirection.x, moveDirection.y, 0.0f) * moveSpeed * Time.deltaTime;
        transform.position += moveOffset;
    }

    void UpdateLifetime()
    {
        remainingLifetime -= Time.deltaTime;
        if(remainingLifetime < 0.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(enemyTag) == false)
        {
            return;
        }

        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
        if(enemyHealth != null)
        {
            Debug.Log("적 명중 완료!!!");

            enemyHealth.TakeDamage(damageAmount);

            Destroy(gameObject);
        }
    }
}
