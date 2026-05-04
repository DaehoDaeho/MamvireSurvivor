using UnityEngine;

/// <summary>
/// 플레이어 주변 범위 내에서 가장 가까운 적을 찾는 역할
/// </summary>
public class EnemyTargetFinder : MonoBehaviour
{
    [SerializeField] private float targetRadius = 8.0f;
    [SerializeField] private LayerMask enemyLayer;

    /// <summary>
    /// 일정 범위 안의 가장 가까운 적을 찾아 방향을 반환.
    /// 실제 방향 정보는 함수가 직접 반환하는 것이 아니라 targetDirecion에 담는다.
    /// </summary>
    /// <param name="targetDirection">찾은 적을 향하는 방향 정보를 담을 참조 변수</param>
    /// <returns>적을 찾았는지 여부</returns>
    public bool TryGetNearestTargetDirection(out Vector2 targetDirection)
    {
        targetDirection = Vector2.zero;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, targetRadius, enemyLayer);

        Transform nearestEnemy = null;
        float nearestDistanceSqr = float.MaxValue;

        for(int i=0; i<enemies.Length; ++i)
        {
            Collider2D currentEnemyCollider = enemies[i];

            Vector2 directionToEnemy = currentEnemyCollider.transform.position - transform.position;
            float distanceSqr = directionToEnemy.sqrMagnitude;  // 방향 벡터의 제곱값을 계산.
            if(distanceSqr < nearestDistanceSqr)
            {
                nearestDistanceSqr = distanceSqr;
                nearestEnemy = currentEnemyCollider.transform;
            }
        }

        if(nearestEnemy == null)
        {
            return false;
        }

        Vector2 finalDirection = nearestEnemy.position - transform.position;

        if(finalDirection == Vector2.zero)
        {
            return false;
        }

        // 대상의 최종 방향 계산.
        targetDirection = finalDirection.normalized;

        return true;
    }
}
