using UnityEngine;

/// <summary>
/// 무한으로 계속 이어지는 타일을 구현하는 역할.
/// </summary>
public class InfiniteMap : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float tileSize = 20.0f;

    private void Start()
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
        if(playerTransform == null)
        {
            return;
        }

        Vector3 playerPos = playerTransform.position;
        Vector3 myPos = transform.position;

        float diffX = playerPos.x - myPos.x;
        float diffY = playerPos.y - myPos.y;

        // Mathf.Abs : 절대값을 구하는 함수.
        float absDiffX = Mathf.Abs(diffX);
        // x축 거리가 타일 크기의 1.5배을 넘어가면 x축 재배치.
        if(absDiffX > tileSize * 1.5f)
        {
            float dirX = diffX > 0.0f ? 1.0f : -1.0f;
            transform.Translate(Vector3.right * dirX * (tileSize * 3.0f));
        }

        float absDiffY = Mathf.Abs(diffY);
        if(absDiffY > tileSize * 1.5f)
        {
            float dirY = diffY > 0.0f ? 1.0f : -1.0f;
            transform.Translate(Vector3.up * dirY * (tileSize * 3.0f));
        }
    }
}
