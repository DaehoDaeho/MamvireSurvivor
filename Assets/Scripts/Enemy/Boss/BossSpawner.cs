using UnityEngine;

/// <summary>
/// 마지막 웨이브에 보스를 한 번만 스폰하는 역할.
/// </summary>
public class BossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private int bossWave = 4;
    [SerializeField] private float spawnDistance = 10.0f;

    [SerializeField] private GameProgressController gameProgressController;
    [SerializeField] private Transform playerTransform;

    [SerializeField] private bool isBossSpawned = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(playerTransform == null)
        {
            PlayerHealth playerHealth = FindAnyObjectByType<PlayerHealth>();
            if(playerHealth != null)
            {
                playerTransform = playerHealth.transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isBossSpawned == true)
        {
            return;
        }

        int currentWave = gameProgressController.GetCurrentWave();
        if(currentWave >= bossWave)
        {
            // 보스 스폰.
            SpawnBoss();
        }
    }

    void SpawnBoss()
    {
        if(bossPrefab == null)
        {
            return;
        }

        if(playerTransform == null)
        {
            return;
        }

        // 반지름이 1인 원 안으로부터 랜덤한 좌표 정보를 뽑아서 방향을 계산한다.
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        if(randomDirection == Vector2.zero)
        {
            randomDirection = Vector2.right;
        }

        // 보스를 스폰할 위치 계산.
        // 플레이어의 위치로부터 spawnDistance 만큼 떨어진 곳에 임의의 방향값을 적용해서 계산.
        Vector3 spawnPosition = playerTransform.position + new Vector3(randomDirection.x,
            randomDirection.y, 0.0f) * spawnDistance;

        Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
        isBossSpawned = true;
    }
}
