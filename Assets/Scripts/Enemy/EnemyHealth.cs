using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 적의 체력을 관리하고 데미지를 처리하는 역할.
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    [Header("체력 설정")]
    [SerializeField] private int maximumHealth = 3;
    [SerializeField] private int currentHealth = 0;

    [Header("상태 확인용")]
    [SerializeField] private bool isDead = false;

    [Header("드랍 설정")]
    [SerializeField] private GameObject experiencePickupPrefab;

    [SerializeField] private EnemyFeedbackController enemyFeedbackController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maximumHealth;
    }

    public void TakeDamage(int damageAmount, float hitFeedbackStrength)
    {
        if(isDead == true)
        {
            return;
        }

        currentHealth -= damageAmount;

        enemyFeedbackController.PlayHitFeedback(hitFeedbackStrength);

        Debug.Log("적 피격, 받은 데미지 = " + damageAmount + " 현재 체력 : " + currentHealth);

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    void Die()
    {
        if(isDead == true)
        {
            return;
        }

        isDead = true;

        // 경험치 드랍, 사망 이펙트, 사운드 재생.
        DropExperiencePickup();

        if(enemyFeedbackController != null)
        {
            enemyFeedbackController.PlayDeathFeedbackAndDestroy();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void DropExperiencePickup()
    {
        Instantiate(experiencePickupPrefab, transform.position, Quaternion.identity);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public bool IsDead()
    {
        return isDead;
    }
}
