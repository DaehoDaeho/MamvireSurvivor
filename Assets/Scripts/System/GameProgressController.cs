using UnityEngine;

/// <summary>
/// 게임의 생존 시간과 웨이브 진행을 관리하는 역할.
/// </summary>
public class GameProgressController : MonoBehaviour
{
    [Header("진행 상태")]
    [SerializeField] private float survivalTime = 0.0f; // 현재까지 생존한 시간.
    [SerializeField] private int currentWave = 1;   // 현재 웨이브 번호.

    [Header("웨이브 설정")]
    [SerializeField] private float waveDuration = 30.0f;    // 한 웨이브당 길이.

    [Header("참조 설정")]
    [SerializeField] private EnemySpawner enemySpawner;

    void Start()
    {
        ApplyWaveIfNeeded(true);
    }

    // Update is called once per frame
    void Update()
    {
        survivalTime += Time.deltaTime;
        ApplyWaveIfNeeded(false);
    }

    /// <summary>
    /// 현재 시간 기준으로 계산된 웨이브가 기존 웨이브와 다를 때,
    /// EnemySpawner에 난이도 반영을 요청.
    /// </summary>
    /// <param name="forceApply">강제로 현재 웨이브를 다시 적용할지 여부</param>
    void ApplyWaveIfNeeded(bool forceApply)
    {
        // Mathf.FloorToInt 함수 : 내림해서 int 값을 반환해주는 함수.
        int calculatedWave = 1 + Mathf.FloorToInt(survivalTime / waveDuration);

        if(forceApply == false && calculatedWave == currentWave)
        {
            return;
        }

        // 웨이브 변경.
        currentWave = calculatedWave;

        if(enemySpawner != null)
        {
            enemySpawner.ApplyDifficulty(currentWave);
        }
    }

    public float GetSurvivalTime()
    {
        return survivalTime;
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }
}
