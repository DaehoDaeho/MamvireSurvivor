using UnityEngine;

/// <summary>
/// 2D 뱀서라이크 게임에서 일정 시간마다 플레이어 주변에 적 프리팹을 생성하는 역할.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [Header("참조 설정")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform playerTransform;

    [Header("스폰 설정")]
    [SerializeField] private float spawnInterval = 2.0f;
    [SerializeField] private float spawnRadius = 6.0f;
    [SerializeField] private int spawnCountPerCycle = 1;

    [Header("웨이브 난이도 설정")]
    [SerializeField] private float spawnIntervalDecreasePerWave = 0.15f;    // 웨이브가 올라갈 때마다 생성 주기를 얼마나 줄일지 저장하는 변수.
    [SerializeField] private float minimumSpawnInterval = 0.3f; // 생성 주기가 너무 낮아지는 것을 막기 위한 최소 생성 주기.
    [SerializeField] private int wavePerAddionalSpawn = 2;  // 웨이브가 몇 단계 올라갈 때마다 한 번에 생성 수를 얼마나 늘릴지 계산하기 위한 기준.

    [Header("기본값 저장용")]
    [SerializeField] private float baseSpawnInterval = 0.0f;    // 초기 생성 주기를 저장하는 변수.
    [SerializeField] private int baseSpawnCountPerCycle = 0;    // 초기 한 번당 생성 수를 저장하는 변수.

    [Header("디버그 확인용")]
    [SerializeField] private float spawnTimer = 0.0f;
    [SerializeField] private int totalSpawnedCount = 0;

    [SerializeField] private GameStateController gameStateController;

    void Awake()
    {
        baseSpawnInterval = spawnInterval;
        baseSpawnCountPerCycle = spawnCountPerCycle;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(playerTransform == null)
        {
            GameObject playerObject = GameObject.Find("Player");
            if(playerObject != null)
            {
                playerTransform = playerObject.transform;
            }
            else
            {
                Debug.LogWarning("Player 오브젝트를 찾지 못했습니다.");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStateController != null && gameStateController.IsPlaying() == false)
        {
            return;
        }

        if(playerTransform == null || enemyPrefab == null)
        {
            return;
        }

        spawnTimer += Time.deltaTime;

        if(spawnTimer >= spawnInterval)
        {
            // 적 스폰 함수 호출.
            SpawnEnemies();

            //spawnTimer = 0.0f;
            spawnTimer -= spawnInterval;
        }
    }

    void SpawnEnemies()
    {
        for(int i=0; i<spawnCountPerCycle; ++i)
        {
            // 스폰 위치 계산.
            Vector3 spawnPosition = CalculateSpawnPosition();

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            ++totalSpawnedCount;
        }
    }

    Vector3 CalculateSpawnPosition()
    {
        // 반지름이 1인 원 내부의 임의의 위치를 나타내는 2D 벡터를 구한다.
        // Random.insideUnitCircle : 반지름이 1인 원 안의 임의의 위치를 반환한다.
        Vector2 randomDirection = Random.insideUnitCircle;

        if(randomDirection == Vector2.zero)
        {
            randomDirection = Vector2.right;
        }

        // 방향만 사용하기 위해 길이를 1로 맞춘다.(정규화)
        randomDirection = randomDirection.normalized;

        // 플레이어 중심에서 일정 거리 떨어진 오프셋을 계산한다.
        Vector2 spawnOffset = randomDirection * spawnRadius;

        // 플레이어의 현재 위치를 가져온다.
        Vector3 playerPosition = playerTransform.position;

        // 최종 생성 위치 계산.
        Vector3 spawnPosition = playerPosition + new Vector3(spawnOffset.x, spawnOffset.y, 0.0f);

        return spawnPosition;
    }

    /// <summary>
    /// 현재 웨이브에 맞는 적 생성 난이도를 적용하는 함수.
    /// </summary>
    /// <param name="wave">현재 웨이브 번호</param>
    public void ApplyDifficulty(int wave)
    {
        // 웨이브 1에서는 기본값을 유지하고, 이후 웨이브부터 감소량을 적용한다.
        float decreasedSpawnInterval = baseSpawnInterval - ((wave - 1) * spawnIntervalDecreasePerWave);

        if(decreasedSpawnInterval < minimumSpawnInterval)
        {
            decreasedSpawnInterval = minimumSpawnInterval;
        }

        spawnInterval = decreasedSpawnInterval;

        spawnCountPerCycle = baseSpawnCountPerCycle + ((wave - 1) / wavePerAddionalSpawn);
    }

    /// <summary>
    /// 적 생성 주기를 외부에서 설정하는 함수.
    /// </summary>
    /// <param name="newSpawnInterval">새 생성 주기</param>
    public void SetSpawnInterval(float newSpawnInterval)
    {
        spawnInterval = newSpawnInterval;
    }

    /// <summary>
    /// 한 번에 생성할 적 수를 외부에서 설정하는 함수.
    /// </summary>
    /// <param name="newSpawnCountPerCycle">새 생성 수</param>
    public void SetSpawnCountPerCycle(int newSpawnCountPerCycle)
    {
        spawnCountPerCycle = newSpawnCountPerCycle;
    }

    public float GetSpawnInterval()
    {
        return spawnInterval;
    }

    public int GetSpawnCountPerCycle()
    {
        return spawnCountPerCycle;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(playerTransform.position, spawnRadius);
    }
}
