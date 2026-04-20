using UnityEngine;

/// <summary>
/// 이펙트 프리팹이 일정 시간이 지난 후 자동으로 파괴되도록 관리하는 역할
/// </summary>
public class AutoDestroyEffect : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, destroyDelay);   
    }
}
