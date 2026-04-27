using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 무기 패턴에 따라 발사 방향 목록을 계산하는 역할.
/// </summary>
public static class ProjectilePatternCalculator
{
    /// <summary>
    /// 현재 무기 패턴에 맞는 발사 방향을 계산해 반환.
    /// </summary>
    /// <param name="weaponData">현재 무기 데이터</param>
    /// <param name="baseDirection">기준 방향</param>
    /// <returns></returns>
    public static List<Vector2> GetDirections(WeaponData weaponData, Vector2 baseDirection)
    {
        // 배열 : 개수가 정해진 메모리 구조 -> 정해진 개수의 데이터만 담을 수 있다.
        // List(동적 배열) : 개수를 동적으로 변경 가능 -> 데이터의 개수가 바뀔 수 있다->데이터를 추가 / 삭제 가능.
        List<Vector2> directions = new List<Vector2>();

        if(weaponData.patternType == WeaponPatternType.Straight)
        {
            // 인자로 받은 방향 벡터를 정규화.
            Vector2 safeNormalizedDirection = GetSafeNormalilzedDirection(baseDirection);

            // 정규화한 방향 벡터를 리스트에 추가하고 반환.
            directions.Add(safeNormalizedDirection);
            return directions;
        }

        if(weaponData.patternType == WeaponPatternType.Spread)
        {
            return GetSpreadDirectioins(weaponData, baseDirection);
        }

        if(weaponData.patternType == WeaponPatternType.Circle)
        {
            return GetCircleDirections(weaponData);
        }

        directions.Add(GetSafeNormalilzedDirection(baseDirection));
        return directions;
    }

    /// <summary>
    /// 방향 벡터가 0이면 기본 오른쪽 방향을 반환하고,
    /// 아니면 정규화된 방향을 반환.
    /// 정규화 : 벡터의 길이를 1로 만들어 순수한 방향 정보를 뽑아내는 것.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private static Vector2 GetSafeNormalilzedDirection(Vector2 direction)
    {
        // 방향 벡터가 0이면 기본 오른쪽 방향을 반환.
        if(direction == Vector2.zero)
        {
            return Vector2.right;
        }

        // 정규화된 방향 벡터를 반환.
        return direction.normalized;
    }

    /// <summary>
    /// 산탄형 발사 방향 목록을 계산.
    /// </summary>
    /// <param name="weaponData">현재 무기 데이터</param>
    /// <param name="baseDirection">기준 방향</param>
    /// <returns></returns>
    private static List<Vector2> GetSpreadDirectioins(WeaponData weaponData, Vector2 baseDirection)
    {
        List<Vector2> directions = new List<Vector2>();
        int projectileCount = weaponData.projectileCount;
        float angleStep = weaponData.spreadAngleStep;
        float centerOffset = (projectileCount - 1) * 0.5f;

        Vector2 safeBaseDirection = GetSafeNormalilzedDirection(baseDirection);

        for(int i=0; i<projectileCount; ++i)
        {
            float angleOffset = (i - centerOffset) * angleStep;
            // 회전 방향 계산.
            Vector2 rotatedDirection = RotateDirection(safeBaseDirection, angleOffset);
            directions.Add(rotatedDirection);
        }

        return directions;
    }

    /// <summary>
    /// 방향 벡터를 지정한 각도만큼 회전.
    /// </summary>
    /// <param name="direction">원래 방향</param>
    /// <param name="angle">회전 각도</param>
    /// <returns>회전된 방향</returns>
    private static Vector2 RotateDirection(Vector2 direction, float angle)
    {
        // 인자로 넘어온 각도값을 이용해서 회전값을 생성.
        Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, angle);

        // 회전 방향 계산.
        Vector2 rotateDirection = rotation * direction;

        // 정규화한 회전 방향을 반환.
        return rotateDirection.normalized;
    }

    /// <summary>
    /// 원형 발사 방향 목록을 계산
    /// </summary>
    /// <param name="weaponData">현재 무기 데이터</param>
    /// <returns>원형 발사 방향 목록</returns>
    private static List<Vector2> GetCircleDirections(WeaponData weaponData)
    {
        List<Vector2> directions = new List<Vector2>();

        int projectileCount = weaponData.projectileCount;
        float angleStep = 360.0f / projectileCount;

        for(int i=0; i<projectileCount; ++i)
        {
            float currentAngle = angleStep * i;
            Vector2 rotatedDirection = RotateDirection(Vector2.right, currentAngle);

            directions.Add(rotatedDirection);
        }

        return directions;
    }
}
