using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f;

    [SerializeField]
    private Vector2 moveDirection;  // 방향 벡터를 저장할 변수.

    [SerializeField]
    private Vector3 moveOffset = Vector3.zero;  // 이동량을 저장할 변수.

    // Update is called once per frame
    void Update()
    {
        ReadMovementInput();
        MovePlayer();
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

    void MovePlayer()
    {
        // 이동량 계산.
        moveOffset = new Vector3(moveDirection.x, moveDirection.y, 0.0f) * moveSpeed * Time.deltaTime;

        transform.position += moveOffset;
    }
}
