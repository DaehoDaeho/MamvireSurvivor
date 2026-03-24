using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 적이 플레이어를 추적하는 역할을 하는 클래스.
/// 플레이어의 Transform을 참조.
/// 플레이어 방향 계산.
/// 적의 추적 이동 처리.
/// </summary>
public class EnemyChaser : MonoBehaviour
{
    [Header("추적 대상 설정")]
    [SerializeField] private Transform targetTransform = null;

    [Header("이동 설정")]
    [SerializeField] private float moveSpeed = 2.5f;

    [Header("디버그 확인용")]
    [SerializeField] private Vector2 moveDirection = Vector2.zero;
    [SerializeField] private Vector3 moveOffset = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(targetTransform == null)
        {
            // GameObject.Find : 인자로 전달한 이름에 해당되는 오브젝트를 씬에서 찾아 반환.
            GameObject playerObject = GameObject.Find("Player");
            if(playerObject != null)
            {
                targetTransform = playerObject.transform;
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
        if(targetTransform == null)
        {
            return;
        }

        // 이동방향 갱신.
        // 이동 처리.
        UpdateMoveDirection();
        MoveToTarget();
    }

    /// <summary>
    /// 플레이어 위치를 중심으로 적의 추적 방향을 계산.
    /// </summary>
    void UpdateMoveDirection()
    {
        Vector3 targetPosition = targetTransform.position;
        Vector3 currentPosition = transform.position;

        Vector3 directionToTarget = targetPosition - currentPosition;

        moveDirection = new Vector2(directionToTarget.x, directionToTarget.y);

        if(moveDirection.magnitude > 0.0f)
        {
            moveDirection = moveDirection.normalized;
        }
        else
        {
            moveDirection = Vector2.zero;
        }
    }

    void MoveToTarget()
    {
        moveOffset = new Vector3(moveDirection.x, moveDirection.y, 0.0f) * moveSpeed * Time.deltaTime;

        transform.position += moveOffset;
    }

    public Vector2 GetMoveDirection()
    {
        return moveDirection;
    }

    public Transform GetTargetTransform()
    {
        return targetTransform;
    }
}
