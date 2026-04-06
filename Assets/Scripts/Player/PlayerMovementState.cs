using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovementState : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f;

    [SerializeField]
    private Vector2 moveDirection;  // 방향 벡터를 저장할 변수.

    [SerializeField]
    private Vector2 lastMoveDirection;  // 마지막 방향 정보를 저장할 변수.

    [SerializeField]
    private bool isMoving = false;  // 현재 플레이어가 이동 중인지 여부를 저장할 변수.

    [SerializeField]
    private Vector3 moveOffset = Vector3.zero;  // 이동량을 저장할 변수.

    private bool previousIsMoving = false;  // 이전 프레임의 이동 상태를 저장할 변수.

    // Update is called once per frame
    void Update()
    {
        ReadMovementInput();
        UpdateMovementState();
        MovePlayer();
        //DebugMovementStateChange();
    }

    void ReadMovementInput()
    {
        // 수평 입력을 읽어온다.
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // 수직 입력을 읽어온다.
        float verticalInput = Input.GetAxisRaw("Vertical");

        // 방향 벡터 생성.
        moveDirection = new Vector2(horizontalInput, verticalInput);

        // 방향 벡터의 길이가 1보다 큰지 검사.
        // Vector2.magnitude : 벡터의 길이.
        if (moveDirection.magnitude > 1.0f)
        {
            // 벡터를 정규화해서 길이를 1로 만든다.
            moveDirection = moveDirection.normalized;
        }
    }

    void UpdateMovementState()
    {
        // Vector2.zero : 제로벡터/영벡터 -> (0, 0)
        if (moveDirection != Vector2.zero)
        {
            isMoving = true;
            lastMoveDirection = moveDirection;
        }
        else
        {
            isMoving = false;
        }
    }

    void MovePlayer()
    {
        // 이동량 계산.
        moveOffset = new Vector3(moveDirection.x, moveDirection.y, 0.0f) * moveSpeed * Time.deltaTime;

        transform.position += moveOffset;
    }

    void DebugMovementStateChange()
    {
        // 이전 상태와 현재 상태가 다른지 비교.
        if(isMoving != previousIsMoving)
        {
            Debug.Log("이동 상태 변화 : " + "isMoving: " + isMoving + ", moveDirectioin: " + moveDirection +
                ", lastMoveDirection: " + lastMoveDirection);
        }

        // 다음 프레임에 비교를 하기 위해 현재 상태를 저장해둔다.
        previousIsMoving = isMoving;
    }

    public Vector2 GetMoveDirection()
    {
        return moveDirection;
    }

    public Vector2 GetLastMoveDirection()
    {
        return lastMoveDirection;
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public void SetMoveSpeed(float newMoveSpeed)
    {
        moveSpeed = newMoveSpeed;
    }
}
