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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maximumHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if(isDead == true)
        {
            return;
        }

        currentHealth -= damageAmount;

        Debug.Log("적 피격, 받은 데미지 = " + damageAmount + " 현재 체력 : " + currentHealth);

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    void Die()
    {
        isDead = true;
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
