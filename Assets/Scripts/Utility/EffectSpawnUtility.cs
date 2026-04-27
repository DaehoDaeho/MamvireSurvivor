using UnityEngine;

/// <summary>
/// 이펙트 프리팹을 지정 위치에 생성하는 공용 유틸리티 역할.
/// </summary>
public static class EffectSpawnUtility
{
    public static GameObject SpawnEffect(GameObject effectPrefab, Vector3 position)
    {
        GameObject spawnedEffect = Object.Instantiate(effectPrefab, position, Quaternion.identity);
        return spawnedEffect;
    }
}
