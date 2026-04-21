using UnityEngine;

/// <summary>
/// 뒈이브 데이터를 배열로 관리하고, 현재 웨이브에 맞는 설정을 찾아 적용하는 역할.
/// </summary>
public class WaveTableController : MonoBehaviour
{
    [SerializeField] private GameProgressController gameProgressController;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private WaveData[] waveTable;

    [SerializeField] private int lastsAppliedWave = -1;

    // Update is called once per frame
    void Update()
    {
        if(gameProgressController == null || enemySpawner == null)
        {
            return;
        }

        int currentWave = gameProgressController.GetCurrentWave();

        if(currentWave == lastsAppliedWave)
        {
            return;
        }

        ApplyWaveData(currentWave);
    }

    /// <summary>
    /// 현재 웨이브 번호에 맞는 WaveData를 찾아 EnemySpawner에 적용하는 함수.
    /// </summary>
    /// <param name="currentWave">현재 웨이브 번호</param>
    void ApplyWaveData(int currentWave)
    {
        WaveData matchedWaveData = FindWaveData(currentWave);

        if(matchedWaveData == null)
        {
            return;
        }

        lastsAppliedWave = currentWave;

        enemySpawner.SetSpawnInterval(matchedWaveData.spawnInterval);
        enemySpawner.SetSpawnCountPerCycle(matchedWaveData.spawnCountPerCycle);
    }

    /// <summary>
    /// 지정된 웨이브 번호와 일치하는 WaveData를 배열에서 찾아 반환하는 함수.
    /// </summary>
    /// <param name="targetWave">찾고 싶은 웨이브 번호</param>
    /// <returns>일치하는 웨이브 데이터</returns>
    WaveData FindWaveData(int targetWave)
    {
        if(waveTable == null)
        {
            return null;
        }

        for(int i=0; i<waveTable.Length; ++i)
        {
            WaveData currentWaveData = waveTable[i];

            if(currentWaveData == null)
            {
                continue;
            }

            if(currentWaveData.waveNumber == targetWave)
            {
                return currentWaveData;
            }
        }

        return null;
    }
}
