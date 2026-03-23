using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 체력 관리, 데미지 처리, 무적 시간 관리, 사망 상태 처리.
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    [Header("체력 설정")]
    [SerializeField] private int maximumHealth = 5;
    [SerializeField] private int currentHealth = 0;

    [Header("피격 및 무적 설정")]
    [SerializeField] private float invincibilityDuration = 0.5f;    // 무적 상태 총 지속시간.
    [SerializeField] private bool isInvincible = false; // 현재 무적 상태인지 여부.
    [SerializeField] private float currentInvincibilityTime = 0.0f; // 현재 남아있는 무적 시간.

    [Header("상태 확인용")]
    [SerializeField] private bool isDead = false;  // 사망 여부.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maximumHealth;
        Debug.Log("플레이어 체력 초기화 완료: " + currentHealth + "/" + maximumHealth);
    }

    // Update is called once per frame
    void Update()
    {
        // 무적 상태 타이머 감소 처리.
        UpdateInvincibilityTimer();

        // 키 입력으로 강제 데미지 적용.
        HandleDamageTestInput();
    }

    /// <summary>
    /// 무적 상태일때 남은 무적 시간을 감소시키고,
    /// 시간이 끝나면 무적 상태를 해제.
    /// </summary>
    void UpdateInvincibilityTimer()
    {
        if(isInvincible == false)
        {
            return;
        }

        // 남은 무적 시간을 프레임 시간만큼 감소시킨다.
        currentInvincibilityTime -= Time.deltaTime;

        if(currentInvincibilityTime <= 0.0f)
        {
            isInvincible = false;
            currentInvincibilityTime = 0.0f;

            Debug.Log("무적 상태 종료");
        }
    }

    /// <summary>
    /// 테스트용 입력을 받아 강제로 데미지 적용.
    /// 키보드 H 키를 누르면 데미지 적용.
    /// </summary>
    void HandleDamageTestInput()
    {
        if(Input.GetKeyDown(KeyCode.H) == true)
        {
            // 데미지 적용.
            TakeDamage(1);
        }
    }

    /// <summary>
    /// 플레이어에게 데미지 적용.
    /// </summary>
    /// <param name="damageAmount">데미지 양</param>
    public void TakeDamage(int damageAmount)
    {
        if(isDead == true)
        {
            Debug.Log("이미 사망한 상태이므로 데미지를 무시");
            return;
        }

        if(isInvincible == true)
        {
            Debug.Log("무적 상태이므로 데미지를 무시");
            return;
        }

        currentHealth -= damageAmount;
        Debug.Log("플레이어 피격: 받은 데미지 = " + damageAmount + ", 현재 체력 = " + currentHealth);

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            // 사망 처리.
            Die();
        }
        else
        {
            // 무적 상태 시작.
            StartInvincibility();
        }
    }

    void Die()
    {
        isDead = true;

        Debug.Log("플레이어 사망 처리");
    }

    void StartInvincibility()
    {
        isInvincible = true;
        currentInvincibilityTime = invincibilityDuration;
        Debug.Log("무적 상태 시작, 지속 시간 = " + invincibilityDuration);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaximumHealth()
    {
        return maximumHealth;
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }

    public bool IsDead()
    {
        return isDead;
    }
}
