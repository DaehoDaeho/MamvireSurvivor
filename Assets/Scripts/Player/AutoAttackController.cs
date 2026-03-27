using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 일정 시간마다 자동으로 공격 이벤트를 실행.
/// </summary>
public class AutoAttackController : MonoBehaviour
{
    [Header("공격 주기 설정")]
    [SerializeField] private float attackInterval = 1.0f;

    [Header("공격 위치 설정")]
    [SerializeField] private Transform attackPoint;

    [Header("테스트용 표시 설정")]
    [SerializeField] private GameObject attackIndicatorPrefab;
    [SerializeField] private float indicatorLifetime = 0.2f;

    [Header("디버그 확인용")]
    [SerializeField] private float attackTimer = 0.0f;
    [SerializeField] private int totalAttackCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(attackPoint == null)
        {
            attackPoint = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAttackTimer();
    }

    void UpdateAttackTimer()
    {
        attackTimer += Time.deltaTime;

        if(attackTimer >= attackInterval)
        {
            PerformAttack();
            attackTimer -= attackInterval;
        }
    }

    void PerformAttack()
    {
        ++totalAttackCount;

        Vector3 attackPosition = attackPoint.position;

        Debug.Log("자동 공격 실행: 공격 회수 = " + totalAttackCount + ", 공격 위치 = " + attackPosition);

        if(attackIndicatorPrefab != null)
        {
            GameObject go = Instantiate(attackIndicatorPrefab, attackPosition, Quaternion.identity);
            Destroy(go, indicatorLifetime);
        }
    }

    public float GetAttackInterval()
    {
        return attackInterval;
    }

    public int GetTotalAttackCount()
    {
        return totalAttackCount;
    }
}
